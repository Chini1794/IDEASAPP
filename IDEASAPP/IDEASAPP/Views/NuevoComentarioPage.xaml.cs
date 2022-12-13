using IDEASAPP.Models;
using IDEASAPP.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEASAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NuevoComentarioPage : ContentPage
    {
        public Item Item { get; set; }

        public NuevoComentarioPage()
        {
            InitializeComponent();
            BindingContext = new NuevoComentarioViewModel();
        }
    }
}