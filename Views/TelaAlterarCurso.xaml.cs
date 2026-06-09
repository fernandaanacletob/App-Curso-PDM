using appProvaA1Curso.Model;

namespace appProvaA1Curso.Views;

public partial class TelaAlterarCurso : ContentPage
{
    public TelaAlterarCurso()
    {
        InitializeComponent();
    }
    private async void ToolbarItemClickedSalvar(object sender, EventArgs e)
    {
        try
        {
            Curso CursoAnexado = BindingContext as Curso;
            //Verificando se os elementos Entry estão vazios ou nulos
            if ((string.IsNullOrWhiteSpace(txtNomeCurso.Text)))
            {
                DisplayAlert("Erro", "Verifique se a caixa de texto Nome do Curso está vazia !!!!", "OK");
                txtNomeCurso.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txtEnderecoCurso.Text))
            {
                DisplayAlert("Erro", "Verifique se a caixa de texto Endereço do Curso está vazia !!!!", "OK");
                txtEnderecoCurso.Focus();
            }
            else
            {
                Curso curso1 = new Curso
                {
                    idCurso = CursoAnexado.idCurso,
                    nomeCurso = txtNomeCurso.Text,
                    enderecoCurso = txtEnderecoCurso.Text
                };
                await App.Database.Update(curso1);
                await DisplayAlert("Curso Alterado com Sucesso !!!!", "", "OK");
                await Navigation.PushAsync(new TelaListaCurso());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro na Alteração do Curso !!!!", ex.Message, "OK");
        }
    }
}
