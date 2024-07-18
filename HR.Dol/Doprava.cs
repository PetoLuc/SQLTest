using HR.Dol.Contracts;

namespace HR.Dol {    
    public class Doprava: IEntityMarker
    {        
        public int DopravaId { get; set; }        
        public int CpId { get; set; }        
        public int DopravaTypId { get; set; }                
    }
}
