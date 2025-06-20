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
    public class GoodsReceiptDocumentsController : ControllerBase
    {
        private readonly WMSLiteDbContext _context;

        public GoodsReceiptDocumentsController(WMSLiteDbContext context)
        {
            _context = context;
        }

        // GET: api/GoodsReceiptDocuments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GoodsReceiptDocument>>> GetGoodsReceiptDocuments()
        {
            return await _context.GoodsReceiptDocuments.ToListAsync();
        }

        // GET: api/GoodsReceiptDocuments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GoodsReceiptDocument>> GetGoodsReceiptDocument(int id)
        {
            var goodsReceiptDocument = await _context.GoodsReceiptDocuments
                .Include(d => d.DocumentItems)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (goodsReceiptDocument == null)
            {
                return NotFound();
            }

            return goodsReceiptDocument;
        }

        // PUT: api/GoodsReceiptDocuments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGoodsReceiptDocument(int id, GoodsReceiptDocument goodsReceiptDocument)
        {
            if (id != goodsReceiptDocument.Id)
            {
                return BadRequest();
            }

            _context.Entry(goodsReceiptDocument).State = EntityState.Modified;

            foreach (var item in goodsReceiptDocument.DocumentItems)
            {
                _context.Entry(item).State = EntityState.Unchanged;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodsReceiptDocumentExists(id))
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

        // POST: api/GoodsReceiptDocuments
        [HttpPost]
        public async Task<ActionResult<GoodsReceiptDocument>> PostGoodsReceiptDocument(GoodsReceiptDocument goodsReceiptDocument)
        {
            _context.GoodsReceiptDocuments.Add(goodsReceiptDocument);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGoodsReceiptDocument", new { id = goodsReceiptDocument.Id }, goodsReceiptDocument);
        }

        // DELETE: api/GoodsReceiptDocuments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoodsReceiptDocument(int id)
        {
            var goodsReceiptDocument = await _context.GoodsReceiptDocuments.FindAsync(id);
            if (goodsReceiptDocument == null)
            {
                return NotFound();
            }

            _context.GoodsReceiptDocuments.Remove(goodsReceiptDocument);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GoodsReceiptDocumentExists(int id)
        {
            return _context.GoodsReceiptDocuments.Any(e => e.Id == id);
        }
    }
}