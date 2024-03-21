using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroApi.DotNet8.Data;
using SuperHeroApi.DotNet8.Entities;
using System.Collections.Specialized;

/*namespace superheroapi.dotnet8.controllers
{
    [route("api/[controller]")]
    [apicontroller]

    public class superherocontroller : controllerbase
    {
        [httpget]
        //public async task<iactionresult> getallheros()
        public async task<actionresult<list<superhero>>> getallheros()
        {
            var heroes = new list<superhero>
            {
                new superhero
                {
                    id=1,
                    name="spiderman",
                    firstname="peter",
                    lastname="parker",
                    place="new york city"
                }
            };
            return ok(heroes);
        }
    }
}*/
namespace SuperHeroApi.DotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        //initalize DataContext
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeros()
        {
            var heroes = await _context.SuperHeroes.ToListAsync();
            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> GetHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return NotFound("Hero not found");

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero updatedHero)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(updatedHero.Id);
            if (dbHero == null)
                return NotFound("Hero not found");

            dbHero.Name = updatedHero.Name;
            dbHero.FirstName = updatedHero.FirstName;
            dbHero.LastName = updatedHero.LastName;
            dbHero.Place = updatedHero.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete]

        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero == null)
                return NotFound("Hero not found");

            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
