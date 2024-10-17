using SQLite;
using ColumnAttribute = SQLite.ColumnAttribute;
using TableAttribute = SQLite.TableAttribute;



namespace ArtistSqlLite
{
    [Table("artist")]
    public class Artist
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }
        [Column("Name")]
        public string ArtistName { get; set; }
        [Column("Date of Birth")]
        public DateTime BirthDate { get; set; }
        [Column("Active")]
        public bool IsActive { get; set; }
        public decimal Salary { get; set; }
    }
}
