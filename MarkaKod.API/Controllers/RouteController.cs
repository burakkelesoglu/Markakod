using MarkaKod.API.Repo;
using MarkaKod.Model;
using Microsoft.AspNetCore.Mvc;

namespace MarkaKod.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        ILocationRepo _repo;
        public RouteController(ILocationRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("list")]
        public IEnumerable<Location> GetList()
        {
            var list = _repo.List();

            var currentLocaiton = list.FirstOrDefault(x => x.CurrentLocation && !x.Visited);

            if (currentLocaiton == null)
            {
                currentLocaiton = list.FirstOrDefault(x => !x.Visited);
                currentLocaiton.Km = 0;
            }

            var visitedLocaltionList = list.Where(x => x.Visited);
            List<Location> routeList = new List<Location>();


            routeList.Add(currentLocaiton);

            var notVisitedList = list.Where(x => !x.Visited && !routeList.Contains(x)).ToList();

            while (notVisitedList != null && notVisitedList.Count > 0)
            {
                foreach (var location in notVisitedList)
                {
                    location.Km = Distance.CalculateDistance(currentLocaiton.Lat, currentLocaiton.Long, location.Lat, location.Long);
                    location.CurrentLocation = false;
                }

                currentLocaiton = notVisitedList.OrderBy(x => x.Km).First();
                routeList.Add(currentLocaiton);

                notVisitedList = list.Where(x => !x.Visited && !routeList.Contains(x)).ToList();
            }

            routeList.AddRange(visitedLocaltionList);

            return routeList;
        }

        [HttpGet]
        [Route("getbyid")]
        public Location GetById(int id)
        {
            var result = _repo.GetById(id);
            return result;
        }


        [HttpPost]
        [Route("create")]
        public Location Create(Location model)
        {
            var result = _repo.Create(model);
            return result;
        }

        [HttpPut]
        [Route("update")]
        public Location Update(Location model)
        {
            var result = _repo.Update(model);
            return result;
        }
    }
}
