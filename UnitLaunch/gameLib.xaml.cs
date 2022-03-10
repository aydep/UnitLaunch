using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
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

        async public void gamesScan()
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
                            catch (Exception) {}
                        }
                    }
                });
            }
        }
        private void addGameCards()
        {
            using (DataModel.UnitContext db = new DataModel.UnitContext())
            {
                foreach (var game in db.Games)
                {
                    Button card = new Button();
                    StackPanel panel = new StackPanel();
                    Border border = new Border();
                    TextBlock gameName = new TextBlock();
                    TextBlock gameFile = new TextBlock();

                    card.Width = 200;
                    card.Height = 300;
                    card.Style = Resources["gameCard"] as Style;
                    card.Content = panel;
                    panel.Children.Add(border);
                    panel.Children.Add(gameName);
                    gameName.Text = Regex.Replace(game.Name, @"([A-Z])", " $1").Trim();
                    panel.Children.Add(gameFile);
                    gameFile.Text = game.FilePath.Split("\\").Last();
                    gameFile.Style = Resources["gameFile"] as Style;

                    card.Click += (s,e) =>
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

                    gameCardsWrap.Inlines.Add(card);
                }
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            gamesScan();
            addGameCards();
        }
    }
}