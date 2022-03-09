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
            mainFrame.Content = new gameLib();
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

        async private void storeBut_Selected(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                using (DataModel.UnitContext db = new DataModel.UnitContext())
                {
                    db.Games.RemoveRange(db.Games);
                    db.SaveChanges();
                    MessageBox.Show(db.Games.ToList().Count().ToString());
                }

            });
        }
    }
}
