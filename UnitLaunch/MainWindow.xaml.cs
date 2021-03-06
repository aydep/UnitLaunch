using Microsoft.EntityFrameworkCore;
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
using System.Windows.Shapes;

namespace UnitLaunch
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Content = new store();

            lastGamesLoad();
        }

        private void lastGamesLoad()
        {
            using (DataModel.UnitContext db = new DataModel.UnitContext())
            {
                var games = db.Games.Where(p => p.LastRun != null).OrderByDescending(d => d.LastRun).ToArray();
                int i = 0;

                foreach (var game in games)
                {
                    Button card = new Button();
                    Image image = new Image();
                    Border border = new Border();
                    Grid grid = new Grid();
                    TextBlock name = new TextBlock();

                    card.Style = Resources["lastGamesItem"] as Style;
                    card.Content = grid;
                    grid.Children.Add(image);
                    grid.Children.Add(border);
                    grid.Children.Add(name);

                    BitmapImage bitImg = new BitmapImage();
                    bitImg.BeginInit();
                    bitImg.UriSource = new Uri("./Resources/images/Prey_(2017_video_game).jpg", UriKind.Relative);
                    bitImg.EndInit();
                    image.Source = bitImg;

                    name.Text = game.Name;

                    card.Name = "lastGameCard"+i;

                    card.Click += (s, e) =>
                    {
                        using (DataModel.UnitContext bdb = new DataModel.UnitContext())
                        {
                            System.Diagnostics.Process Proc = new System.Diagnostics.Process();
                            var curGame = bdb.Games.Find(game.Id);
                            Proc.StartInfo.FileName = game.FilePath;
                            Proc.Start();
                            curGame.LastRun = DateTime.Now;
                            bdb.SaveChanges();
                        }
                    };

                    mainMenu.Children.Add(card);

                    i++;
                    if (i == 3) break;
                }
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double winWidth = e.NewSize.Width;

            //width.Text = winWidth.ToString();
            //height.Text = libBut.FontSize.ToString();

            if (winWidth <= 1980)
            {
                libBut.FontSize = winWidth / 70;
                storeBut.FontSize = winWidth / 70;
                downloadBut.FontSize = winWidth / 70;
                lastGamesBorder.FontSize = winWidth / 100;

                ixon1.Height = winWidth / 60;
                ixon1.Width = winWidth / 60;

                ixon2.Height = winWidth / 60;
                ixon2.Width = winWidth / 60;

                ixon3.Height = winWidth / 60;
                ixon3.Width = winWidth / 60;
            }
        }

        private void downloadBut_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void storeBut_Selected(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new store());
        }

        private void libBut_Selected(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new gameLib());
            mainFrame.Navigate(new gameLib());
        }
    }
}
