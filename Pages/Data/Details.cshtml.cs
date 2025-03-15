using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection_0._1.Data;
using MyCollection_0._1.Model;

namespace MyCollection_0._1.Pages.Data
{
    public class DetailsModel : PageModel
    {
        private readonly MyCollection_0._1.Data.MyCollection_0_1Context _context;

        public DetailsModel(MyCollection_0._1.Data.MyCollection_0_1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Item Item { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            Item = item;
            return Page();
        }

        // Ajoute cette méthode pour gérer la soumission de la note personnelle
        public async Task<IActionResult> OnPostAsync(int? id, string? personalNote)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemToUpdate = await _context.Item.FirstOrDefaultAsync(m => m.Id == id);
            if (itemToUpdate == null)
            {
                return NotFound();
            }

            // Mettre à jour la note personnelle de l'élément
            itemToUpdate.PersonalNote = personalNote;

            // Sauvegarder les modifications dans la base de données
            await _context.SaveChangesAsync();

            // Rediriger vers la page de détails avec un message de confirmation
            TempData["Message"] = "Your personal note has been saved!";

            return RedirectToPage(new { id = id });
        }
    }
}
