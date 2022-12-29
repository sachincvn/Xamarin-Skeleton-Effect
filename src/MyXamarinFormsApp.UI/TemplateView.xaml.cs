using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyXamarinFormsApp.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TemplateView : ContentView
    {
        private string _myText;
        public string MyText { get => _myText; set => _myText = value; }
        public TemplateView()
        {
            InitializeComponent();
            MyLabel.Text = MyText;
        }
    }
}
