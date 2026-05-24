using appProvaA1Curso.Model;

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
            // Verificando se os elementos Entry estão vazios ou nulos
            if (string.IsNullOrWhiteSpace(txtNomeCurso.Text))
            {
                await DisplayAlert("Erro", "Verifique se a caixa de texto Nome do Curso está vazia !!!!", "OK");
                txtNomeCurso.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCargaHoraria.Text))
            {
                await DisplayAlert("Erro", "Verifique se a caixa de texto Carga Horária está vazia !!!!", "OK");
                txtCargaHoraria.Focus();
                return;
            }

            if (!int.TryParse(txtCargaHoraria.Text, out int cargaHoraria))
            {
                await DisplayAlert("Erro", "Carga Horária deve ser um número válido !!!!", "OK");
                txtCargaHoraria.Focus();
                return;
            }

            // Preenchendo o model Curso
            Curso novoCurso = new Curso
            {
                Nome = txtNomeCurso.Text,
                CargaHoraria = cargaHoraria
            };

            // Inserindo no banco
            await App.Database.InsertCurso(novoCurso);

            await DisplayAlert("Curso Cadastrado com Sucesso !!!!", "", "OK");
            await Navigation.PopAsync(); // Volta para a lista
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro no Cadastro do Curso !!!!", ex.Message, "OK");
            txtNomeCurso.Text = "";
            txtCargaHoraria.Text = "";
            txtNomeCurso.Focus();
        }
    }
}