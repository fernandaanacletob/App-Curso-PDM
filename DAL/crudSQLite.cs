using appProvaA1Curso.Model;

using SQLite;

namespace appClasseCursoBD.DAL { 
    public class crudSQLite
    {
        readonly SQLiteAsyncConnection _conexao;

        public crudSQLite(string path) { 
            _conexao = new SQLiteAsyncConnection(path);

            _conexao.CreateTableAsync<Curso>().Wait();
        }

        public Task<int> Insert (Curso curso1)
        {
            return _conexao.InsertAsync(curso1);
        }

        public Task<List<Curso>> Update(Curso curso1)
        {
            string sql = "UPDATE Curso SET nomeCurso = ?, enderecoCurso = ? WHERE idCurso = ?";
            return _conexao.QueryAsync<Curso>(sql, curso1.nomeCurso, curso1.enderecoCurso, curso1.idCurso);
        }

        public Task<List<Curso>> GetAll()
        {
            return _conexao.Table<Curso>().ToListAsync();
        }

        public Task<int> Delete(int idCurso)
        {
            return _conexao.Table<Curso>().DeleteAsync(c => c.idCurso == idCurso);
        }

        public Task<List<Curso>> Search(string buscaCurso)
        {
            string sql = "SELECT * FROM Curso WHER cursoNome LIKE '%" + buscaCurso + "%'";

            return _conexao.QueryAsync<Curso>(sql);
        }
    }
}

