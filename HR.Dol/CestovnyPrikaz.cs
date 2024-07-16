using HR.Dol.Attributes;

namespace HR.Dol {

    [TableName("CestovnyPrikaz")]
    public class CestovnyPrikaz {
        [ColumnName("cp_id")]
        public int CpId { get; set; }

        [ColumnName("datum_vytvorenia")]
        public DateTime DatumVytvorenia { get; set; }

        //[ColumnName("ucastnik")]
        //public required Zamestnanec? Ucastnik { get; set; }

        [ColumnName("ucastnik")]
        public required string UcastnikId { get; set; }

        [ColumnName("miesto_zaciatku")]
        public int MiestoZaciatku { get; set; }

        [ColumnName("miesto_konca")]
        public int MiestoKonca { get; set; }

        [ColumnName("datum_cas_zaciatku")]
        public DateTime DatumCasZaciatku { get; set; }

        [ColumnName("datum_cas_konca")]
        public DateTime DatumCasKonca { get; set; }

        [ColumnName("stav_id")]
        public int StavId { get; set; }

        public Mesto? MiestoZaciatkuMesto { get; set; }
        public Mesto? MiestoKoncaMesto { get; set; }
        public Stav Stav { get; set; } = Stav.Vytvoreny;
        public List<Doprava> DopravaList { get; set; } = [];
    }
}
