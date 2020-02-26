﻿using System;
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
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();
            DataHoje.Text = DateTime.Now.DayOfWeek.ToString() + ", " + DateTime.Now.ToString("dd/MM");
            CarregarTarefas();
        }

        private void ActionGoCadastro(object sender, EventArgs args)
        {
            Navigation.PushAsync(new Cadastro());
        }
        private void CarregarTarefas()
        {
            SLTarefas.Children.Clear();
            List<Tarefa> Lista = new GerenciadoTarefa().Listagem();

            int i = 0;

            foreach(Tarefa tarefa in Lista)
            {
                LinhaStackLayout(tarefa, i);
                i++;
            }

        }
        public void LinhaStackLayout(Tarefa tarefa, int index)
        {
            View StackCentral = null;
            if (tarefa.DataFinalizacao == null)
            {
                StackCentral = new Label()
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Text = tarefa.Nome
                };
            }
            else
            {

                StackCentral = new StackLayout()
                {
                    VerticalOptions = LayoutOptions.Center,
                    Spacing = 0,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                ((StackLayout)StackCentral).Children.Add(new Label()
                    {
                    Text = tarefa.Nome,
                    TextColor = Color.Gray
                });

                ((StackLayout)StackCentral).Children.Add(new Label() { Text = "Finalizado em " + tarefa.DataFinalizacao.Value.ToString("dd/MM/yyyy - hh:mm") + "h", TextColor = Color.Gray, FontSize = 10 });

            }


            Image Check = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("img/CheckOff.png") };
            if(tarefa.DataFinalizacao != null)
            {
                Check.Source = ImageSource.FromFile("img/CheckOn.png");
            };

            TapGestureRecognizer CheckTap = new TapGestureRecognizer();
            CheckTap.Tapped += delegate
            {
                new GerenciadoTarefa().Finalizar(index, tarefa);
                CarregarTarefas();
            };
            Check.GestureRecognizers.Add(CheckTap);

            Image Prioridade = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("img/" + tarefa.Prioridade + ".png") };
            Image Delete = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("img/Delete.png") };

            TapGestureRecognizer DeleteTap = new TapGestureRecognizer();
            DeleteTap.Tapped += delegate
            {
                new GerenciadoTarefa().Deletar(index);
                CarregarTarefas();
            };
            Delete.GestureRecognizers.Add(DeleteTap);


            StackLayout Linha = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 15 };
            Linha.Children.Add(Check);
            Linha.Children.Add(StackCentral);
            Linha.Children.Add(Prioridade);
            Linha.Children.Add(Delete);

            SLTarefas.Children.Add(Linha);
        }
    }
}