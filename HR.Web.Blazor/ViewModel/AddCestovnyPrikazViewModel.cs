using HR.Dal.Repos.Contracts;
using HR.Dol;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public List<DopravaTyp> DopravaList { get; set; } = [];

        public List<DopravaTyp> DopravaTypes { get; set; }

        [BindProperty]
        public int SelectedDopravaId { get; set; } = DopravaTyp.SluzobneAuto.DopravaTypId;


        private readonly IDopravaRepository _dopravaRepository;

        public AddCestovnyPrikazViewModel(IDopravaRepository dopravaRepository)
        {
            _dopravaRepository = dopravaRepository;
            DopravaTypes = [.. _dopravaRepository.GetAll()]; 
            
        }

        public void AddDoprava()
        {
            var dopravaType = DopravaTypes.FirstOrDefault(td => td.DopravaTypId == SelectedDopravaId);
            if (dopravaType != null)
            {
                DopravaList.Add(dopravaType);
            }
        }

        public void RemoveDoprava()
        {
            if (DopravaList.Count > 0)
            {
                DopravaList.RemoveAt(DopravaList.Count - 1);
            }            
        }

    }
}
