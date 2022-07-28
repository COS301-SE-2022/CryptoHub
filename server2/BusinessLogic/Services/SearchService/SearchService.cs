using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.UserDTOs;
using Infrastructure.DTO.CoinDTOs;


namespace BusinessLogic.Services.SearchService
{
    public class SearchService : ISearchService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFollowerRepository _userFollowerRepository;
        private readonly ICoinRepository _coinRepository;
        private readonly IUserCoinRepository _userCoinRepository;
        private readonly IMapper _mapper;

        public SearchService(IUserRepository userRepository, IUserFollowerRepository userFollowerRepository,
            ICoinRepository coinRepository, IUserCoinRepository userCoinRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _userFollowerRepository = userFollowerRepository;
            _coinRepository = coinRepository;
            _userCoinRepository = userCoinRepository;
            _mapper = mapper;
        }

        public async Task<List<SearchDTO>> SearchUser(int id, string searchterm)
        {
            //fetch all search results
            var results = await _userRepository.FindRange(u => u.Username.ToLower().StartsWith(searchterm.ToLower()) ||
            u.Firstname.ToLower().StartsWith(searchterm.ToLower()) || u.Lastname.ToLower().StartsWith(searchterm.ToLower()));

            var resultList = new List<SearchDTO>();

            foreach (var result in results)
            {
                var temp = new SearchDTO
                {
                    UserId = result.UserId,
                    Firstname = result.Firstname,
                    Lastname = result.Lastname,
                    Username = result.Username,
                    followCount = 0
                };
                resultList.Add(temp);
            }

            foreach (var r in resultList)
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
                r.followCount = userFol.Count();
            }

            resultList = resultList.OrderByDescending(r => r.followCount).ToList();

            var followers = await _userFollowerRepository.FindRange(uf => uf.FollowId == id);
            var users = await _userRepository.GetAll();

            var userfollowers = from f in followers
                                join u in users
                                on f.UserId equals u.UserId
                                select new SearchDTO
                                {
                                    UserId = u.UserId,
                                    Firstname = u.Firstname,
                                    Lastname = u.Lastname,
                                    Username = u.Username,
                                };

            //join current users followers and search results
            var searchFollowers = from r in resultList
                                  join f in userfollowers
                                  on r.UserId equals f.UserId
                                  select new SearchDTO
                                  {
                                      UserId = r.UserId,
                                      Firstname = r.Firstname,
                                      Lastname = r.Lastname,
                                      Username = r.Username,
                                      followedByCurrentUser = true
                                  };

            //rearange results to show followed users first
            var sf = searchFollowers.ToList();
            var final = sf;

            //find all mutual followers
            var mutuals = new List<SearchDTO>();
            foreach (var usf in userfollowers.ToList())
            {
                var mutFollowers = await _userFollowerRepository.FindRange(uf => uf.FollowId == usf.UserId);
                var mutUsers = await _userRepository.GetAll();

                var mutUserfollowers = from f in mutFollowers
                                       join u in mutUsers
                                    on f.UserId equals u.UserId
                                       select new SearchDTO
                                       {
                                           UserId = u.UserId,
                                           Firstname = u.Firstname,
                                           Lastname = u.Lastname,
                                           Username = u.Username,
                                       };

                foreach (var r in resultList.ToList())
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
            foreach (var r in resultList.ToList())
            {
                foreach (var m in sf.ToList())
                {
                    if (m.UserId == r.UserId)
                    {
                        resultList.Remove(r);
                    }
                }
                foreach (var m in mutuals.ToList())
                {
                    if (m.UserId == r.UserId)
                    {
                        resultList.Remove(r);
                    }
                }
            }
            foreach (var s in sf.ToList())
            {
                foreach (var m in mutuals.ToList())
                {
                    if (m.UserId == s.UserId)
                    {
                        mutuals.Remove(m);
                    }
                }
            }

            //add mutuals to result
            foreach (var m in mutuals.ToList())
            {
                final.Add(m);
            }

            //add rest of the result
            foreach (var r in resultList.ToList())
            {
                final.Add(r);
            }

            return _mapper.Map<List<SearchDTO>>(final);
        }

        //************************************************************************************************************************************//
        //************************************************************************************************************************************//

        public async Task<List<CoinSearchDTO>> SearchCoin(int id, string searchterm)
        {
            //fetch all search results
            var results = await _coinRepository.FindRange(c => c.CoinName.ToLower().StartsWith(searchterm.ToLower()));

            //Finds and rearanges result based on follow count
            foreach (var r in results)
            {
                var userCoins = await _userCoinRepository.FindRange(uc => uc.CoinId == r.CoinId);
                var allCoins = await _coinRepository.GetAll();


                var userFol = from uc in userCoins
                              join c in allCoins
                              on uc.CoinId equals c.CoinId
                              select new CoinSearchDTO
                              {
                                  CoinId = c.CoinId,
                                  CoinName = c.CoinName
                              };
                r.ImageId = userFol.Count();
            }

            results = results.OrderByDescending(r => r.ImageId).ToList();

            //Find directly followed coins
            var followedCoins = await _userCoinRepository.FindRange(uc => uc.UserId == id);
            var coins = await _coinRepository.GetAll();

            var usercoins = from fc in followedCoins
                            join c in coins
                            on fc.CoinId equals c.CoinId
                            select new CoinSearchDTO
                            {
                                CoinId = c.CoinId,
                                CoinName = c.CoinName
                            };

            //join current users followers and search results
            var searchFollowers = from r in results
                                  join uc in usercoins
                                  on r.CoinId equals uc.CoinId
                                  select new CoinSearchDTO
                                  {
                                      CoinId = uc.CoinId,
                                      CoinName = uc.CoinName
                                  };

            //rearange results to show followed coins first
            var sf = searchFollowers.ToList();
            var final = sf;

            //find all mutual followers
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

            var mutuals = new List<CoinSearchDTO>();
            foreach (var usf in userfollowers.ToList())
            {
                var mutFollowers = await _userCoinRepository.FindRange(uc => uc.UserId == usf.UserId);
                var mutCoins = await _coinRepository.GetAll();

                var mutCoinfollowers = from f in mutFollowers
                                       join c in mutCoins
                                       on f.CoinId equals c.CoinId
                                       select new CoinSearchDTO
                                       {
                                           CoinId = c.CoinId,
                                           CoinName = c.CoinName
                                       };

                foreach (var r in results.ToList())
                {
                    foreach (var m in mutCoinfollowers.ToList())
                    {
                        if (m.CoinId == r.CoinId)
                        {
                            mutuals.Add(m);
                        }
                    }
                }
            }
            //remove deplicates from mutuals
            mutuals = mutuals.GroupBy(x => x.CoinId).Select(x => x.First()).ToList();

            //add mutual followers to the results
            foreach (var r in results.ToList())
            {
                foreach (var m in sf.ToList())
                {
                    if (m.CoinId == r.CoinId)
                    {
                        results.Remove(r);
                    }
                }
                foreach (var m in mutuals.ToList())
                {
                    if (m.CoinId == r.CoinId)
                    {
                        results.Remove(r);
                    }
                }
            }
            foreach (var s in sf.ToList())
            {
                foreach (var m in mutuals.ToList())
                {
                    if (m.CoinId == s.CoinId)
                    {
                        mutuals.Remove(m);
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
                final.Add(new CoinSearchDTO
                {
                    CoinId = r.CoinId,
                    CoinName = r.CoinName
                });
            }

            return _mapper.Map<List<CoinSearchDTO>>(final);
        }
    }
}
