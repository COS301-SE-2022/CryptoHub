﻿using AutoMapper;
using Domain.Models;
using Infrastructure.DTO.LikeDTOs;
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
            CreateMap<Like, LikeDTO>();
            #endregion

            #region DTO to Model
            CreateMap<RegisterDTO, User>();
            CreateMap<LikeDTO, Like>();
            #endregion

        }
    }
}
