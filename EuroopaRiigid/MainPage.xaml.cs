using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EuroopaRiigid
{
    public partial class MainPage : ContentPage
    {
        Button euroopa_riigid_btn;
        public MainPage()
        {
            euroopa_riigid_btn = new Button
            {
                Text = "Euroopa riigid page",
                BackgroundColor = Color.BlueViolet
            };
            StackLayout st = new StackLayout
            {
                Children = { euroopa_riigid_btn }
            };
            st.BackgroundColor = Color.Bisque;
            Content = st;

            euroopa_riigid_btn.Clicked += Start_Pages;
        }

        private async void Start_Pages(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (sender == euroopa_riigid_btn)
            {
                await Navigation.PushAsync(new EuroopaRiigidPage());
            }
        }
    }
}
