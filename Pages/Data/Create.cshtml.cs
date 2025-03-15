using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCollection_0._1.Data;
using MyCollection_0._1.Model;
using System.Threading.Tasks;

namespace MyCollection_0._1.Pages.Data
{
    public class CreateModel : PageModel
    {
        private readonly MyCollection_0_1Context _context;

        // Constructeur pour injecter le contexte de la base de données
        public CreateModel(MyCollection_0_1Context context)
        {
            _context = context;
        }

       
        public IActionResult OnGet()
        {
            return Page();
        }

        // Propriété liée au formulaire
        [BindProperty]
        public Item Item { get; set; } = new Item();

        // Méthode appelée lors de la soumission du formulaire
        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            
            _context.Item.Add(Item);

            await _context.SaveChangesAsync();

          
            return RedirectToPage("./Index");
        }
    }
}
