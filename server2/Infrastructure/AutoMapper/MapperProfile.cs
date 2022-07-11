using AutoMapper;
using Domain.Models;
using Infrastructure.DTO.RoleDTOs;
using Infrastructure.DTO.TagDTOs;
using Infrastructure.DTO.UserDTOs;
using Infrastructure.DTO.CoinDTOs;
using Infrastructure.DTO.CommentDTOs;
using Infrastructure.DTO.LikeDTOs;
using Infrastructure.DTO.TagDTOs;
using Infrastructure.DTO.UserCoinDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.DTO.PostDTO;
using Infrastructure.DTO.UserRoleDTOs;
using Infrastructure.DTO.ReplyDTOs;
using Infrastructure.DTO.UserFollowerDTOs;

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
            CreateMap<UserCoin, UserCoinDTO>();
            CreateMap<Post, PostDTO>();
            CreateMap<Reply, ReplyDTO>();
            CreateMap<UserFollower, UserFollowerDTO>();
            #endregion

            #region DTO to Model
            CreateMap<RegisterDTO, User>();
            CreateMap<LikeDTO, Like>();
            CreateMap<CoinDTO, Coin>();
            CreateMap<CommentDTO, Comment>();
            CreateMap<PostDTO, Post>();
            CreateMap<UserCoinDTO, UserCoin>();
            CreateMap<ReplyDTO, Reply>();
            CreateMap<UserFollowerDTO, UserFollower>();

            #endregion

        }
    }
}
