using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIcheck.Data;

namespace WebAPIcheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonTimesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public LessonTimesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/LessonTimes
        [HttpGet]
        public async Task<IActionResult> GetLessonTime()
        {
          if (_context.LessonTime == null)
          {
              return NotFound();
          }
            return Ok(_context.LessonTime.ToArray());
        }

        // GET: api/LessonTimes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LessonTime>> GetLessonTime(int id)
        {
          if (_context.LessonTime == null)
          {
              return NotFound();
          }
            var lessonTime = await _context.LessonTime.FindAsync(id);

            if (lessonTime == null)
            {
                return NotFound();
            }

            return lessonTime;
        }

        // PUT: api/LessonTimes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLessonTime(int id, LessonTime lessonTime)
        {
            if (id != lessonTime.IdTime)
            {
                return BadRequest();
            }
            string[] StatrTime = lessonTime.EndTime.Split((":"));
            if (StatrTime.Length < 2)
            {
                return Ok(value: "0");
            }
            if (!StatrTime[0].All(char.IsDigit) || !StatrTime[1].All(char.IsDigit))
            {
                return Ok(value: "1");
            }
            if (int.Parse(StatrTime[0]) > 23 || int.Parse(StatrTime[1]) > 59)
            {
                return Ok(value: "2");
            }
            string[] EndTime = lessonTime.EndTime.Split((":"));
            if (EndTime.Length < 2)
            {
                return Ok(value: "0");
            }
            if (!EndTime[0].All(char.IsDigit) || !EndTime[1].All(char.IsDigit))
            {
                return Ok(value: "1");
            }
            if (int.Parse(EndTime[0]) > 23 || int.Parse(EndTime[1]) > 59)
            {
                return Ok(value: "2");
            }
            _context.Entry(lessonTime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LessonTimeExists(id))
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

        // POST: api/LessonTimes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LessonTime>> PostLessonTime(LessonTime lessonTime)
        {
          if (_context.LessonTime == null)
          {
              return Problem("Entity set 'MyDbContext.LessonTime'  is null.");
          }
            string[] StatrTime = lessonTime.EndTime.Split((":"));
            if (StatrTime.Length < 2)
            {
                return Ok(value: "0");
            }
            if (!StatrTime[0].All(char.IsDigit) || !StatrTime[1].All(char.IsDigit))
            {
                return Ok(value: "1");
            }
            if (int.Parse(StatrTime[0]) > 23 || int.Parse(StatrTime[1]) > 59)
            {
                return Ok(value: "2");
            }
            string[] EndTime = lessonTime.EndTime.Split((":"));
            if (EndTime.Length < 2)
            {
                return Ok(value: "0");
            }
            if (!EndTime[0].All(char.IsDigit) || !EndTime[1].All(char.IsDigit))
            {
                return Ok(value:"1");
            }
            if (int.Parse(EndTime[0]) > 23 || int.Parse(EndTime[1]) > 59)
            {
                return Ok(value: "2");
            }
            _context.LessonTime.Add(lessonTime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLessonTime", new { id = lessonTime.IdTime }, lessonTime);
        }

        // DELETE: api/LessonTimes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLessonTime(int id)
        {
            if (_context.LessonTime == null)
            {
                return NotFound();
            }
            var lessonTime = await _context.LessonTime.FindAsync(id);
            if (lessonTime == null)
            {
                return NotFound();
            }

            _context.LessonTime.Remove(lessonTime);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LessonTimeExists(int id)
        {
            return (_context.LessonTime?.Any(e => e.IdTime == id)).GetValueOrDefault();
        }
    }
}
