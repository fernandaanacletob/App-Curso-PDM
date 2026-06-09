using appProvaA1Curso.Model;
using appProvaA1Curso;

namespace appProvaA1Curso.Views;

public partial class TelaIncluirCurso : ContentPage
{
    public TelaIncluirCurso()
    {
        InitializeComponent();
    }
    
    private async void ToolbarItemClickedSalvar(object sender, EventArgs e)
    {
        try
        {
            if ((string.IsNullOrWhiteSpace(txtNomeCurso.Text)))
            {
                DisplayAlert("Erro", "Verifique se a caixa de texto Nome do Curso está vazia !!!!",  "OK");
                txtNomeCurso.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txtEnderecoCurso.Text))
            {
                DisplayAlert("Erro", "Verifique se a caixa de texto Idade da Pessoa está vazia !!!!", "OK");
                txtEnderecoCurso.Focus();
            }
            else
            {
                Curso curso1 = new Curso
                {
                    nomeCurso = txtNomeCurso.Text,
                    enderecoCurso = txtEnderecoCurso.Text
                };
                await App.Database.Insert(curso1);

                await DisplayAlert("Curso Cadastrado com Sucesso !!!!", "", "OK");
                
                await Navigation.PushAsync(new TelaListaCurso());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro no Cadastro do Curso !!!!", ex.Message, "OK");
            txtNomeCurso.Text = "";
            txtEnderecoCurso.Text = "";
            txtNomeCurso.Focus();
        }
    }
}