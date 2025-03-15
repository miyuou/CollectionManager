using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection_0._1.Data;
using MyCollection_0._1.Model;

namespace MyCollection_0._1.Pages
{
    public class FavoritesModel : PageModel
    {
        private readonly MyCollection_0_1Context _context;

        public FavoritesModel(MyCollection_0_1Context context)
        {
            _context = context;
        }

        // Liste des �l�ments favoris
        public List<Item> Items { get; set; } = new List<Item>();

        public async Task OnGetAsync()
        {
            // Synchroniser les �l�ments favoris dans FavoriteItems
            await SyncFavoriteItemsAsync();

            // Charger les �l�ments favoris avec leurs informations d�taill�es
            Items = await _context.Item
                .Where(i => i.IsFavorite == true) // Filtrer les �l�ments favoris
                .ToListAsync();
        }

        // M�thode pour synchroniser la table FavoriteItems avec les �l�ments favoris
        public async Task SyncFavoriteItemsAsync()
        {
            // R�cup�rer tous les �l�ments ayant IsFavorite = true
            var favoriteItemsToAdd = await _context.Item
                .Where(i => i.IsFavorite == true) // Filtrer les �l�ments favoris
                .Where(i => !_context.FavoriteItems.Any(f => f.ItemId == i.Id)) // Exclure ceux d�j� dans FavoriteItems
                .ToListAsync();

            // Ajouter les �l�ments favoris dans FavoriteItems
            foreach (var item in favoriteItemsToAdd)
            {
                _context.FavoriteItems.Add(new FavoriteItem
                {
                    ItemId = item.Id,
                    Item = item // Relier l'�l�ment � FavoriteItem
                });
            }

            // Sauvegarder les modifications dans la base de donn�es
            if (favoriteItemsToAdd.Count > 0)
            {
                await _context.SaveChangesAsync();
            }
        }
    }
}