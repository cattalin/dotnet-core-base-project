using Core.Dtos;
using Core.Managers;
using Infrastructure.Base;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/users")]
    public class UsersController : BaseController
    {
        private UsersManager usersManager { get; set; }

        public UsersController(
            UsersManager usersManager
        )
        {
            this.usersManager = usersManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(usersManager.GetRawList(0, 10));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(usersManager.GetRawById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserDto newUser)
        {
            var result = usersManager.CreateNew(newUser);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDto newUser)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
