using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Dado
{
    public partial class MainPage : ContentPage
    {
        private Dado meuDado = new Dado();
        private int totalVitorias = 0;
        private int sequenciaAtual = 0;
        private int somaLadosOpostos = 0;
        private int ladosDoDado = 6;

        public MainPage()
        {
            InitializeComponent();

            TipoDadoPicker.SelectedIndexChanged += TipoDadoPicker_SelectedIndexChanged;
            TipoDadoPicker.SelectedIndex = 0;
        }

        private void TipoDadoPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TipoDadoPicker.SelectedIndex)
            {
                case 0: ladosDoDado = 6; break;
                case 1: ladosDoDado = 8; break;
                case 2: ladosDoDado = 10; break;
                case 3: ladosDoDado = 12; break;
                case 4: ladosDoDado = 16; break;
                case 5: ladosDoDado = 20; break;
            }

            SelecaoPicker.Items.Clear();
            for (int i = 1; i <= ladosDoDado; i++)
            {
                SelecaoPicker.Items.Add(i.ToString());
            }
            SelecaoPicker.SelectedIndex = 0;
        }

        private async void RollButton_Clicked(object sender, EventArgs e)
        {
            if (SelecaoPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Aviso", "Escolha um número antes de rolar!", "Ok");
                return;
            }

            if (somaLadosOpostos >= 25)
            {
                await DisplayAlert("Aviso", "Tentativas máximas utilizadas", "Ok");
                return;
            }

            await AnimarDado();

            int resultado = meuDado.Jogar(ladosDoDado);
            dadoImagem.Source = meuDado.GetImagem(resultado);

            if (int.TryParse(SelecaoPicker.SelectedItem?.ToString(), out int escolha))
            {
                int ladoOposto = (ladosDoDado + 1) - resultado;
                somaLadosOpostos += ladoOposto;

                if (escolha == resultado)
                {
                    totalVitorias++;
                    sequenciaAtual++;
                    await DisplayAlert("Parabéns", $"Você acertou!\nVitórias: {totalVitorias}\nSequência: {sequenciaAtual}", "Ok");
                }
                else
                {
                    sequenciaAtual = 0;
                    await DisplayAlert("Tente novamente", $"Você escolheu {escolha}, mas saiu {resultado}.\nSequência resetada.", "Ok");
                }
            }

            if (somaLadosOpostos >= 25)
            {
                await DisplayAlert("Aviso", "Tentativas máximas utilizadas", "Ok");
            }
        }

        private async void RollButtonY_Clicked(object sender, EventArgs e)
        {
            await AnimarDado();

            int resultado = meuDado.Jogar(ladosDoDado);
            dadoImagem.Source = meuDado.GetImagem(resultado);

            if (resultado <= 3)
            {
                await DisplayAlert("Boa", "Você acertou!", "Ok");
            }
            else
            {
                await DisplayAlert("Ixi", "Você errou!", "Ok");
            }
        }

        private async void RollButtonX_Clicked(object sender, EventArgs e)
        {
            await AnimarDado();

            int resultado = meuDado.Jogar(ladosDoDado);
            dadoImagem.Source = meuDado.GetImagem(resultado);

            if (resultado >= 4)
            {
                await DisplayAlert("Boa", "Você acertou!", "Ok");
            }
            else
            {
                await DisplayAlert("Ixi", "Você errou!", "Ok");
            }
        }

        private async Task AnimarDado()
        {
            // Simula uma animação simples de espera
            await Task.Delay(500);
        }
    }
}
