using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection_0._1.Data;
using MyCollection_0._1.Model;

namespace MyCollection_0._1.Pages.Data
{
    public class IndexModel : PageModel
    {
        private readonly MyCollection_0_1Context _context;

        public IndexModel(MyCollection_0_1Context context)
        {
            _context = context;
        }

        // Texte de recherche saisi par l'utilisateur
        [BindProperty(SupportsGet = true)]
        public string SearchTitle { get; set; } = string.Empty;

        // Liste des éléments à afficher
        public IList<Item> Items { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // Filtrer les éléments en fonction de la recherche
            IQueryable<Item> query = _context.Item;

            if (!string.IsNullOrEmpty(SearchTitle))
            {
                query = query.Where(i => i.Title.Contains(SearchTitle));
            }

            Items = await query.ToListAsync();
        }
        public async Task<IActionResult> OnPostToggleFavoriteAsync(int id)
        {
            var item = await _context.Item.FindAsync(id); // Trouver l'élément dans la base
            if (item != null)
            {
                item.IsFavorite = !item.IsFavorite; // Basculer la propriété IsFavorite
                await _context.SaveChangesAsync(); // Enregistrer les modifications
            }
            return RedirectToPage(); // Recharger la page
        }
        public async Task<IActionResult> OnPostToggleFavoriteWithIdAsync(int id)
        {
            var item = await _context.Item.FindAsync(id); // Trouver l'élément dans la base
            if (item != null)
            {
                item.IsFavorite = !item.IsFavorite; // Basculer la propriété IsFavorite
                await _context.SaveChangesAsync(); // Enregistrer les modifications
            }

            return new JsonResult(new { success = true });
        }

    }

}
