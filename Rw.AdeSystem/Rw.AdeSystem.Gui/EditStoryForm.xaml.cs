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

namespace Rw.AdeSystem.Gui
{
    /// <summary>
    /// Interaction logic for EditStoryForm.xaml
    /// </summary>
    public partial class EditStoryForm : Window
    {
        private MainWindow MainWindowRef { get; set; }
        public EditStoryForm(MainWindow _ref)
        {
            InitializeComponent();
            MainWindowRef = _ref;
            domainTextBlock.Text = Core.AdeSystem.Domain;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            //TU WSTAWIC REBUILD MODELU
            MainWindowRef.LoadModel(domainTextBlock.Text);
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
