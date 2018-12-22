using Microsoft.AspNetCore.Mvc;
using RestAspNetRestAspNet.Model;
using RestAspNetRestAspNet.Services;
namespace RestAspNetRestAspNet.Controllers
{
    /* Mapeia as requisições de http://localhost:{porta}/api/person/
    Por padrão o ASP.NET Core mapeia todas as classes que extendem Controller
    pegando a primeira parte do nome da classe em lower case [Person]Controller
    e expõe como endpoint REST
    */
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        //Declaração do serviço usado
        private IPersonService _personService;

        /* Injeção de uma instancia de IPersonService ao criar
        uma instancia de PersonController */
        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        //Mapeia as requisições GET para http://localhost:{porta}/api/person/
        //Get sem parâmetros para o FindAll --> Busca Todos
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        //Mapeia as requisições GET para http://localhost:{porta}/api/person/{id}
        //recebendo um ID como no Path da requisição
        //Get com parâmetros para o FindById --> Busca Por ID
        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var person = _personService.FindById(id);
            if (person == null) return NotFound();

            return Ok(person);
        }

        //Mapeia as requisições POST para http://localhost:{porta}/api/person/
        //O [FromBody] consome o Objeto JSON enviado no corpo da requisição
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            return new ObjectResult(_personService.Create(person));
        }

        //Mapeia as requisições PUT para http://localhost:{porta}/api/person/
        //O [FromBody] consome o Objeto JSON enviado no corpo da requisição
        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Person person)
        {
            if (person == null) return BadRequest();
            return new ObjectResult(_personService.Update(person));
        }

        //Mapeia as requisições DELETE para http://localhost:{porta}/api/person/{id}
        //recebendo um ID como no Path da requisição
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}
