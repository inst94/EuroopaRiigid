using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace EuroopaRiigid
{
    public class EuroopaRiigidData
    {
        public string Nimetus { get; set; }
        public string Linn { get; set; }
        public string Rahvaarv { get; set; }
        public string Pilt { get; set; }
    }
    public partial class EuroopaRiigidPage : ContentPage
    {
        public ObservableCollection<EuroopaRiigidData> riigid { get; set; }
        Label lbl_list;
        ListView list;
        Entry country_ent, capital_ent, population_ent, img_ent;
        Button lisa_btn, kustuta_btn, muuda_btn;
        public EuroopaRiigidPage()
        {
            riigid = new ObservableCollection<EuroopaRiigidData>
            {
                new EuroopaRiigidData {Nimetus="Eesti", Linn="Tallinn", Rahvaarv="426538", Pilt="tallinn.jpg"},
                new EuroopaRiigidData {Nimetus="Prantsusmaa", Linn="Pariis", Rahvaarv="2161000", Pilt="paris.jpg"},
                new EuroopaRiigidData {Nimetus="Inglismaa", Linn="London", Rahvaarv="8982000", Pilt="london.jpg"},
                new EuroopaRiigidData {Nimetus="Itaalia", Linn="Rooma", Rahvaarv="2873000", Pilt="rome.jpg"}
            };
            lbl_list = new Label
            {
                Text = "Euroopa riigide loetelu",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            list = new ListView
            {
                SeparatorColor = Color.Orange,
                Header = "Euroopa riigid:",
                Footer = DateTime.Now.ToString("T"),

                HasUnevenRows = true,
                ItemsSource = riigid,
                ItemTemplate = new DataTemplate(() =>
                {
                    ImageCell imageCell = new ImageCell { TextColor = Color.Red, DetailColor = Color.Green };
                    imageCell.SetBinding(ImageCell.TextProperty, "Nimetus");
                    Binding companyBinding = new Binding { Path = "Linn", StringFormat = "{0}" };
                    imageCell.SetBinding(ImageCell.DetailProperty, companyBinding);
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "Pilt");
                    return imageCell;
                })
            };
            country_ent = new Entry
            {
                Placeholder = "Riigi nimetus"
            };
            capital_ent = new Entry
            {
                Placeholder = "Pea linn"
            };
            population_ent = new Entry
            {
                Placeholder = "Rahvaarv"
            };
            img_ent = new Entry
            {
                Placeholder = "Pilt"
            };
            lisa_btn = new Button { Text = "Lisa riik" };
            kustuta_btn = new Button { Text = "Kustuta riik" };
            muuda_btn = new Button { Text = "Muuda riik" };
            list.ItemTapped += List_ItemTapped;
            lisa_btn.Clicked += Lisa_btn_Clicked;
            kustuta_btn.Clicked += Kustuta_btn_Clicked;
            muuda_btn.Clicked += Muuda_btn_Clicked;
            this.Content = new StackLayout { Children = { lbl_list, list, country_ent, capital_ent, population_ent, img_ent, lisa_btn, muuda_btn, kustuta_btn } };
        }

        private async void Muuda_btn_Clicked(object sender, EventArgs e)
        {
            EuroopaRiigidData er = list.SelectedItem as EuroopaRiigidData;

            var name = country_ent.Text;
            if (riigid.Any(x => x.Nimetus == name))
            {
                await DisplayAlert("Error", "Antud riik olemas", "OK");
            }

            else
            {
                if (er != null)
                {
                    riigid.Remove(er);
                    list.SelectedItem = null;
                }
                riigid.Add(new EuroopaRiigidData { Nimetus = country_ent.Text, Linn = capital_ent.Text, Rahvaarv = population_ent.Text, Pilt = img_ent.Text });

            }
        }

        private void Kustuta_btn_Clicked(object sender, EventArgs e)
        {
            EuroopaRiigidData riik = list.SelectedItem as EuroopaRiigidData;
            if (riik != null)
            {
                riigid.Remove(riik);
                list.SelectedItem = null;
            }
        }

        private async void Lisa_btn_Clicked(object sender, EventArgs e)
        {
            var name = country_ent.Text;
            if (riigid.Any(x => x.Nimetus == name))
            {
                await DisplayAlert("Error", "Antud riik olemas", "OK");
            }
            else
            {
                riigid.Add(new EuroopaRiigidData { Nimetus = country_ent.Text, Linn = capital_ent.Text, Rahvaarv = population_ent.Text, Pilt = img_ent.Text });
            }
        }

        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var riik = ((ListView)sender).SelectedItem as EuroopaRiigidData;
            if (riigid == null)
                return;
            await DisplayAlert("Andmed", "Nimetus: " + riik.Nimetus + "\nLinn: " + riik.Linn + " \nRahvaarv: " + riik.Rahvaarv, "Ok");
        }
    }
}
