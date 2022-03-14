using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UnitLaunch
{
    /// <summary>
    /// Логика взаимодействия для store.xaml
    /// </summary>
    public partial class store : Page
    {
        public store()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            parseGenres();
        }

        private async void parseGenres()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var doc = await AngleSharp.BrowsingContext.New(config).OpenAsync("https://rutracker.org/forum/viewforum.php?f=5");

            for (int i = 1; i < doc.GetElementsByClassName("forumlink").Length; i++)
            {
                string title = doc.GetElementsByClassName("forumlink")[i].GetElementsByTagName("a")[0].InnerHtml;
                string link = "https://rutracker.org/forum/" + doc.GetElementsByClassName("forumlink")[i].GetElementsByTagName("a")[0].GetAttribute("href");

                ListViewItem card = new ListViewItem();
                StackPanel cardPanel = new StackPanel();
                TextBlock cardTitle = new TextBlock();
                TextBlock cardLink = new TextBlock();

                card.Style = Resources["butt"] as Style;
                card.Padding = new Thickness(5, 15, 5, 15);
                card.Content = cardPanel;
                cardPanel.Children.Add(cardTitle);
                cardTitle.Text = title;
                cardLink.Text = link;

                MainGrid.Items.Add(card);

                card.Selected += (s, e) => parseGenreGames(link);
            }
        }

        private async void parseGenreGames(string path)
        {

        }
    }
}
