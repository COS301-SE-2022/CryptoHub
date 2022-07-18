using Domain.Models;
using Infrastructure.DTO.ImageDTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intergration.FireStoreService
{
    public interface IFireStorageService
    {
        Task<Response<Image>> UploadImage(CreateImageDTO imageDTO);
    }
}
