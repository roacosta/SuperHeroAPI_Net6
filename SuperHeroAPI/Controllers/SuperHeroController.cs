using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {


        //private static List<SuperHero> heroes = new List<SuperHero>
        //{
        //    new SuperHero{
        //        Id = 1,
        //        Name = "Spider Mano",
        //        FirstName = "Peter",
        //        LastName = "Parker",
        //        Place = "New York City"
        //    },
        //    new SuperHero{
        //        Id = 2,
        //        Name = "Ironman",
        //        FirstName = "Tony",
        //        LastName = "Stark",
        //        Place = "Long Island"
        //    }
        //};


        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero == null)
            {
                return BadRequest("The specifc id cant be found.");
            }
            return Ok(dbHero);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero == null)
                return BadRequest("The specified id cant be found.");
            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null)
            {
                return BadRequest("The specifc id cant be found.");
            }
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Name = request.Name;
            dbHero.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        private readonly DataContext _context;
        public SuperHeroController(DataContext context)
        {
            _context = context;
        }
    }
}
