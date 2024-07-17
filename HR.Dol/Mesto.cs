namespace HR.Dol {    
    public class Mesto {        
        public int MestoId { get; set; }        
        public required string NazovMesta { get; set; }        
        public required string Stat { get; set; }        
        public decimal ZemepisnaSirka { get; set; }        
        public decimal ZemepisnaDlzka { get; set; }
    }
}
