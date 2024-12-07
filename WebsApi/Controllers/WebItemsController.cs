using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using WebsApi.Context;
using WebsApi.Models;

namespace WebsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WebItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/WebItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WebItem>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }

        // GET: api/WebItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WebItem>> GetWebItem(long id)
        {
            var webItem = await _context.Items.FindAsync(id);

            if (webItem == null)
            {
                return NotFound();
            }

            return webItem;
        }

        // PUT: api/WebItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWebItem(long id, WebItem webItem)
        {
            if (id != webItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(webItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebItemExists(id))
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

        // POST: api/WebItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WebItem>> PostWebItem(WebItem webItem)
        {
            _context.Items.Add(webItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetWebItem", new { id = webItem.Id }, webItem);
            return CreatedAtAction(nameof(GetWebItem), new { id = webItem.Id }, webItem);
        }

        // DELETE: api/WebItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWebItem(long id)
        {
            var webItem = await _context.Items.FindAsync(id);
            if (webItem == null)
            {
                return NotFound();
            }

            _context.Items.Remove(webItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WebItemExists(long id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
