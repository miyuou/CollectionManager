using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection_0._1.Data;
using MyCollection_0._1.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection_0._1.Pages
{
    public class HistoryModel : PageModel
    {
        private readonly MyCollection_0_1Context _context;

        public HistoryModel(MyCollection_0_1Context context)
        {
            _context = context;
        }

        public List<History> Histories { get; set; } = new List<History>();

        public async Task OnGetAsync()
        {
            Histories = await _context.Histories
                .OrderByDescending(h => h.Timestamp)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostRestoreAsync(int id)
        {
            var histories = await _context.Histories.FindAsync(id);

            if (histories == null || string.IsNullOrEmpty(histories.ItemName))
            {
                ModelState.AddModelError(string.Empty, "Élément introuvable ou données manquantes dans l'historique.");
                return Page();
            }

            if (!IsValidJson(histories.ItemName))
            {
                ModelState.AddModelError(string.Empty, "Les données stockées ne sont pas au format JSON valide.");
                return Page();
            }

            try
            {
                var restoredItemDetails = JsonConvert.DeserializeObject<Item>(histories.ItemName);

                if (restoredItemDetails == null)
                {
                    ModelState.AddModelError(string.Empty, "Impossible de restaurer les données. Format inattendu.");
                    return Page();
                }

                var restoredItem = new Item
                {
                    Title = restoredItemDetails.Title,
                    Genre = restoredItemDetails.Genre,
                    Link = restoredItemDetails.Link,
                    IsFavorite = restoredItemDetails.IsFavorite,
                    ImageUrl = restoredItemDetails.ImageUrl,
                    Type = restoredItemDetails.Type
                };

                _context.Item.Add(restoredItem);
                await _context.SaveChangesAsync();
            }
            catch (JsonException ex)
            {
                ModelState.AddModelError(string.Empty, $"Erreur lors de la restauration des données : {ex.Message}");
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Une erreur inattendue est survenue.");
                return Page();
            }

            return RedirectToPage("/History");
        }

        public async Task<IActionResult> OnPostClearHistoryAsync()
        {
            try
            {
                // Supprimer toutes les entrées de l'historique
                _context.Histories.RemoveRange(_context.Histories);

                // Sauvegarde dans la base de données
                await _context.SaveChangesAsync();
                ModelState.AddModelError(string.Empty, "L'historique a été vidé avec succès.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erreur lors de la suppression de l'historique : {ex.Message}");
            }

            return RedirectToPage("/History");
        }

        private bool IsValidJson(string input)
        {
            input = input?.Trim();
            if (string.IsNullOrWhiteSpace(input)) return false;

            if ((input.StartsWith("{") && input.EndsWith("}")) ||
                (input.StartsWith("[") && input.EndsWith("]")))
            {
                try
                {
                    var obj = JToken.Parse(input);
                    return true;
                }
                catch (JsonReaderException)
                {
                    return false;
                }
            }

            return false;
        }
    }
}