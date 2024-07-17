namespace HR.Dol {
    
    public class Zamestnanec {        
        public required string OsobneCislo { get; set; }
        
        public required string KrstneMeno { get; set; }
        
        public required string Priezvisko { get; set; }
        
        public DateTime DatumNarodenia { get; set; }
        
        public required string RodneCislo { get; set; }
    }
}
