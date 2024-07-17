using HR.Dal.Repos.Contracts;
using HR.Dol;
using HR.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HR.Web.Pages
{
    public class IndexModel(ILogger<IndexModel> logger, ICestovnyPrikazRepository cestovnyPrikazRepository) : PageModel {
        private readonly ILogger<IndexModel> _logger = logger;
        private readonly ICestovnyPrikazRepository _cestovnyPrikazRepository = cestovnyPrikazRepository;
        public List<CestovnyPrikaz> CestovnePrikazy { get; set; } = [];
        //public List<int> SelectedDopravaTypIds { get; set; } = new List<int>();
 
        [BindProperty]
        public AddCestovnyPrikazViewModel AddCestovnyVykazRequest { get; set; } = new AddCestovnyPrikazViewModel {
            DatumCasZaciatku = DateTime.Now,
            DatumCasKonca = DateTime.Now.AddDays(1),
            DopravaList = DopravaTyp.GetAll().Select(d => new DopravaTypViewModel {
                DopravaTypId = d.DopravaTypId,
                KodTypu = d.KodTypu,
                NazovTypu = d.NazovTypu,
                Selected = true
            }).ToList()
        };

        public async Task<IActionResult> OnGet() {
            CestovnePrikazy = await _cestovnyPrikazRepository.GetAsync();
            return Page();
        }
        public async Task<IActionResult> OnPost() {
            var sss = AddCestovnyVykazRequest;
            throw new NotImplementedException();
        }
    }
}
