using HtmlAgilityPack;
using Fizzler;
using Fizzler.Systems.HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xamarin.Forms;
using System.Web;

namespace GrigorevMaxim
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            currentIdEditor.Text = "";
            GetXml(@"https://yastatic.net/market-export/_/partner/help/YML.xml");

        }


        XmlNodeList collections;
        public void GetXml(string adress)
        {
            var xml = new WebClient().DownloadString(adress);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            var page = doc.DocumentElement;
            collections = page.SelectNodes("//yml_catalog/shop/offers/offer");
            List<string> ids = new List<string>();
            foreach (XmlNode item in collections)
            {
                ids.Add(item.Attributes["id"].Value);
            }

            OutputList.ItemsSource = ids;
        }

        private void OutputList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            foreach (XmlNode item in collections)
            {
                if (item.Attributes["id"].Value == OutputList.SelectedItem.ToString())
                {
                    currentIdEditor.Text = JsonConvert.SerializeObject(item);
                }
            }
        }
    }
}
