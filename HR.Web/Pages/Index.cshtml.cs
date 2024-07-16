using HR.Dal.Contracts;
using HR.Dol;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HR.Web.Pages {
    public class IndexModel(ILogger<IndexModel> logger, ICestovnyPrikazRepository cestovnyPrikazRepository) : PageModel {
        private readonly ILogger<IndexModel> _logger = logger;
        private readonly ICestovnyPrikazRepository _cestovnyPrikazRepository = cestovnyPrikazRepository;
        public List<CestovnyPrikaz> CestovnePrikazy { get; set; } = [];
        public async Task<IActionResult> OnGet() {
             CestovnePrikazy = await _cestovnyPrikazRepository.GetAsync();
            return Page();
        }
    }
}
