using HR.Dol.Attributes;

namespace HR.Dol {    
    public class DopravaTyp {        
        public int DopravaTypId { get; private set; }        
        public string KodTypu { get; private set; }        
        public string NazovTypu { get; private set; }

        private DopravaTyp(int id, string kod, string nazov) {
            DopravaTypId = id;
            KodTypu = kod;
            NazovTypu = nazov;
        }

        public static readonly DopravaTyp SluzobneAuto = new (1, "AUTO", "Sluzobne auto");
        public static readonly DopravaTyp Autobus = new (2, "BUS", "Autobus");
        public static readonly DopravaTyp MHD = new (3, "MHD", "MHD");
        public static readonly DopravaTyp Peso = new (4, "PESO", "Peso");
        public static readonly DopravaTyp Vlak = new (5, "VLAK", "Vlak");
        public static readonly DopravaTyp Taxi = new (6, "TAXI", "Taxi");
        public static readonly DopravaTyp Lietadlo = new (7, "LIET", "Lietadlo");

        public static IEnumerable<DopravaTyp> GetAll() {
            yield return SluzobneAuto;
            yield return Autobus;
            yield return MHD;
            yield return Peso;
            yield return Vlak;
            yield return Taxi;
            yield return Lietadlo;
        }
    }
}
