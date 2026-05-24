/*
 * Arquivo Model para criação da Tabela e Transporte de Dados
 */
using appProvaA1Curso.Model;

/*
 * Classes da Biblioteca do SQLite para acesso aos dados e criação da estrutura da tabela.
 */
using SQLite;

namespace appProvaA1Curso.DAL
{
    /*
     * Definição da classe crudSQLite que funciona como uma abstração de acesso ao arquivo db3 do SQLite.
     * A classe contém as informações de "conexão" e os métodos para realizar o CRUD (Create, Read, Update e Delete).
     * Observe que na classe todos os métodos são Async, isso significa que todos são executados via Threads
     * o que, em teoria, não trava a interface do app enquanto os dados são lidos/gravados no arquivo db3.
     */
    public class crudSQLite
    {
        /*
         * Campo da classe que armazena a "conexão" com o arquivo db3.
         * Isso significa que o arquivo db3 é aberto e armazenado aqui para que
         * essa classe possa usar os métodos da classe do SQLite para gravar
         * e ler dados das pessoas cadastradas.
         */
        readonly SQLiteAsyncConnection _conexao;

        /*
         * Método construtor da classe que recebe um parâmetro chamado path para
         * "conectar" ao arquivo db3.
         */
        public crudSQLite(string path)
        {
            /*
             * Abrindo uma nova "conexão" com o arquivo db3 através do caminho recebido.
             * note a utilização da biblioteca SQLite "instalada" no projeto via pacote Nuget
             */
            _conexao = new SQLiteAsyncConnection(path);

            /*
             * Criação da tabela com base no Model Curso (mais detalhes no arquivo Curso.cs na pasta Model)
             * Note que apesar do Async na criação da tabela é chamado o método Wait() que define a espera
             * da criação da tabela (se ela ainda não existir) antes de efetuar as outras operações, por exemplo, insert.
             */
            _conexao.CreateTableAsync<Curso>().Wait();
        }

        /*
         * Método que faz a inserção de um novo registro na tabela. Veja que o método recebe uma Model
         * preenchida com os dados a serem inseridos. Observem que o método tem um retorno do tipo int
         * (número de linhas inseridas) sendo executado via Task (tarefa sendo executada de forma assíncrona).
         */
        public Task<int> InsertCurso(Curso curso)
        {
            return _conexao.InsertAsync(curso);
        }

        /*
         * Método implementado com uso da estratégia de escrever o código SQL.
         * Neste método podemos ver a abstração que o SQLite faz, onde podemos digitar código SQL
         * para manipulação do arquivo db3. O método também recebe uma model preenchida para atualizar
         * no db3 e o retorno em forma de Task.
         */
        public async Task<int> UpdateCurso(Curso curso)
        {
            string sql = "UPDATE Curso SET Nome=?, CargaHoraria=? WHERE Id=?";
            return await _conexao.ExecuteAsync(sql, curso.Nome, curso.CargaHoraria, curso.Id);
        }

        /*
         * Método que faz o retorno de todas as linhas contidas no arquivo db3 referentes
         * a tabela Curso. Veja que o método executa a listagem de forma assíncrona.
         */
        public Task<List<Curso>> GetAllCursos()
        {
            return _conexao.Table<Curso>().ToListAsync();
        }

        /*
         * Método que remove um registro do arquivo db3 de forma assíncrona.
         * Este método recebe como parâmetro o campo Id do registro a ser removido.
         * Observe o uso da LINQ no processo de remoção.
         */
        public Task<int> DeleteCurso(int id)
        {
            return _conexao.Table<Curso>().DeleteAsync(i => i.Id == id);
        }

        /*
         * Método para realizar uma busca na tabela com base em uma string.
         * O método recebe um parâmetro do tipo string e por meio do SQL faz uma busca
         * no campo Nome. É retornada uma Lista de Cursos por meio de uma Task.
         */
        public Task<List<Curso>> SearchCursos(string busca)
        {
            // Use parameterized query to avoid SQL injection and make search case-insensitive
            string sql = "SELECT * FROM Curso WHERE Nome LIKE ?";
            string pattern = $"%{busca}%";
            return _conexao.QueryAsync<Curso>(sql, pattern);
        }
    }
}