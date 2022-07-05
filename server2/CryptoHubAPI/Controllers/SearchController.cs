﻿using Domain.IRepository;
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

        [HttpGet("{name}")]
        public async Task<ActionResult<List<User>>> SearchUser(string name, int id)
        {
            //fetch all search results
            var results = await _userRepository.FindRange(u => u.Username.ToLower().StartsWith(name.ToLower()) || u.Firstname.ToLower().StartsWith(name.ToLower()) || u.Lastname.ToLower().StartsWith(name.ToLower()));
            if (results == null)
                return NotFound();

            var followers = await _userFollowerRepository.FindRange(uf => uf.FollowId == id);
            var users = await _userRepository.GetAll();

            var userfollowers = from f in followers
                                join u in users
                                on f.UserId equals u.UserId
                                select new User
                                {
                                    UserId = u.UserId,
                                    Firstname = u.Firstname,
                                    Lastname = u.Lastname,
                                    Username = u.Username,
                                };

            var searchFollowers = from r in results
                          join f in userfollowers
                          on r.UserId equals f.UserId
                          select new User
                          {
                              UserId = r.UserId,
                              Firstname = r.Firstname,
                              Lastname = r.Lastname,
                              Username = r.Username,
                          };
            var sf = searchFollowers.ToList();
            var final = sf;

            foreach (var r in results.ToList())
            {
                foreach (var m in sf.ToList())
                {
                    if (m.UserId == r.UserId)
                    {
                        results.Remove(r);
                    }
                }
            }
            foreach (var r in results)
            {
                final.Add(r);
            }




            return Ok(final);
        }
    }
}

