using Domain.Models;
using Infrastructure.DTO.ImageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intergration.FireStoreService
{
    public interface IFireStorageService
    {
        Task<Response<string>> UploadImage(ImageDTO imageDTO);
    }
}
