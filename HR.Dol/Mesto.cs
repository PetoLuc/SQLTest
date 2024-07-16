using HR.Dol.Attributes;

namespace HR.Dol {
    [TableName("Mesto")]
    public class Mesto {
        [ColumnName("mesto_id")]
        public int MestoId { get; set; }

        [ColumnName("nazov_mesta")]
        public required string NazovMesta { get; set; }

        [ColumnName("stat")]
        public required string Stat { get; set; }

        [ColumnName("zemepisna_sirka")]
        public decimal ZemepisnaSirka { get; set; }

        [ColumnName("zemepisna_dlzka")]
        public decimal ZemepisnaDlzka { get; set; }
    }
}
