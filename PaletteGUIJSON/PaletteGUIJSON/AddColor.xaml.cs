using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using AnchorMapLib;

namespace PaletteGUIJSON
{
    /// <summary>
    /// Interaction logic for AddColor.xaml
    /// </summary>
    public partial class AddColor : Window
    {
        private ListView ColorList;
        private TextBox CountBox;
        private MainWindow Main;

        public AddColor(ListView ColorGrid, TextBox countbox, MainWindow Main)
        {
            InitializeComponent();
            ColorList = ColorGrid;
            this.Main = Main;
            NameBox.Focus();
            CountBox = countbox;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var name = NameBox.Text;
            int id;
            int r;
            int g;
            int b;
            int a;

            if (!int.TryParse(IDBox.Text, out id))
            {
                IDBox.Clear();
                MessageBox.Show("Incorrect ID value input", "Add color Error", MessageBoxButton.OK);
            }
            else
            {
                if (int.TryParse(RBox.Text, out r) && int.TryParse(GBox.Text, out g) && int.TryParse(BBox.Text, out b) && int.TryParse(ABox.Text, out a))
                {
                    if (r < 0 || r > 255 || g < 0 || g > 255 || b < 0 || b > 255 || a < 0 || a > 255)
                    {
                        MessageBox.Show("RGBA values must be greater than 0 and less than 255", "Add Color Error", MessageBoxButton.OK);
                    }
                    else
                    {
                        List<string> namelist = new List<string>();
                        List<int> idlist = new List<int>();
                        List<string> rgbalist = new List<string>();
                        foreach (var color in Main.Palette.ColorList)
                        {
                            namelist.Add(color.Name);
                            idlist.Add(color.Id);
                            rgbalist.Add($"{color.R}{color.G}{color.B}{color.A}");
                        }

                        var NewColor = new ColorTile(name, id, r, g, b, a);

                        if (namelist.Contains(NewColor.Name))
                        {
                            MessageBox.Show("A color with this name already exists", "Add Color error", MessageBoxButton.OK);
                            NameBox.Clear();
                            NameBox.Focus();
                        }
                        else if (idlist.Contains(NewColor.Id))
                        {
                            MessageBox.Show("A color with this ID already exists", "Add Color Error", MessageBoxButton.OK);
                            IDBox.Clear();
                            IDBox.Focus();
                        }
                        else if (rgbalist.Contains($"{NewColor.R}{NewColor.G}{NewColor.B}{NewColor.A}"))
                        {
                            MessageBox.Show("A Color with this RGBA already exists", "Add Color Error", MessageBoxButton.OK);
                            RBox.Clear();
                            GBox.Clear();
                            BBox.Clear();
                            ABox.Clear();
                            RBox.Focus();
                        }
                        else
                        {
                            Main.Palette.ColorList.Add(NewColor);
                            ColorList.ItemsSource = null;
                            ColorList.ItemsSource = Main.Palette.ColorList;
                            Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("incorrect RGB value input");
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            int count = int.Parse(CountBox.Text);
            count--;
            CountBox.Text = count.ToString();
        }
    }
}
