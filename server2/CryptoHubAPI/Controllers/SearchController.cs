using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
namespace CryptoHubAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SearchController : Controller
    {

        private readonly IUserFollowerRepository _userFollowerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICoinRepository _coinRepository;

        public SearchController(IUserRepository userRepository, IUserFollowerRepository userFollowerRepository, ICoinRepository coinRepository)
        {
            _userFollowerRepository = userFollowerRepository;
            _userRepository = userRepository;
            _coinRepository = coinRepository;
        }
    }
}

