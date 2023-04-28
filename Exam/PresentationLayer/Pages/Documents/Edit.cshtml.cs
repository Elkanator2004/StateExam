using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using DataLayer;

namespace PresentationLayer.Pages.Documents
{
    public class EditModel : PageModel
    {
        private readonly DataLayer.ExamDbContext _context;

        public EditModel(DataLayer.ExamDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Document Document { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Documents == null)
            {
                return NotFound();
            }

            var document =  await _context.Documents.FirstOrDefaultAsync(m => m.EntryNumber == id);
            if (document == null)
            {
                return NotFound();
            }
            Document = document;
           ViewData["ReceiverId"] = new SelectList(_context.Users, "Id", "Id");
           ViewData["SenderId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Document).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentExists(Document.EntryNumber))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DocumentExists(string id)
        {
          return (_context.Documents?.Any(e => e.EntryNumber == id)).GetValueOrDefault();
        }
    }
}
