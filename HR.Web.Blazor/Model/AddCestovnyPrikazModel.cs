using HR.Dol;
using Microsoft.AspNetCore.Mvc;

namespace HR.Web.Blazor.Model {
    public class AddCestovnyPrikazModel {

        public DateTime DatumCasZaciatku { get; set; }
        public DateTime DatumCasKonca { get; set; }
        public Mesto? MiestoZaciatkuMesto { get; set; }
        public Mesto? MiestoKoncaMesto { get; set; }
        public Stav Stav { get; set; } = Stav.Vytvoreny;        
        public List<DopravaTyp> DopravaList { get; set; } = [];

    }
}
