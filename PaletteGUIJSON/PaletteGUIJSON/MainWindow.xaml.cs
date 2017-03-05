using System;
using System.Collections.Generic;
using System.IO;
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
using AnchorMapLib;
using System.Web.Script.Serialization;

namespace PaletteGUIJSON
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Palette Palette;
        public bool PaletteOpen;
        public bool ChangesMade;
        public bool HasBeenSavedAs;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region
        private void AddColor_Click(object sender, RoutedEventArgs e)
        {
            if (PaletteOpen)
            {
                AddColor AddColor_win = new AddColor(List, CountBox, this);
                AddColor_win.Show();
                int count = int.Parse(CountBox.Text);
                CountBox.Text = ""; //REMOVE IS NO ERRORS ARISE
                count++;
                CountBox.Text = count.ToString();
                Palette.Count += 1;
                ChangesMade = true;
            }
            else
            {
                MessageBox.Show("You must be editing a palette to add colors", "Add Color Error", MessageBoxButton.OK);
            }

        }

        private void RemoveColor_Click(object sender, RoutedEventArgs e)
        {
            if (PaletteOpen)
            {
                if (List.SelectedIndex == -1)
                {
                    MessageBox.Show("You must select a color", "Remove Color Error", MessageBoxButton.OK);
                }
                else
                {
                    var result = MessageBox.Show($"Are you sure you want to delete the color \"{Palette.ColorList[List.SelectedIndex].Name}\"?", "Color Loss Warning", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        var item = Palette.ColorList[List.SelectedIndex];
                        Palette.ColorList.Remove(item);
                        List.ItemsSource = null;
                        List.ItemsSource = Palette.ColorList;

                        var count = int.Parse(CountBox.Text);
                        count--;
                        CountBox.Text = count.ToString();
                    }
                }
            }
            else
            {
                MessageBox.Show("You must be editing a palette to remove colors", "Remove Color Error", MessageBoxButton.OK);
            }
            ChangesMade = true;
        }

        private void NewPalette_Click(object sender, RoutedEventArgs e)
        {
            if (PaletteOpen)
            {
                var result = MessageBox.Show("Do you want to save your current Palette?", "File Loss Warning", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        SaveAsPalette_Click(this, new RoutedEventArgs());
                        break;

                    case MessageBoxResult.Cancel:
                        break;
                }

                if (result == MessageBoxResult.Yes || result == MessageBoxResult.No)
                {
                    NewPalette NewWin = new NewPalette(NameBox, CountBox, this);
                    NewWin.Show();
                }
            }
            else
            {
                NewPalette Win = new NewPalette(NameBox, CountBox, this);
                Win.Show();
            }

            PaletteOpen = true;
            //Palette = new Palette(); DELETE THIS IF NO PROBLEMS ARISE

            HasBeenSavedAs = false;
            ChangesMade = true;
        }

        

        private void ExitApp_Click(object sender, RoutedEventArgs e)
        {
            if (PaletteOpen && ChangesMade)
            {
                var res = MessageBox.Show("Do you want to save your current palette before exiting?", "File Loss Warning", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                    if (HasBeenSavedAs)
                    {
                        SavePalette_Click(this, new RoutedEventArgs());
                    }
                    else
                    {
                        SaveAsPalette_Click(this, new RoutedEventArgs());
                    }
                }
            }
            else
            {
                Close();
            }
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            var AboutWin = new AboutWin();
            AboutWin.Show();
        }

        private void Creator_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.github.com/tidizzle");
        }
        #endregion

        private void OpenPalette_Click(object sender, RoutedEventArgs e)
        {
            HasBeenSavedAs = true;

            Microsoft.Win32.OpenFileDialog openDialog = new Microsoft.Win32.OpenFileDialog();
            openDialog.DefaultExt = ".json";
            openDialog.Filter = "JavaScript Object Notation File | *.json";
            openDialog.Title = "Open Palette File";

            var result = openDialog.ShowDialog();

            if (result == true)
            {
                if (!openDialog.FileName.Contains(".json"))
                {
                    MessageBox.Show("Incorrect File Type", "File Open Error", MessageBoxButton.OK);
                }
                else
                {
                    PaletteOpen = true;

                    var palString = File.ReadAllText(openDialog.FileName);
                    var ser = new JavaScriptSerializer();
                    var openedPalette = ser.Deserialize<>();
                }
            }
        }

        private void SavePalette_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveAsPalette_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
