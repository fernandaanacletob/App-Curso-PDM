using appProvaA1Curso.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace appProvaA1Curso.Views;

public partial class TelaListaCurso : ContentPage
{
    ObservableCollection<Curso> listagemCursos = new ObservableCollection<Curso>();
    
    int _searchToken = 0;

    public TelaListaCurso()
    {
        InitializeComponent();
        lstCursos.ItemsSource = listagemCursos;
    }

    // Tratamento do evento de clique no ToolBarItem
    private async void irTelaIncluirCurso(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new TelaIncluirCurso());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro no Cadastro do Curso !!!!", ex.Message, "OK");
        }
    }

    // Método executado quando a página é exibida
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            listagemCursos.Clear();
            List<Curso> temp = await App.Database.GetAllCursos();
            foreach (var curso in temp)
            {
                listagemCursos.Add(curso);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro Desconhecido no Carregamento da Lista !!!!", ex.Message, "OK");
        }
    }

    // Trata o evento Invoked do SwipeItem (EXCLUIR)
    private async void excluirCurso(object sender, EventArgs e)
    {
        try
        {
            var swipeItem = sender as SwipeItem;
            Curso cursoSelecionado = swipeItem?.BindingContext as Curso;

            if (cursoSelecionado == null) return;

            bool confirmacao = await DisplayAlert("Tem Certeza que quer excluir o Curso?",
                $"Excluir {cursoSelecionado.Nome}", "Sim", "Não");

            if (confirmacao)
            {
                await App.Database.DeleteCurso(cursoSelecionado.Id);
                listagemCursos.Remove(cursoSelecionado);
                await DisplayAlert("Sucesso", "Curso excluído com sucesso!", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro na Exclusão do Curso !!!!", ex.Message, "OK");
        }
    }

    // Trata o evento TextChanged da SearchBar (PESQUISAR)
    private async void txtBuscar(object sender, TextChangedEventArgs e)
    {
        // Capture token to ensure only latest search updates the collection
        int token = System.Threading.Interlocked.Increment(ref _searchToken);
        string busca = e.NewTextValue?.Trim();
        try
        {
            refreshView.IsRefreshing = true;

            listagemCursos.Clear();

            List<Curso> temp;
            if (string.IsNullOrWhiteSpace(busca))
            {
                temp = await App.Database.GetAllCursos();
            }
            else
            {
                temp = await App.Database.SearchCursos(busca);
            }

            // If another search started after this one, ignore these results
            if (token != _searchToken) return;

            foreach (var curso in temp)
            {
                listagemCursos.Add(curso);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro na Busca de Cursos !!!!", ex.Message, "OK");
        }
        finally
        {
            // Only clear refreshing if this is the latest search
            if (token == _searchToken)
                refreshView.IsRefreshing = false;
        }
    }

    // Trata o evento SelectionChanged da CollectionView navegando para a página de detalhes (ALTERAR)
    private void lstCursosItemSelected(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            Curso cursoSelecionado = e.CurrentSelection?.FirstOrDefault() as Curso;
            if (cursoSelecionado == null) return;

            Navigation.PushAsync(new TelaAlterarCurso
            {
                BindingContext = cursoSelecionado,
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro Desconhecido na Seleção de Curso !!!!", ex.Message, "OK");
        }
    }

    // Refresh da lista (Pull To Refresh)
    private async void refCarregando(object sender, EventArgs e)
    {
        try
        {
            listagemCursos.Clear();
            List<Curso> temp = await App.Database.GetAllCursos();
            foreach (var curso in temp)
            {
                listagemCursos.Add(curso);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro Desconhecido no carregamento de Cursos !!!!", ex.Message, "OK");
        }
        finally
        {
            refreshView.IsRefreshing = false;
        }
    }
}