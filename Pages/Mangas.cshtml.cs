using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection_0._1.Data;
using MyCollection_0._1.Model;

namespace MyCollection_0._1.Pages.Data
{
    public class MangasModel : PageModel
    {
        private readonly MyCollection_0._1.Data.MyCollection_0_1Context _context;

        public MangasModel(MyCollection_0._1.Data.MyCollection_0_1Context context)
        {
            _context = context;
        }

        public IList<Item> Items { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // Récupérer tous les mangas (items où Type = "Manga")
            Items = await _context.Item
                .Where(i => i.Type == "Manga")
                .ToListAsync();
        }
    }
}
