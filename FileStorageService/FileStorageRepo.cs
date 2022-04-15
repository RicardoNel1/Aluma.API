using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using DataService.Dto;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileStorageService
{
    public interface IFileStorageRepo
    {
        Task UploadAsync(FileStorageDto dto);

        Task<byte[]> DownloadAsync(FileStorageDto dto);

        Task DeleteAsync(FileStorageDto dto);

        Task DeleteAllAsync(FileStorageDto dto);
    }

    public class FileStorageRepo : IFileStorageRepo
    {
        private readonly ShareServiceClient _shareServiceClient;

        public FileStorageRepo(ShareServiceClient shareServiceClient)
        {
            _shareServiceClient = shareServiceClient;
        }

        public async Task UploadAsync(FileStorageDto dto)
        {
            if (string.IsNullOrEmpty(dto.FileDirectory))
                throw new Exception("The File Directory cannot be empty.");

            if (string.IsNullOrEmpty(dto.FileName))
                throw new Exception("The File Name cannot be empty.");

            if (dto.FileBytes == null || dto.FileBytes.Length == 0)
                throw new Exception("The File's Bytes cannot be empty.");

            // Get a reference to a share and then create it
            ShareClient share = await CreateShareAsync(dto.BaseShare.ToLower());
            dto.FileDirectory = dto.FileDirectory.Replace($"{share.Uri}/", "").ToLower();

            // Get a reference to a directory and create it
            ShareDirectoryClient directory = await CreateFullDirectoryAsync(share, dto.FileDirectory);

            // Get a reference to a file and upload it
            ShareFileClient file = directory.GetFileClient(dto.FileName);

            // Upload file
            using MemoryStream stream = new MemoryStream(dto.FileBytes, 0, dto.FileBytes.Length);
            {
                try
                {
                    if (file.Exists() == false)
                    {
                        await file.CreateAsync(stream.Length);

                        //file.UploadRange(new HttpRange(0, stream.Length), stream);

                        int blockSize = 1 * 1024;
                        long offset = 0;//Define http range offset
                        BinaryReader reader = new BinaryReader(stream);
                        while (true)
                        {
                            byte[] buffer = reader.ReadBytes(blockSize);
                            if (buffer.Length == 0)
                                break;

                            MemoryStream uploadChunk = new MemoryStream();
                            uploadChunk.Write(buffer, 0, buffer.Length);
                            uploadChunk.Position = 0;

                            HttpRange httpRange = new HttpRange(offset, buffer.Length);
                            await file.UploadRangeAsync(httpRange, uploadChunk);
                            offset += buffer.Length;//Shift the offset by number of bytes already written
                        }

                        reader.Close();

                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    throw new Exception(error, ex);
                }
            }
        }

        public async Task<byte[]> DownloadAsync(FileStorageDto dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.FileDirectory))
                    throw new Exception("The File Directory cannot be empty.");

                if (string.IsNullOrEmpty(dto.FileName))
                    throw new Exception("The File Name cannot be empty.");

                // Get a reference to the file
                ShareClient share = _shareServiceClient.GetShareClient(dto.BaseShare.ToLower());
                dto.FileDirectory = dto.FileDirectory.Replace($"{share.Uri}/", "").ToLower();

                ShareDirectoryClient directory = share.GetDirectoryClient(dto.FileDirectory);
                ShareFileClient file = directory.GetFileClient(dto.FileName);

                ShareFileDownloadInfo download = await file.DownloadAsync();
                return await GetBytesFromStream(download.Content);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw new Exception(error, ex);
            }
        }

        public async Task DeleteAsync(FileStorageDto dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.FileDirectory))
                    throw new Exception("The File Directory cannot be empty.");

                if (string.IsNullOrEmpty(dto.FileName))
                    throw new Exception("The File Name cannot be empty.");

                // Get a reference to the file
                ShareClient share = _shareServiceClient.GetShareClient(dto.BaseShare.ToLower());
                dto.FileDirectory = dto.FileDirectory.Replace($"{share.Uri}/", "").ToLower();

                ShareDirectoryClient directory = share.GetDirectoryClient(dto.FileDirectory);
                ShareFileClient file = directory.GetFileClient(dto.FileName);
                await file.DeleteIfExistsAsync();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw new Exception(error, ex);
            }
        }

        public async Task DeleteAllAsync(FileStorageDto dto)
        {
            try
            {
                ShareClient share = _shareServiceClient.GetShareClient(dto.BaseShare.ToLower());
                ShareDirectoryClient directory = share.GetRootDirectoryClient();

                await foreach (ShareFileItem item in directory.GetFilesAndDirectoriesAsync())
                {
                    if (item.IsDirectory)
                    {
                        var subDir = directory.GetSubdirectoryClient(item.Name);

                        await foreach (ShareFileItem subitem in subDir.GetFilesAndDirectoriesAsync())
                        {
                            if (!subitem.IsDirectory)
                            {
                                await subDir.DeleteFileAsync(subitem.Name);
                            }
                            else
                            {
                                var subDir1 = subDir.GetSubdirectoryClient(subitem.Name);
                                await foreach (ShareFileItem subitem1 in subDir1.GetFilesAndDirectoriesAsync())
                                {
                                    if (!subitem1.IsDirectory)
                                    {
                                        await subDir1.DeleteFileAsync(subitem.Name);
                                    }
                                    else
                                    {
                                        var subDir2 = subDir1.GetSubdirectoryClient(subitem1.Name);
                                        await foreach (ShareFileItem subitem2 in subDir2.GetFilesAndDirectoriesAsync())
                                        {
                                            if (!subitem2.IsDirectory)
                                            {
                                                await subDir2.DeleteFileAsync(subitem2.Name);

                                            }
                                            else
                                            {
                                                var subDir3 = subDir2.GetSubdirectoryClient(subitem2.Name);
                                                await foreach (ShareFileItem subitem3 in subDir3.GetFilesAndDirectoriesAsync())
                                                {

                                                    await subDir3.DeleteFileAsync(subitem3.Name);


                                                }
                                                await subDir3.DeleteAsync();
                                            }
                                        }
                                        await subDir2.DeleteAsync();

                                    }
                                }
                                await subDir1.DeleteAsync();
                            }
                        }
                        await subDir.DeleteAsync();
                    }
                    else
                    {
                        await directory.DeleteFileAsync(item.Name);
                    }
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw new Exception(error, ex);
            }
        }

        private async Task<ShareClient> CreateShareAsync(string baseShare)
        {
            ShareClient share = _shareServiceClient.GetShareClient(baseShare);

            try
            {
                await share.CreateAsync();
            }
            catch (RequestFailedException ex)
            {
                if (ex.ErrorCode != ShareErrorCode.ShareAlreadyExists)
                {
                    throw;
                }
            }

            return share;
        }

        private async Task<ShareDirectoryClient> CreateFullDirectoryAsync(ShareClient share, string fileDirectory)
        {
            ShareDirectoryClient directory = null;
            var dirSections = fileDirectory.Split('/');
            var currentDir = dirSections[0];
            try
            {
                directory = await CreateDirectoryAsync(share, currentDir);
                if (dirSections.Length > 1)
                {
                    for (int i = 1; i < dirSections.Length; i++)
                    {
                        currentDir = $"{currentDir}/{dirSections[i]}";
                        directory = await CreateDirectoryAsync(share, currentDir);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return directory;
        }

        private async Task<ShareDirectoryClient> CreateDirectoryAsync(ShareClient share, string fileDirectory)
        {
            ShareDirectoryClient directory = share.GetDirectoryClient(fileDirectory);
            try
            {
                await directory.CreateAsync();
            }
            catch (RequestFailedException ex)
            {
                if (ex.ErrorCode != ShareErrorCode.ResourceAlreadyExists)
                {
                    throw;
                }
            }

            return directory;
        }

        private async Task<byte[]> GetBytesFromStream(Stream stream)
        {
            int bufferSize = 16 * 1024;
            using (MemoryStream ms = new MemoryStream())
            {
                await stream.CopyToAsync(ms, bufferSize);
                return ms.ToArray();
            }
        }
    }
}