using System;
using Domain.IRepository;

namespace BusinessLogic.Services.UserFollowerService
{
    public class UserFollowerService : IUserFollowerService
    {
        private readonly IUserFollowerRepository _userFollowerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserFollowerService(IUserFollowerRepository userFollowerRepository, IUserRepository userRepository, )
        {
            _userFollowerRepository = userFollowerRepository;
            _userRepository = userRepository;
        }
    }
}

