using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCollection_0._1.Data;
using MyCollection_0._1.Model;

namespace MyCollection_0._1.Pages.Data
{
    public class FilmsModel : PageModel
    {
        private readonly MyCollection_0._1.Data.MyCollection_0_1Context _context;

        public FilmsModel(MyCollection_0._1.Data.MyCollection_0_1Context context)
        {
            _context = context;
        }

        public IList<Item> Items { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // R�cup�rer tous les films (items o� Type = "Film")
            Items = await _context.Item
                .Where(i => i.Type == "Film")
                .ToListAsync();
        }
    }
}
