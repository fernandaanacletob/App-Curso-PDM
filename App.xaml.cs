using appProvaA1Curso.DAL;
using appProvaA1Curso.Views;

namespace appProvaA1Curso
{
    public partial class App : Application
    {
        static crudSQLite? database;

        public static crudSQLite Database
        {
            get
            {
                if (database == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "Curso.db3"
                     );

                    database = new crudSQLite(path);
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            /* Colocando a tela de lista como Inicial*/
            MainPage = new NavigationPage(new TelaListaCurso());
        }
    }
}