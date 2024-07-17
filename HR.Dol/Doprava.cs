namespace HR.Dol {    
    public class Doprava {        
        public int DopravaId { get; set; }        
        public int CpId { get; set; }        
        public int DopravaTypId { get; set; }
        public CestovnyPrikaz? CestovnyPrikaz { get; set; }
        public required DopravaTyp DopravaTyp { get; set; }
    }
}
