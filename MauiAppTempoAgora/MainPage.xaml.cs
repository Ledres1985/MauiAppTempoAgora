using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {

                var conexao = Connectivity.NetworkAccess;
                if (conexao != NetworkAccess.Internet)
                {
                    await DisplayAlert("Sem conexão", "Verifique sua conexão com a internet e tente novamente.", "OK");
                    return;
                }


                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Nascer do Sol: {t.sunrise} \n" +
                                         $"Por do Sol: {t.sunset} \n" +
                                         $"Temp Máx: {t.temp_max} \n" +
                                         $"Temp Min: {t.temp_min} \n" +
                                         $"Velocidade do vento: {t.speed} \n" +
                                         $"Descrição do clima: {t.description} \n" +
                                         $"Visibilidade: {t.visibility} \n"
                                         ;

                        lbl_res.Text = dados_previsao;

                    }
                    else
                    {

                        lbl_res.Text = "Sem dados de Previsão";
                        await DisplayAlert("Cidade não encontrada", "Não foi possível localizar a cidade informada. Verifique o nome e tente novamente.", "OK");
                    }

                }
                else
                {
                    lbl_res.Text = "Preencha a cidade.";
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}
    
