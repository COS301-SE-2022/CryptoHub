using AutoMapper;
using Domain.Models;
using Infrastructure.DTO.RoleDTOs;
using Infrastructure.DTO.TagDTOs;
using Infrastructure.DTO.UserDTOs;
using Infrastructure.DTO.CoinDTOs;
using Infrastructure.DTO.CommentDTOs;
using Infrastructure.DTO.LikeDTOs;
using Infrastructure.DTO.TagDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.DTO.UserRoleDTOs;

namespace Infrastructure.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region Model to DTO
            CreateMap<User, UserDTO>();
            CreateMap<Like, LikeDTO>();
            CreateMap<Coin, CoinDTO>();
            CreateMap<Comment, CommentDTO>();
            CreateMap<Role, RoleDTO>();
            CreateMap<Tag, TagDTO>();
            CreateMap<UserRole, UserRoleDTO>();
            #endregion

            #region DTO to Model
            CreateMap<RegisterDTO, User>();
            CreateMap<LikeDTO, Like>();
            CreateMap<CoinDTO, Coin>();
            CreateMap<CommentDTO, Comment>();
            #endregion

        }
    }
}
