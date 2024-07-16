using HR.Dol.Attributes;

namespace HR.Dol {
    [TableName("Doprava")]
    public class Doprava {
        [ColumnName("doprava_id")]
        public int DopravaId { get; set; }

        [ColumnName("cp_id")]
        public int CpId { get; set; }

        [ColumnName("doprava_typ_id")]
        public int DopravaTypId { get; set; }

        public CestovnyPrikaz? CestovnyPrikaz { get; set; }
        public required DopravaTyp DopravaTyp { get; set; }
    }
}
