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

        [HttpGet("{name}")]
        public async Task<ActionResult<List<User>>> SearchUser(string name, int id)
        {
            //fetch all search results
            var results = await _userRepository.FindRange(u => u.Username.ToLower().StartsWith(name.ToLower()) || u.Firstname.ToLower().StartsWith(name.ToLower()) || u.Lastname.ToLower().StartsWith(name.ToLower()));
            if (results == null)
                return NotFound();

            foreach (var r in results)
            {
                var fol = await _userFollowerRepository.FindRange(uf => uf.UserId == r.UserId);
                var allUsers = await _userRepository.GetAll();



                var userFol = from f in fol
                              join u in allUsers
                              on f.FollowId equals u.UserId
                              select new
                              {
                                  UserId = u.UserId,
                                  Username = u.Username
                              };
                r.ImageId = userFol.Count();
                r.Email = null;
                r.Password = null;
            }

            results = results.OrderByDescending(r => r.ImageId).ToList();

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

            //join current users followers and search results
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

            //rearange results to show followed users first
            var sf = searchFollowers.ToList();
            var final = sf;

            //find all mutual followers
            var mutuals = new List<User>();
            foreach (var usf in userfollowers.ToList())
            {
                var mutFollowers = await _userFollowerRepository.FindRange(uf => uf.FollowId == usf.UserId);
                var mutUsers = await _userRepository.GetAll();

                var mutUserfollowers = from f in mutFollowers
                                       join u in mutUsers
                                    on f.UserId equals u.UserId
                                       select new User
                                       {
                                           UserId = u.UserId,
                                           Firstname = u.Firstname,
                                           Lastname = u.Lastname,
                                           Username = u.Username,
                                       };

                foreach (var r in results.ToList())
                {
                    foreach (var m in mutUserfollowers.ToList())
                    {
                        if (m.UserId == r.UserId)
                        {
                            mutuals.Add(m);
                        }
                    }
                }
            }
            //remove deplicates from mutuals
            mutuals = mutuals.GroupBy(x => x.UserId).Select(x => x.First()).ToList();

            //add mutual followers to the results
            foreach (var r in results.ToList())
            {
                foreach (var m in sf.ToList())
                {
                    if (m.UserId == r.UserId)
                    {
                        results.Remove(r);
                    }
                }
                foreach (var m in mutuals.ToList())
                {
                    if (m.UserId == r.UserId)
                    {
                        results.Remove(r);
                    }
                }
            }

            //add mutuals to result
            foreach (var m in mutuals.ToList())
            {
                final.Add(m);
            }

            //add rest of the result
            foreach (var r in results.ToList())
            {
                final.Add(r);
            }

            return Ok(final);
        }
    }
}

