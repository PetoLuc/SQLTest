using HR.Dol.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace HR.Web.ViewModels {
    
    public class DopravaTypViewModel {
        [BindProperty]
        public int DopravaTypId { get; set; }

        [BindProperty]
        public string? KodTypu { get; set; }

        [BindProperty]
        public string? NazovTypu { get; set; }

        [BindProperty]
        public bool Selected { get; set; } = false;

    }
}
