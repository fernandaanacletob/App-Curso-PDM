using SQLite;

namespace appProvaA1Curso.Model
{
    [Table("Curso")]
    public class Curso
    {
        [PrimaryKey, AutoIncrement, NotNull, Unique]
        public int idCurso { get; set; }
        [MaxLength(100)]
        public string nomeCurso { get; set; }
        [MaxLength(100)]
        public string enderecoCurso { get; set; }
    }
}
