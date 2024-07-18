using HR.Dol.Contracts;

namespace HR.Dol {    
    public class Mesto : IEntityMarker
    {        
        public int MestoId { get; set; }        
        public required string NazovMesta { get; set; }        
        public string? Stat { get; set; }        
        public decimal ZemepisnaSirka { get; set; }        
        public decimal ZemepisnaDlzka { get; set; }
    }
}
