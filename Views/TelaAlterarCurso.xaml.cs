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
            // Obtém qual foi o Curso anexado no BindingContext
            Curso cursoAnexado = BindingContext as Curso;

            if (cursoAnexado == null)
            {
                await DisplayAlert("Erro", "Curso não encontrado!", "OK");
                return;
            }

            // Preenche o ID (somente leitura)
            txtId.Text = cursoAnexado.Id.ToString();

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

            // Preenchendo o model
            Curso cursoAtualizado = new Curso
            {
                Id = cursoAnexado.Id,
                Nome = txtNomeCurso.Text,
                CargaHoraria = cargaHoraria
            };

            // Atualizando no banco
            await App.Database.UpdateCurso(cursoAtualizado);

            await DisplayAlert("Curso Alterado com Sucesso !!!!", "", "OK");
            await Navigation.PopAsync(); // Volta para a lista
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro na Alteração do Curso !!!!", ex.Message, "OK");
        }
    }
}