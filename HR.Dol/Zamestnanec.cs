using HR.Dol.Attributes;

namespace HR.Dol {

    [TableName("Zamestnanec")]
    public class Zamestnanec {
        [ColumnName("osobne_cislo")]
        public required string OsobneCislo { get; set; }

        [ColumnName("krstne_meno")]
        public required string KrstneMeno { get; set; }

        [ColumnName("priezvisko")]
        public required string Priezvisko { get; set; }

        [ColumnName("datum_narodenia")]
        public DateTime DatumNarodenia { get; set; }

        [ColumnName("rodne_cislo")]
        public required string RodneCislo { get; set; }
    }
}
