using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult GetDocument(DocumentListDto dto)
        {
            try
            {
                if(dto.DocumentType == "UserDocument")
                {
                    var document = _repo.UserDocuments.FindByCondition(a => a.Name == dto.DocumentName && a.UserId == dto.UserId);
                    if (document.Any())
                    {
                        UserDocumentModel model = document.First();

                        byte[] bytes = _repo.DocumentHelper.GetDocumentData(model.URL, dto.DocumentName);

                        UserDocumentDto response = new()
                        {
                            Id = model.Id,
                            DocumentName = model.Name,
                            b64 = "data:application/pdf;base64," + Convert.ToBase64String(bytes, 0, bytes.Length),
                        };
                        return Ok(response);

                    }

                }
                else if(dto.DocumentType == "ApplicationDocument")
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
        public async Task<IActionResult> ApplicationDocsList(int applicationId,int userId)
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
    }
}