using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeneByGeneAssessment.Models;

namespace GeneByGeneAssessment.Controllers
{
    [Produces("application/json")]
    [Route("api/Status")]
    public class StatusController : Controller
    {
        private readonly GBGContext _context;

        public StatusController(GBGContext context)
        {
            _context = context;
        }

        // GET: api/Status
        [HttpGet]
        public IEnumerable<Status> GetStatuses()
        {
            return _context.Statuses.OrderBy(s => s.StatusName);
        }

        // GET: api/Status/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var status = await _context.Statuses.SingleOrDefaultAsync(m => m.StatusId == id);

            if (status == null)
            {
                return NotFound();
            }

            return Ok(status);
        }       

        private bool StatusExists(int id)
        {
            return _context.Statuses.Any(e => e.StatusId == id);
        }
    }
}