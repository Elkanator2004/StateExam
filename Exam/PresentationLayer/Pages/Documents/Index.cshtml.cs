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
    public class IndexModel : PageModel
    {
        private readonly DataLayer.ExamDbContext _context;

        public IndexModel(DataLayer.ExamDbContext context)
        {
            _context = context;
        }

        public IList<Document> Document { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Documents != null)
            {
                Document = await _context.Documents
                .Include(d => d.Receiver)
                .Include(d => d.Sender).ToListAsync();
            }
        }
    }
}
