using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lembrete.Modelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lembrete.Telas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cadastro : ContentPage
    {
        private byte Prioridade { get; set; }
        public Cadastro()
        {
            InitializeComponent();
        }

        private void PrioridadeSelectAction(object sender, EventArgs args)
        {
            var Stacks = SLPrioridades.Children;
            foreach (var Stack in Stacks)
            {
                Label LblPrioridade = ((StackLayout)Stack).Children[1] as Label;
                LblPrioridade.TextColor = Color.Gray;
            }
            ((Label)((StackLayout)sender).Children[1]).TextColor = Color.Black;
            FileImageSource Sourcer = ((Image)((StackLayout)sender).Children[0]).Source as FileImageSource;
            String Prioridade = Sourcer.File.ToString().Replace("img/", "").Replace(".png", "");

            this.Prioridade = byte.Parse(Prioridade);

        }
        public void SalvarAction(object sender, EventArgs args)
        {
            bool ErroExiste = false;
            if (TxtNome.Text == null || TxtNome.Text.Trim().Length <= 0)
            {
                ErroExiste = true;
                DisplayAlert("ERROR", "Tarefa nao digitada", "OK");
            }

            if (Prioridade <= 0)
            {
                ErroExiste = true;
                DisplayAlert("ERROR", "Prioridade nao escolhida", "OK");
            }
            if (ErroExiste == false)
            {
                //salvar dados
                Tarefa tarefa = new Tarefa ();
                tarefa.Nome = TxtNome.Text.Trim();
                tarefa.Prioridade = this.Prioridade;

                new GerenciadoTarefa().Salvar(tarefa);

                App.Current.MainPage = new NavigationPage(new Inicio());
            }
                

                
        }
    }
}