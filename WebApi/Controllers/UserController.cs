using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("users")]
    public class UsersController : ApiController
    {
        [Route]
        public IHttpActionResult Get()
        {
            return Ok(Database.Users);
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            User user = Database.Users.FirstOrDefault(i => i.Id == id);
            if (user == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(user);
            }
        }

        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            User user = Database.Users.FirstOrDefault(i => i.Name.ToLower() == name.ToLower());
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        [Route]
        public IHttpActionResult Post(User user)
        {
            Database.Users.Add(user);
            return Created(this.Request.RequestUri.AbsolutePath + "/" + user.Id, user);
        }

        [Route("{id:int}")]
        public IHttpActionResult Put(int id, User user)
        {
            User existing = Database.Users.FirstOrDefault(i => i.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            else
            {
                existing.Name = user.Name;
                return Ok(existing);
            }
        }

        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            User existing = Database.Users.FirstOrDefault(i => i.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            else
            {
                Database.Users.Remove(existing);
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
    }

    public static class Database
    {
        public static IList<User> Users = new List<User>()
        {
            new User("Mable Failla"),
            new User("Hailey Ruehl"),
            new User("Kennith Marengo"),
            new User("Ayana Bill"),
            new User("Esta Luzier"),
            new User("Arline Leaks"),
            new User("Jacquetta Leveille"),
            new User("Russ Reilley"),
            new User("Arnoldo Shaikh"),
            new User("Terrie Bashaw"),
            new User("Gena Faulkner"),
            new User("Charmaine Marasco"),
            new User("Anglea Miron"),
            new User("Elenor Perales"),
            new User("Emanuel Edlund"),
            new User("Elizbeth Mackie"),
            new User("Tisa Fujimoto"),
            new User("Elayne Mcmillen"),
            new User("Neta Olivo"),
            new User("Kari Williams")
        };
    }

    public class User
    {
        private static int IdCount = 0;

        public User(string name)
        {
            this.Id = ++IdCount;
            this.Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; set; }
    }
}
