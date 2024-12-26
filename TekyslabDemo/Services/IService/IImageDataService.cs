using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekyslabDemo.Models;
using TekyslabDemo.Models.ApiResponseModels;

namespace TekyslabDemo.Services.IService
{
    //[Headers("Authorization: Bearer")]
    public interface IImageDataService
    {
        [Put("/api/users/{id}")]
        Task<ResponseMessage> PostImageData(int id,[Body] RequestImagesModel imagesModel);
    }
}
