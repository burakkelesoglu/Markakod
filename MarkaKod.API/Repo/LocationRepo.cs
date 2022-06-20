using Dapper;
using MarkaKod.Model;
using System.Data;
using System.Data.SqlClient;

namespace MarkaKod.API.Repo
{
    public interface ILocationRepo
    {
        List<Location> List();
        Location GetById(int id);
        Location Create(Location model);
        Location Update(Location model);

    }

    public class LocationRepo : ILocationRepo
    {
        private string connectionString = "Data Source=.;Initial Catalog=MarkaKod;Integrated Security=True;";
        public List<Location> List()
        {
            List<Location> locationlist = new List<Location>();

            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                locationlist = conn.Query<Location>("select * from Location").OrderByDescending(x => x.Id).ToList();

                conn.Close();
            }

            return locationlist;
        }
        public Location GetById(int id)
        {
            Location location = new Location();

            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                location = conn.Query<Location>("select * from Location where Id = " + id).Single();

                conn.Close();
            }

            return location;
        }

        public Location Create(Location model)
        {
            Location result;
            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                result = conn.Query<Location>("INSERT INTO [Location](Title,Lat,Long,CurrentLocation,Visited,KM) VALUES(@Title,@LatString,@LongString,@CurrentLocation,@Visited,@KM); SELECT * FROM [Location] WHERE Id = SCOPE_IDENTITY()", model).Single();

                conn.Close();
            }

            return result;
        }

        public Location Update(Location model)
        {
            Location result;
            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                result = conn.Query<Location>("UPDATE[Location] SET Title = @Title, Lat = @LatString, Long = @LongString, CurrentLocation = @CurrentLocation, Visited = @Visited, KM = @KM WHERE Id = @Id; SELECT * FROM [Location] WHERE Id = @Id", model).Single();

                conn.Close();
            }

            return result;
        }
    }
}
