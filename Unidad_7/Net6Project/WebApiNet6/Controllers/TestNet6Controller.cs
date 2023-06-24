using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiNet6.Models;

namespace WebApiNet6.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestNet6Controller : ControllerBase
    {
        private readonly TaskAssignerDbContext _taskAssignerDbContext;
        public TestNet6Controller(TaskAssignerDbContext assigner)
        {
            _taskAssignerDbContext = assigner;
        }

        [HttpPost(Name = "InsertIntoDb")]
        public IActionResult actionResult()
        {

            _taskAssignerDbContext.Itworkers.Add(

                new Itworkers
                {
                    Name = "Juanito",
                    Surname = "Pruebas"
                }

            );

            _taskAssignerDbContext.SaveChanges();

            return Ok("It works");
        }

    }
}
