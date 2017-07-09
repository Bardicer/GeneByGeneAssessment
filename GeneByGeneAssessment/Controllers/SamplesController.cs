using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeneByGeneAssessment.Models;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json.Linq;

namespace GeneByGeneAssessment.Controllers
{
    [Produces("application/json")]
    [Route("api/Samples")]
    public class SamplesController : Controller
    {
        private readonly GBGContext _context;

        public SamplesController(GBGContext context)
        {
            _context = context;
        }

        // POST: api/Samples
        [Route("GetSamples")]
        [HttpPost]
        public IEnumerable<Sample> GetSamples([FromBody] SampleSearch search)
        {
            try
            {
                return _context.Samples.Include("Creator").Include("Status")
                    .Where(s => 
                    (string.IsNullOrEmpty(search.Query) || (!string.IsNullOrEmpty(search.Query) 
                        && (s.Creator.FirstName.ToLowerInvariant().Contains(search.Query) || s.Creator.LastName.ToLowerInvariant().Contains(search.Query))))
                    //(!search.UserId.HasValue || (search.UserId.HasValue && s.CreatedBy == search.UserId))
                    && (!search.StatusId.HasValue || (search.StatusId.HasValue && s.StatusId == search.StatusId.Value)));                
            }
            catch (Exception ex)
            {
                return new List<Sample>();
            }            
        }

        // GET: api/Samples/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSample([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sample = await _context.Samples.Include(s => s.Status).Include(s => s.Creator).SingleOrDefaultAsync(m => m.SampleId == id);

            if (sample == null)
            {
                return NotFound();
            }

            return Ok(sample);
        }

        // PUT: api/Samples/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSample([FromRoute] int id, [FromBody] Sample sample)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sample.SampleId)
            {
                return BadRequest();
            }

            _context.Entry(sample).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SampleExists(id))
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

        // POST: api/Samples
        [Route("InsertSample")]
        [HttpPost]
        public async Task<IActionResult> InsertSample([FromBody] Sample sample)
        {            
            sample.CreatedAt = DateTime.Now;
            sample.SampleId = _context.Samples.OrderByDescending(s => s.SampleId).FirstOrDefault().SampleId + 1;
            _context.Samples.Add(sample);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SampleExists(sample.SampleId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSample", new { id = sample.SampleId }, sample);
        }

        private bool SampleExists(int id)
        {
            return _context.Samples.Any(e => e.SampleId == id);
        }
    }
}