using appProvaA1Curso.Model;
using System.Collections.ObjectModel;

namespace appProvaA1Curso.Views;

public partial class TelaListaCurso : ContentPage
{
    ObservableCollection<Curso> listagemCursos = new ObservableCollection<Curso>();
    public TelaListaCurso()
    {
        InitializeComponent();

        lstCurso.ItemsSource = listagemCursos;
    }

    private async void irTelaIncluirCurso(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new TelaIncluirCurso());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro no Cadastro do Curso !!!!", ex.Message, "OK");
        }
    }

    protected async override void OnAppearing()
    {
        try
        {
            listagemCursos.Clear();
            List<Curso> temp = await App.Database.GetAll();
            temp.ForEach(i => listagemCursos.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro Desconhecido no Carregamento da Lista !!!!", ex.Message, "OK");
        }
    }

    private async void excluirCurso(object sender, EventArgs e)
    {
        try
        {
            MenuItem itemSelecionado = sender as MenuItem;

            Curso cursoSelecionado = itemSelecionado.BindingContext as Curso;

            bool confirmacao = await DisplayAlert("Tem Certeza que quer excluir o Curso?", $"Excluir {cursoSelecionado.nomeCurso}", "Sim", "Não");
            if (confirmacao)
            {
                await App.Database.Delete(cursoSelecionado.idCurso);
            }
            listagemCursos.Remove(cursoSelecionado);
        }
        catch (Exception ex)
        {
        await DisplayAlert("Erro na Exclusão do Curso !!!!", ex.Message, "OK");
        }
    }
    private async void txtBuscar(object sender, TextChangedEventArgs e)
    {
        try
        {
            string busca = e.NewTextValue;
            lstCurso.IsRefreshing = true;
           
            listagemCursos.Clear();
            List<Curso> temp = await App.Database.Search(busca);
            temp.ForEach(i => listagemCursos.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro na Busca de Cursos !!!!", ex.Message, "OK");
        }
        finally
        {
            lstCurso.IsRefreshing = false;
        }
    }
    /*
    * Trata o evento ItemSelected da ListView navegando para a página de detalhes.
    */
    private void lstCursoItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Curso curso1 = e.SelectedItem as Curso;
            Navigation.PushAsync(new TelaAlterarCurso
            {
                BindingContext = curso1,
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro Desconhecido na Seleção de Curso !!!!", ex.Message, "OK");
        }
    }
    private async void refCarregando(object sender, EventArgs e)
    {
        try
        {
            listagemCursos.Clear();
            List<Curso> temp = await App.Database.GetAll();
            temp.ForEach(i => listagemCursos.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro Desconhecido no carregamento de Cursos !!!!", ex.Message, "OK");
        }
        finally
        {
            lstCurso.IsRefreshing = false;
        }
    }
}