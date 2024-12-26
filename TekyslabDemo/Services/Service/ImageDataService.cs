
using TekyslabDemo.Services.IService;
using TekyslabDemo.Models.ApiResponseModels;
using TekyslabDemo.Models;

namespace TekyslabDemo.Services.Service
{
    public class ImageDataService
    {
        public static async Task<ResponseMessage> PostImageData(IImageDataService imageDataService, RequestImagesModel imageData)
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                response = await imageDataService.PostImageData(1, imageData);
            }
            catch (Exception ex)
            {
                //response.Message = ex.Message;
            }
            return response;
        }
    }
}
