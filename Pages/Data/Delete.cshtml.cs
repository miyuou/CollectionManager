using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection_0._1.Data;
using MyCollection_0._1.Model;
using Newtonsoft.Json;

namespace MyCollection_0._1.Pages.Data
{
    public class DeleteModel : PageModel
    {
        private readonly MyCollection_0_1Context _context;

        public DeleteModel(MyCollection_0_1Context context)
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
            else
            {
                Item = item;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item != null)
            {
                // Créer une représentation JSON des détails de l'élément
                var previousState = JsonConvert.SerializeObject(new
                {
                    Title = item.Title,
                    Genre = item.Genre,
                    Link = item.Link,
                    IsFavorite = item.IsFavorite,
                    ImageUrl = item.ImageUrl,
                    Type = item.Type
                });

                // Enregistrer l'action de suppression dans l'historique
                var history = new History
                {
                    Action = "Delete",
                    ItemName = previousState,
                    Timestamp = DateTime.Now
                };

                _context.Histories.Add(history);
                _context.Item.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
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
