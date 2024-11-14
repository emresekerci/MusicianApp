using Microsoft.AspNetCore.Mvc;
using MusicianApp.Models;
using System.Collections.Generic;
using System.Linq;
using MusicianApp.Models;

namespace CrazyMusicians.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusiciansController : ControllerBase
    {
        private static List<Musician> _musicians = new List<Musician>
        {
            new Musician { Id = 1, Name = "Ahmet Çalgı", Profession = "Ünlü Çalgı Çalar", FunFact = "Her zaman yanlış nota çalar, ama çok eğlenceli" },
            new Musician { Id = 2, Name = "Zeynep Melodi", Profession = "Popüler Melodi Yazarı", FunFact = "Şarkıları yanlış anlaşılır ama çok popüler" },
            new Musician { Id = 3, Name = "Cemil Akor", Profession = "Çılgın Akorist", FunFact = "Akorları sık değiştirir, ama şaşırtıcı derecede yetenekli" },
            // Diğer müzisyenleri de buraya ekleyin.
        };

        // GET: api/musicians
        [HttpGet]
        public ActionResult<IEnumerable<Musician>> GetAllMusicians()
        {
            return Ok(_musicians);
        }

        // GET: api/musicians/1
        [HttpGet("{id}")]
        public ActionResult<Musician> GetMusicianById(int id)
        {
            var musician = _musicians.FirstOrDefault(m => m.Id == id);
            if (musician == null)
            {
                return NotFound();
            }
            return Ok(musician);
        }

        // POST: api/musicians
        [HttpPost]
        public ActionResult<Musician> CreateMusician(Musician musician)
        {
            musician.Id = _musicians.Max(m => m.Id) + 1;
            _musicians.Add(musician);
            return CreatedAtAction(nameof(GetMusicianById), new { id = musician.Id }, musician);
        }

        // PUT: api/musicians/1
        [HttpPut("{id}")]
        public IActionResult UpdateMusician(int id, Musician musician)
        {
            var existingMusician = _musicians.FirstOrDefault(m => m.Id == id);
            if (existingMusician == null)
            {
                return NotFound();
            }

            existingMusician.Name = musician.Name;
            existingMusician.Profession = musician.Profession;
            existingMusician.FunFact = musician.FunFact;

            return NoContent();
        }

        // PATCH: api/musicians/1
        [HttpPatch("{id}")]
        public IActionResult PartialUpdateMusician(int id, [FromBody] string funFact)
        {
            var musician = _musicians.FirstOrDefault(m => m.Id == id);
            if (musician == null)
            {
                return NotFound();
            }

            musician.FunFact = funFact;
            return NoContent();
        }

        // DELETE: api/musicians/1
        [HttpDelete("{id}")]
        public IActionResult DeleteMusician(int id)
        {
            var musician = _musicians.FirstOrDefault(m => m.Id == id);
            if (musician == null)
            {
                return NotFound();
            }

            _musicians.Remove(musician);
            return NoContent();
        }

        // GET: api/musicians/search?name=Zeynep
        [HttpGet("search")]
        public ActionResult<IEnumerable<Musician>> SearchMusicians([FromQuery] string name)
        {
            var musicians = _musicians.Where(m => m.Name.Contains(name)).ToList();
            if (!musicians.Any())
            {
                return NotFound();
            }
            return Ok(musicians);
        }
    }
}
