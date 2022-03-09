using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для gameLib.xaml
    /// </summary>
    public partial class gameLib : Page
    {
        public gameLib()
        {
            InitializeComponent();
        }

        async public void gamesDbUpdate()
        {
            using (DataModel.UnitContext db = new DataModel.UnitContext())
            {
                await Task.Run(() =>
                {
                    string[] ignoreWords = new string[] { "uni", "Uni", "000", "setup", "Setup", "crash", "Crash", "handler", "Handler", "32" };
                    string[] gameFolders = System.IO.Directory.GetDirectories("D:\\Games");

                    foreach (string folder in gameFolders)
                    {
                        DataModel.Game game = new DataModel.Game();
                        game.Name = folder.Split("\\").Last();

                        string[] files = System.IO.Directory.GetFiles(folder, "*.exe");

                        foreach (string file in files)
                        {
                            if (!ignoreWords.Any(x => file.IndexOf(x) >= 0))
                            {
                                game.FilePath = file;
                            }
                        }

                        if (game.FilePath != null)
                        {
                            try
                            {
                                db.Games.Add(game);
                                db.SaveChanges();
                            }
                            catch (Exception)
                            {

                            }
                            
                        }
                    }

                    //var games = db.Games.ToList();
                    foreach (var game in db.Games)
                    {
                        MessageBox.Show($"{game.Id}.{game.Name} {game.FilePath} {game.ImagePath}");
                    }
                });
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            gamesDbUpdate();
        }

        private void addGameCard()
        {
            using (DataModel.UnitContext db = new DataModel.UnitContext())
            {
                var games = db.Games.ToList();

                foreach (var game in db.Games)
                {
                    Button card = new Button();
                    StackPanel panel = new StackPanel();
                    Border border = new Border();
                    TextBlock gameName = new TextBlock();
                    TextBlock gameFolder = new TextBlock();


                    card.Width = 200;
                    card.Height = 300;
                    card.Style = Resources["gameCard"] as Style;
                    card.Content = panel;
                    panel.Children.Add(border);
                    panel.Children.Add(gameName);
                    gameName.Text = game.Name;
                    panel.Children.Add(gameFolder);
                    gameFolder.Text = game.FilePath.Split("\\").Last();
                    gameFolder.Style = Resources["gameFolder"] as Style;

                    card.AddHandler(Button.ClickEvent, new RoutedEventHandler(gameCardClick));

                    gameCardsWrap.Inlines.Add(card);
                }
            }
        }

        private void gameCardClick(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            addGameCard();
        }
    }
}