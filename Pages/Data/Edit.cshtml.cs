using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection_0._1.Data;
using MyCollection_0._1.Model;

namespace MyCollection_0._1.Pages.Data
{
    public class EditModel : PageModel
    {
        private readonly MyCollection_0_1Context _context;

        public EditModel(MyCollection_0_1Context context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var itemToUpdate = await _context.Item.FirstOrDefaultAsync(m => m.Id == Item.Id);
            if (itemToUpdate == null)
            {
                return NotFound();
            }

            // Enregistrer l'action de modification dans l'historique
            await LogHistoryAsync("Update", itemToUpdate.Title);

            itemToUpdate.Title = Item.Title;
            itemToUpdate.Genre = Item.Genre;
            itemToUpdate.Link = Item.Link;
            itemToUpdate.ImageUrl = Item.ImageUrl;
            itemToUpdate.IsFavorite = Item.IsFavorite;
            itemToUpdate.PersonalNote = Item.PersonalNote;
            itemToUpdate.Type = Item.Type;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(Item.Id))
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

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.Id == id);
        }

        // Méthode pour enregistrer une action dans l'historique
        private async Task LogHistoryAsync(string action, string itemName)
        {
            var history = new History
            {
                Action = action,
                ItemName = itemName,
                Timestamp = DateTime.Now
            };

            _context.Histories.Add(history);
            await _context.SaveChangesAsync();
        }
    }
}
