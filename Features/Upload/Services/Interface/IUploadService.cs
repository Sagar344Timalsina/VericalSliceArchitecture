using verticalSliceArchitecture.Features.Upload.DTOs;
using verticalSliceArchitecture.Shared;

namespace verticalSliceArchitecture.Features.Upload.Services.Interface
{
    public interface IUploadService
    {
        Task<Result> uploadImage(UploadImageRequestDTO requestDTO);

    }
}
