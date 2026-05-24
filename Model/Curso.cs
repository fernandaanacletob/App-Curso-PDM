/*
 * A biblioteca SQLite é chamada aqui para que as anotações de chave primária
 * e de autoIncremento possam ser usadas na propriedade Id
 */
using SQLite;

namespace appProvaA1Curso.Model
{
    [Table("Curso")]
    public class Curso
    {
        [PrimaryKey, AutoIncrement, Unique, NotNull]
        public int Id { get; set; }

        [MaxLength(200)]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(5)]
        public int CargaHoraria { get; set; }
    }
}