using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Formatting;
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

namespace Acheron.Tests.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int i = 0;
        string str = new(
            "# Header 1\n" +
            "## Header 2\n" +
            "### Header 3\n" +
            "**bold *strong***_*italic*normal__underline~~strikethrough**boldstrikethrough**~~__ `Code Block` *italic\n" +
            "text***`bold __code__ block` plus** __new**line**__ handling!! Oh and **[hyperlinks!](https://google.com)**"
        );
        public MainWindow()
        {
            InitializeComponent();

            scrollViewer.Content = str;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (i == 0)
            {
                scrollViewer.Content = str.ToTextBlockBold(body: 20, h3: 22);
                i = 1;
            }
            else if (i == 1)
            {
                scrollViewer.Content = str;
                i = 0;
            }
        }
    }
}
