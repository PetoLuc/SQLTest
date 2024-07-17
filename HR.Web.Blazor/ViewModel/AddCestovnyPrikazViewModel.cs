using HR.Dal.Repos.Contracts;
using HR.Dol;
using HR.Web.Blazor.Model;
using Microsoft.AspNetCore.Mvc;

namespace HR.Web.Blazor.ViewModel
{
    public class AddCestovnyPrikazViewModel
    {
        [BindProperty]
        public DateTime DatumCasZaciatku { get; set; }

        [BindProperty]
        public DateTime DatumCasKonca { get; set; }

        [BindProperty]
        public int? MiestoZaciatkuMestoId { get; set; }

        [BindProperty]
        public int? MiestoKoncaMestoId { get; set; }

        [BindProperty]
        public int StavId { get; set; } = Stav.Vytvoreny.StavId;

        [BindProperty]
        public List<DopravaTypModel> DopravaList { get; set; }


        private readonly IDopravaRepository _dopravaRepository;

        public AddCestovnyPrikazViewModel(IDopravaRepository dopravaRepository)
        {
            _dopravaRepository = dopravaRepository;

            DopravaList = _dopravaRepository.GetAll().Select(d => 
            new DopravaTypModel
            {
                DopravaTypId = d.DopravaTypId,
                IsSelected = false,
                KodTypu = d.KodTypu,
                NazovTypu = d.NazovTypu
            }).ToList();
        }
    }
}
