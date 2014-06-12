using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Rw.AdeSystem.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> _fluents = new List<string>();
        private List<string> _actions = new List<string>();
        private List<string> _executors = new List<string>();

        private List<string> _lastSuggestions;

        public MainWindow()
        {

            InitializeComponent();
            queryLabel.Visibility = System.Windows.Visibility.Hidden;
            queryTextBox.Visibility = System.Windows.Visibility.Hidden;
            suggestionListBox.Visibility = System.Windows.Visibility.Hidden;
            answerLabel.Visibility = System.Windows.Visibility.Hidden;
            historyListView.Visibility = System.Windows.Visibility.Hidden;
            queryButton.Visibility = System.Windows.Visibility.Hidden;
            clearButton.Visibility = System.Windows.Visibility.Hidden;
            editModelButton.Visibility = Visibility.Visible;
        }

        private void loadModelButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                pathLabel.Content = filename;
                
                List<string> words = LoadModelFile(filename);
                string text = System.IO.File.ReadAllText(filename);
                LoadModel(text);
            }
        }

        public void LoadModel(string text)
        {
            Core.AdeSystem.ResetProlog();
            String[] param = { /*"-q"*/ };
            Core.AdeSystem.Initialize(param);
            try
            {
                Core.AdeSystem.LoadDomain(text);

                Core.AdeSystem.ConstructSystemDomain();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message, "Error");
            }
            _actions = Core.AdeSystem.Actions;
            _fluents = Core.AdeSystem.Fluents;
            _executors = Core.AdeSystem.Executors;
            _executors.Add("epsilon");
            queryButton.Visibility = System.Windows.Visibility.Visible;
            queryTextBox.Text = "";
            answerLabel.Content = "QUERIES HISTORY";
            queryLabel.Visibility = System.Windows.Visibility.Visible;
            queryTextBox.Visibility = System.Windows.Visibility.Visible;
            suggestionListBox.Visibility = System.Windows.Visibility.Visible;
            answerLabel.Visibility = System.Windows.Visibility.Visible;
            historyListView.Visibility = System.Windows.Visibility.Visible;
            clearButton.Visibility = System.Windows.Visibility.Visible;
            editModelButton.Visibility = Visibility.Visible;
            UpdateQueryTextBox();
            queryButton.IsEnabled = true;
        }

        List<String> LoadModelFile(string filePath)
        {
            List<String> words = new List<String>();
            foreach (String line in File.ReadAllLines(filePath))
            {
                // można zwracać linie, zależy co łatwiej będzie przerabiać na prologowy model
                string[] tokens = line.Split(new char[] { ' ' });
                for (int i = 0; i < tokens.Count(); i++)
                {
                    words.Add(tokens[i]);
                }
            }
            return words;
        }

        private void queryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateQueryTextBox();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            var s = e.AddedItems[0];
            string ss = s.ToString();
            if (queryTextBox.Text.EndsWith("!") || ss.Contains(",")) queryTextBox.Text += s;
            else queryTextBox.Text += " " + s;
            UpdateQueryTextBox();
        }

        private void UpdateQueryTextBox()
        {
            suggestionListBox.Items.Clear();
            var openings = new List<string>(new[] { "always", "possibly", "typically" });
            var logicBinOp = new List<string>(new[] { "(", ")","&", "|" });
            if (queryTextBox.Text == String.Empty)
            {
                openings.ForEach(s => suggestionListBox.Items.Add(s))
                ;
                _lastSuggestions = suggestionListBox.Items.Cast<string>().ToList();
            }

            var splited = queryTextBox.Text.Split(' ').Where(s => s.Length > 0);
            var last = splited.LastOrDefault();
            if (string.IsNullOrEmpty(last))
            {
                return;
            }

            // nothing fits
            var keywords =
                new List<string>(new[] { "always", "possibly", "typically", "!", "|", "&", "accessible", "involved", "executable", "from", "in", "by", "after","(",")" });
            keywords.AddRange(_actions);
            keywords.AddRange(_fluents);
            keywords.AddRange(_executors);
            if (!keywords.Contains(last))
            {
                var tmp = last;
                if (tmp.EndsWith(","))
                {
                    last = last.Substring(0, last.Length - 1);
                }
                if (tmp.Contains("!"))
                {
                    last = last.Substring(1, last.Length - 1);
                    tmp = last;
                }
                if (!keywords.Contains(tmp))
                {
                    //then maybe starts with
                    if (_lastSuggestions != null) _lastSuggestions.Where(s => s.StartsWith(tmp))
                        .ToList()
                        .ForEach(i => suggestionListBox.Items.Add(i));

                    return;
                }
            }
            //----------

            suggestionListBox.Items.Clear();
            if (openings.Contains(last))
            {
                suggestionListBox.Items.Add("!");
                _fluents.ForEach(s => suggestionListBox.Items.Add(s));
                Array.ForEach(new[] { "involved", "accessible" }, s => suggestionListBox.Items.Add(s));
                if (last != "typically")
                {
                    suggestionListBox.Items.Add("executable");
                }
            }
            else
            {
                bool com;
                if ((com = queryTextBox.Text.Last() == ',') || queryTextBox.Text.Last() == ' ')
                {
                    if (last.Contains(","))
                    {
                        last = last.Substring(0, last.Length - 1);
                    }
                    if (_actions.FirstOrDefault(s => s.Contains(last)) != null)
                    {
                        _actions.ForEach(s => suggestionListBox.Items.Add(s));
                    }
                    //if (_fluents.FirstOrDefault(s => s.Contains(last)) != null)
                    //{
                    //    _fluents.ForEach(s => suggestionListBox.Items.Add(s));
                    //}
                    if (_executors.FirstOrDefault(s => s.Contains(last)) != null)
                    {
                        _executors.ForEach(s => suggestionListBox.Items.Add(s));
                    }
                    if (com)
                    {
                        _lastSuggestions = suggestionListBox.Items.Cast<string>().ToList();
                        return;
                    }
                }
                if (last == "!" || logicBinOp.Contains(last))
                {
                    if (logicBinOp.Contains(last))
                        suggestionListBox.Items.Add("!");
                    _fluents.ForEach(s => suggestionListBox.Items.Add(s));
                }
                if (last == "from")
                {
                    suggestionListBox.Items.Add("!");
                    _fluents.ForEach(s => suggestionListBox.Items.Add(s));
                }
                if (_fluents.Contains(last))
                {
                    _fluents.ForEach(f => suggestionListBox.Items.Add( f));
                    
                    logicBinOp.ForEach(op => suggestionListBox.Items.Add(op));
                    if (!queryTextBox.Text.Contains("from") && !queryTextBox.Text.Contains("after"))
                    {
                        suggestionListBox.Items.Add("after");
                    }
                }
                if (last == "executable" || last == "in" || last == "after")
                {
                    _actions.ForEach(a => suggestionListBox.Items.Add(a));
                }
                if (last == "accessible" || last == "in")
                {
                    suggestionListBox.Items.Add("!");
                    _fluents.ForEach(s => suggestionListBox.Items.Add(s));
                }
                if (_actions.Contains(last))
                {
                    _actions.ForEach(a => suggestionListBox.Items.Add(", " + a));
                    suggestionListBox.Items.Add("by");
                }
                if (last == "by" || last == "involved")
                {
                    _executors.ForEach(e => suggestionListBox.Items.Add(e));
                }
                if (!_executors.Contains("in") && _executors.Contains(last) && queryTextBox.Text.Contains("involved"))
                {
                    suggestionListBox.Items.Add("in");
                }
                if (_executors.Contains(last))
                {
                    _executors.ForEach(e => suggestionListBox.Items.Add(", " + e));
                }
                if (!queryTextBox.Text.Contains("involved") && (_executors.Contains(last) || _actions.Contains(last) || _fluents.Contains(last)))
                {
                    suggestionListBox.Items.Add("from");
                }

            }
            _lastSuggestions = suggestionListBox.Items.Cast<string>().ToList();
        }

        private void QueryButton_OnClick(object sender, RoutedEventArgs e)
        {
            var query = queryTextBox.GetLineText(0);
            string answer = "";
            try
            {
                answer = Core.AdeSystem.ParseQuery(query);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message, "Error");
                return;
            }
            string output = "\n\n" + query + "\n" + answer;
            answerLabel.Content += output;
            queryTextBox.Text = "";
            UpdateQueryTextBox();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            suggestionListBox.Visibility = System.Windows.Visibility.Visible;
            queryTextBox.Text = "";
            UpdateQueryTextBox();
        }

        private void editModelButton_Click(object sender, RoutedEventArgs e)
        {
            new EditStoryForm(this).ShowDialog();
        }
    }
}
