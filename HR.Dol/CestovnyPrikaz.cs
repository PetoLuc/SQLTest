using HR.Dol.Contracts;

namespace HR.Dol {
    
    public class CestovnyPrikaz: IEntityMarker
    {
        public int CpId { get; set; }
        
        public DateTime DatumVytvorenia { get; set; }

        //[ColumnName("ucastnik")]
        //public required Zamestnanec? Ucastnik { get; set; }
        
        public required string UcastnikId { get; set; }
        
        public int MiestoZaciatkuId { get; set; }
        
        public int MiestoKoncaId { get; set; }
        
        public DateTime DatumCasZaciatku { get; set; }

        public DateTime DatumCasKonca { get; set; }
        
        public int StavId { get; set; }
        
        public Zamestnanec? Ucastnik { get; set; } 
        public Mesto? MiestoZaciatkuMesto { get; set; }
        public Mesto? MiestoKoncaMesto { get; set; }
        public Stav Stav { get; set; } = Stav.Vytvoreny;
        public List<Doprava> DopravaList { get; set; } = [];
    }
}
