using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WSConvertisseur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevisesController : ControllerBase
    {
        private static List<Devise> devises = new List<Devise>
        {
            new Devise(1, "Dollar", 1.08),
            new Devise(2, "Franc Suisse", 1.07),
            new Devise(3, "Yen", 120)
        };

        [HttpGet]
        public IEnumerable<Devise> GetAll() => devises;

        [HttpGet("{id}", Name = "GetDevise")]
        public ActionResult<Devise> GetById(int id)
        {
            var devise = devises.FirstOrDefault(d => d.Id == id);
            if (devise == null) return NotFound();
            return devise;
        }

        [HttpPost]
        public ActionResult<Devise> Post([FromBody] Devise devise)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            devises.Add(devise);
            return CreatedAtRoute("GetDevise", new { id = devise.Id }, devise);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Devise devise)
        {
            if (!ModelState.IsValid || id != devise.Id) return BadRequest();
            var index = devises.FindIndex(d => d.Id == id);
            if (index < 0) return NotFound();
            devises[index] = devise;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var devise = devises.FirstOrDefault(d => d.Id == id);
            if (devise == null) return NotFound();
            devises.Remove(devise);
            return NoContent();
        }
    }
}
