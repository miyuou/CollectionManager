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

        // Liste des éléments favoris
        public List<Item> Items { get; set; } = new List<Item>();

        public async Task OnGetAsync()
        {
            // Synchroniser les éléments favoris dans FavoriteItems
            await SyncFavoriteItemsAsync();

            // Charger les éléments favoris avec leurs informations détaillées
            Items = await _context.Item
                .Where(i => i.IsFavorite == true) // Filtrer les éléments favoris
                .ToListAsync();
        }

        // Méthode pour synchroniser la table FavoriteItems avec les éléments favoris
        public async Task SyncFavoriteItemsAsync()
        {
            // Récupérer tous les éléments ayant IsFavorite = true
            var favoriteItemsToAdd = await _context.Item
                .Where(i => i.IsFavorite == true) // Filtrer les éléments favoris
                .Where(i => !_context.FavoriteItems.Any(f => f.ItemId == i.Id)) // Exclure ceux déjà dans FavoriteItems
                .ToListAsync();

            // Ajouter les éléments favoris dans FavoriteItems
            foreach (var item in favoriteItemsToAdd)
            {
                _context.FavoriteItems.Add(new FavoriteItem
                {
                    ItemId = item.Id,
                    Item = item // Relier l'élément à FavoriteItem
                });
            }

            // Sauvegarder les modifications dans la base de données
            if (favoriteItemsToAdd.Count > 0)
            {
                await _context.SaveChangesAsync();
            }
        }
    }
}