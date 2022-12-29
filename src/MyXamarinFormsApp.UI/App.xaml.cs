using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaManager;
using Xamarin.Forms;

namespace MyXamarinFormsApp.UI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            CrossMediaManager.Current.Init();
        }
    }
}
