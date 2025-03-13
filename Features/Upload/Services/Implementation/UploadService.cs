using Microsoft.EntityFrameworkCore;
using verticalSliceArchitecture.Data;
using verticalSliceArchitecture.Domain;
using verticalSliceArchitecture.Features.Upload.DTOs;
using verticalSliceArchitecture.Features.Upload.Services.Interface;
using verticalSliceArchitecture.Shared;

namespace verticalSliceArchitecture.Features.Upload.Services.Implementation
{
    public class UploadService(ApplicationDbContext _Context, IAppSettings _appSettings) : IUploadService
    {
        public async Task<Result> uploadImage(UploadImageRequestDTO requestDTO)
        {
            try
            {
                var uploadFolder = Path.Combine(_appSettings.Drive, _appSettings.Path);
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
                var sanitizedFileName = Path.GetFileNameWithoutExtension(requestDTO.File.FileName);
                var uniqueFileName = $"{sanitizedFileName}_{Guid.NewGuid()}{Path.GetExtension(requestDTO.File.FileName).ToLower()}";
                var filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await requestDTO.File.CopyToAsync(stream);
                }
        
                UploadFile upload = new();

                upload.FilePath = filePath;
                upload.OriginalFileName = requestDTO.File.FileName;
                upload.FileName = uniqueFileName;


                _Context.Uploads.Add(upload);
                await _Context.SaveChangesAsync();

                return Result.Success(new { FilePath = filePath, Message = "Successfully Uploaded!!!" });

            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Failure("UploadFile Failed", $"UploadFile Failed :{ex.Message}"));
            }
        }
    }
}
