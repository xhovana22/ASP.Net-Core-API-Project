﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamuraiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamuraisController : ControllerBase
    {
        private readonly SamuraiContext _context;

        public SamuraisController(SamuraiContext context)
        {
            _context = context;
        }

        // GET: api/Samurais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Samurai>>> GetSamurais()
        {
            return await _context.Samurais.ToListAsync();
        }

        // GET: api/Samurais/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Samurai>> GetSamurai(int id)
        {
            var samurai = await _context.Samurais.FindAsync(id);

            if (samurai == null)
            {
                return NotFound();
            }

            return samurai;
        }

        // PUT: api/Samurais/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSamurai(int id, Samurai samurai)
        {
            if (id != samurai.Id)
            {
                return BadRequest();
            }

            _context.Entry(samurai).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SamuraiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Samurais
        
        [HttpPost]
        public async Task<ActionResult<Samurai>> PostSamurai(Samurai samurai)
        {
            _context.Samurais.Add(samurai);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSamurai", new { id = samurai.Id }, samurai);
        }

        // DELETE: api/Samurais/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Samurai>> DeleteSamurai(int id)
        {
            var samurai = await _context.Samurais.FindAsync(id);
            if (samurai == null)
            {
                return NotFound();
            }
            _context.Samurais.Remove(samurai);
            await _context.SaveChangesAsync();
            return samurai;
        }
        [HttpDelete("sproc/{id}")]
        public async Task<ActionResult<string>> DeleteQuotesForSamurai(int id)
        {
            var rowsAffected = await _context.Database
                              .ExecuteSqlInterpolatedAsync($"EXEC DeleteQuotesForSamurai {id}");
            return $"{rowsAffected} Quotes deleted";
        }

        private bool SamuraiExists(int id)
        {
            return _context.Samurais.Any(e => e.Id == id);
        }
    }
}