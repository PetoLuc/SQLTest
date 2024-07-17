using HR.Dol.Attributes;

namespace HR.Dol {    
    public class Stav {        
        public int StavId { get; set; }
        
        public string KodStavu { get; set; }
        
        public string NazovStavu { get; set; }

        private Stav(int id, string kod, string nazov) {
            StavId = id;
            KodStavu = kod;
            NazovStavu = nazov;
        }

        public static readonly Stav Vytvoreny = new (1, "VYT", "Vytvoreny");
        public static readonly Stav Schvaleny = new (2, "SCH", "Schvaleny");
        public static readonly Stav Vyuctovany = new (3, "VYC", "Vyuctovany");
        public static readonly Stav Zruseny = new (4, "ZRU", "Zruseny");

        public static IEnumerable<Stav> GetAll() {
            yield return Vytvoreny;
            yield return Schvaleny;
            yield return Vyuctovany;
            yield return Zruseny;
        }
    }


}
