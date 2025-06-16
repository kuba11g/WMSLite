using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMSLite.Api.Data;
using WMSLite.Api.Models;

namespace WMSLite.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentItemsController : ControllerBase
    {
        private readonly WMSLiteDbContext _context;

        public DocumentItemsController(WMSLiteDbContext context)
        {
            _context = context;
        }

        // GET: api/DocumentItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentItem>>> GetDocumentItems()
        {
            return await _context.DocumentItems.ToListAsync();
        }

        // GET: api/DocumentItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentItem>> GetDocumentItem(int id)
        {
            var documentItem = await _context.DocumentItems.FindAsync(id);

            if (documentItem == null)
            {
                return NotFound();
            }

            return documentItem;
        }

        // PUT: api/DocumentItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentItem(int id, DocumentItem documentItem)
        {
            if (id != documentItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(documentItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentItemExists(id))
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

        // POST: api/DocumentItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DocumentItem>> PostDocumentItem(DocumentItem documentItem)
        {
            _context.DocumentItems.Add(documentItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocumentItem", new { id = documentItem.Id }, documentItem);
        }

        // DELETE: api/DocumentItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentItem(int id)
        {
            var documentItem = await _context.DocumentItems.FindAsync(id);
            if (documentItem == null)
            {
                return NotFound();
            }

            _context.DocumentItems.Remove(documentItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocumentItemExists(int id)
        {
            return _context.DocumentItems.Any(e => e.Id == id);
        }
    }
}
