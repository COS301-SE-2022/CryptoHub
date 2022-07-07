using AutoMapper;
using Domain.Models;
using Infrastructure.DTO.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region Model to DTO
            CreateMap<User, UserDTO>();
            #endregion

            #region DTO to Model
            CreateMap<RegisterDTO, User>();
            #endregion

        }
    }
}
