using Aluma.API.RepoWrapper;
using Aluma.API.Helpers.Extensions;
using AutoMapper;
using DataService.Dto;
using DataService.Enum;
using DataService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class DocumentController : ControllerBase
    {
        private readonly IWrapper _repo;

        public DocumentController(IWrapper repo, IMapper mapper)
        {
            _repo = repo;
        }


        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> GetDocument(DocumentListDto dto)
        {
            try
            {
                if (dto.DocumentType == "UserDocument")
                {
                    var document = _repo.UserDocuments.FindByCondition(a => a.Name == dto.DocumentName && a.UserId == dto.UserId);
                    if (document.Any())
                    {
                        UserDocumentModel model = document.First();

                        byte[] bytes = await _repo.DocumentHelper.GetDocumentDataAsync(model.URL, dto.DocumentName);

                        UserDocumentDto response = new()
                        {
                            Id = model.Id,
                            DocumentName = model.Name,
                            b64 = "data:application/pdf;base64," + Convert.ToBase64String(bytes, 0, bytes.Length),
                        };
                        return Ok(response);

                    }

                }
                else if (dto.DocumentType == "ApplicationDocument")
                {
                    var document = _repo.ApplicationDocuments.FindByCondition(a => a.Name == dto.DocumentName && a.ApplicationId == dto.ApplicationId);

                    if (document.Any())
                    {
                        ApplicationDocumentModel model = document.First();
                        byte[] bytes = _repo.DocumentHelper.GetDocumentData(model.URL, dto.DocumentName);

                        ApplicationDocumentDto response = new()
                        {
                            Id = model.Id,
                            DocumentName = model.Name,
                            b64 = "data:application/pdf;base64," + Convert.ToBase64String(bytes, 0, bytes.Length),
                        };
                        return Ok(response);
                    }
                }

                return BadRequest("Document couldn't be downloaded");


            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("get_client_Docuemnt"), DisableRequestSizeLimit, AllowAnonymous]
        public async Task<IActionResult> GetClientDocument(int documentId, int userId, int applicationId, string documentName, string documentType)
        {
            try
            {
                byte[] pdf = new byte[0];

                if (documentType == "UserDocument")
                {
                    var document = _repo.UserDocuments.FindByCondition(a => a.Name == documentName && a.UserId == userId && a.Id == documentId);
                    if (document.Any())
                    {
                        UserDocumentModel model = document.First();
                        pdf = await _repo.DocumentHelper.GetDocumentDataAsync(model.URL, documentName);
                    }
                }
                else if (documentType == "ApplicationDocument")
                {
                    var document = _repo.ApplicationDocuments.FindByCondition(a => a.Name == documentName && a.ApplicationId == applicationId && a.Id == documentId);
                    if (document.Any())
                    {
                        ApplicationDocumentModel model = document.First();
                        pdf = await _repo.DocumentHelper.GetDocumentDataAsync(model.URL, documentName);
                    }
                }

                if (pdf != null && pdf.Length > 0)
                {
                    Stream stream = new MemoryStream(pdf);
                    stream.Position = 0;

                    return File(stream, MediaTypeNames.Application.Octet, documentName);
                }

                return BadRequest($"Could not download the '{documentName}'");
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not download the '{documentName}', {ex.Message}, {ex.InnerException?.Message}");
            }
        }

        [HttpGet("deleteAll"), AllowAnonymous]
        public IActionResult DeleteAll()
        {
            try
            {
                _repo.DocumentHelper.DeleteAllDocuments();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpGet("application/list"), AllowAnonymous]
        public async Task<IActionResult> ApplicationDocsList(int applicationId, int userId)
        {
            try
            {
                //not actual documents just list
                List<DocumentListDto> docs = await _repo.DocumentHelper.GetApplicationDocListAsync(applicationId, userId);

                return Ok(docs);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("application/list/download")]
        public IActionResult ApplicationDocsDownload(ApplicationDto dto)
        {
            try
            {
                // actual documents, just list of base64 strings
                var docList = _repo.ApplicationDocuments.GetDocuments(dto);

                return Ok(docList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("application/list/id"), AllowAnonymous]
        public IActionResult GetApplicationDocument(ApplicationDocumentDto dto)
        {
            try
            {
                var doc = _repo.ApplicationDocuments.GetDocument(dto);

                return Ok(doc);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("user/list"), AllowAnonymous]
        public async Task<IActionResult> UserDocsList(int userId)
        {
            try
            {
                //not actual documents just list
                List<DocumentListDto> docs = await _repo.DocumentHelper.GetUserDocListAsync(userId);

                return Ok(docs);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("user/list/download")]
        public IActionResult UserDocsDownload(UserDto dto)
        {
            try
            {
                // actual documents, just list of base64 strings
                var docList = _repo.UserDocuments.GetDocuments(dto.Id);

                return Ok(docList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("user/list/id"), AllowAnonymous]
        public IActionResult GetUserDocument(UserDocumentDto dto)
        {
            try
            {
                var doc = _repo.UserDocuments.GetDocument(dto);

                return Ok(doc);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("user/upload")]
        public IActionResult UserUploadMultipleDocuments([FromBody] List<UserDocumentDto> dto)
        {
            try
            {
                foreach (var doc in dto)
                {
                    _repo.UserDocuments.UploadDocument(doc);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("user/upload/{type}/{clientId}"), DisableRequestSizeLimit, AllowAnonymous]
        public async Task<IActionResult> UploadDocument(int clientId, string type, IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    byte[] docData = await file.GetBytes();

                    switch (type.ToLower())
                    {
                        case "consent":
                            {
                                await _repo.Client.UploadConsentForm(docData, clientId);
                                break;
                            }
                        case "policy-schedule":
                            {
                                await _repo.Client.UploadOtherDocuments(docData, $"{file.FileName.Replace(".pdf", "")} ", DocumentTypesEnum.PolicyShedule, clientId);
                                break;
                            }
                        default:
                            throw new Exception("Invanid document type");
                    }
                    return NoContent();
                }

                return BadRequest("There is no file to upload");
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not upload selected file. {ex.Message}");
            }
        }
    }
}