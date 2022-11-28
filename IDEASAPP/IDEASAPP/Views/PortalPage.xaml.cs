﻿using IDEASAPP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEASAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PortalPage : ContentPage
    {
        public PortalPage()
        {
            InitializeComponent();
            this.BindingContext = new PortalViewModel();
        }
    }
}