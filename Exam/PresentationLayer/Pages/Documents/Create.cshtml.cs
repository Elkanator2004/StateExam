using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessLayer;
using DataLayer;

namespace PresentationLayer.Pages.Documents
{
    public class CreateModel : PageModel
    {
        private readonly DataLayer.ExamDbContext _context;

        public CreateModel(DataLayer.ExamDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ReceiverId"] = new SelectList(_context.Users, "Id", "Id");
        ViewData["SenderId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Document Document { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Documents == null || Document == null)
            {
                return Page();
            }

            _context.Documents.Add(Document);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
