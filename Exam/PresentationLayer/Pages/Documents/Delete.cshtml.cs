using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using DataLayer;

namespace PresentationLayer.Pages.Documents
{
    public class DeleteModel : PageModel
    {
        private readonly DataLayer.ExamDbContext _context;

        public DeleteModel(DataLayer.ExamDbContext context)
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

            var document = await _context.Documents.FirstOrDefaultAsync(m => m.EntryNumber == id);

            if (document == null)
            {
                return NotFound();
            }
            else 
            {
                Document = document;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.Documents == null)
            {
                return NotFound();
            }
            var document = await _context.Documents.FindAsync(id);

            if (document != null)
            {
                Document = document;
                _context.Documents.Remove(Document);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
