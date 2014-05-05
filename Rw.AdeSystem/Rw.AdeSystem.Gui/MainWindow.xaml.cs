using System;
using System.IO;
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

namespace Rw.AdeSystem.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<string> _fluents = new List<string>();
        private readonly List<string> _actions = new List<string>();
        private readonly List<string> _executors = new List<string>();

        private List<string> _lastSuggestions;

        public MainWindow()
        {
            InitializeComponent();
            queryLabel.Visibility = System.Windows.Visibility.Hidden;
            queryTextBox.Visibility = System.Windows.Visibility.Hidden;
            suggestionListBox.Visibility = System.Windows.Visibility.Hidden;
            answerLabel.Visibility = System.Windows.Visibility.Hidden;
            historyListView.Visibility = System.Windows.Visibility.Hidden;

            _fluents.Add("hasGunHador");
            _fluents.Add("hasGunMiętus");
            _fluents.Add("alive");
            _fluents.Add("walking");

            _actions.Add("SHOOT");
            _actions.Add("CHOWN");
            _actions.Add("ENTICE");

            _executors.Add("Hador");
            _executors.Add("Miętus");
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
                createModelButton.IsEnabled = true;
            }
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

        private void createModelButton_Click(object sender, RoutedEventArgs e)
        {
            buildModelLabel.Content = "Model is ready. You can now start querying.";
            queryLabel.Visibility = System.Windows.Visibility.Visible;
            queryTextBox.Visibility = System.Windows.Visibility.Visible;
            suggestionListBox.Visibility = System.Windows.Visibility.Visible;
            answerLabel.Visibility = System.Windows.Visibility.Visible;
            historyListView.Visibility = System.Windows.Visibility.Visible;
            UpdateQueryTextBox();
        }

        private void queryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateQueryTextBox();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            var s = e.AddedItems[0];
            queryTextBox.Text += " " + s;
            UpdateQueryTextBox();
        }

        private void UpdateQueryTextBox()
        {
            suggestionListBox.Items.Clear();
            var openings = new List<string>(new[] { "always", "possibly", "typically" });
            var logicBinOp = new List<string>(new[] { "or,and" });
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
                new List<string>(new[] { "always", "possibly", "typically", "not", "or", "and", "accessible", "involved", "executable", "from", "in", "by", "after" });
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
                suggestionListBox.Items.Add("not");
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
                    if (_fluents.FirstOrDefault(s => s.Contains(last)) != null)
                    {
                        _fluents.ForEach(s => suggestionListBox.Items.Add(s));
                    }
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
                if (last == "not" || logicBinOp.Contains(last))
                {
                    _fluents.ForEach(s => suggestionListBox.Items.Add(s));
                }
                if (last == "from")
                {
                    _fluents.ForEach(s => suggestionListBox.Items.Add(s));
                }
                if (_fluents.Contains(last))
                {
                    _fluents.ForEach(f => suggestionListBox.Items.Add(", " + f));
                    logicBinOp.ForEach(op => suggestionListBox.Items.Add(op));
                    if (!queryTextBox.Text.Contains("from") && !queryTextBox.Text.Contains("after"))
                    {
                        suggestionListBox.Items.Add("after");
                    }
                }
                if (last == "executable" || last == "in")
                {
                    _actions.ForEach(a => suggestionListBox.Items.Add(a));
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

                if (!queryTextBox.Text.Contains("involved") && (_executors.Contains(last) || _actions.Contains(last) || _fluents.Contains(last)))
                {
                    suggestionListBox.Items.Add("from");
                }

            }
            _lastSuggestions = suggestionListBox.Items.Cast<string>().ToList();
        }

        /*private void FluentsTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            _fluents.Clear();
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                Array.ForEach(textBox.Text.Split(' ', ','), s => _fluents.Add(s));
            }
            UpdateListBox();
        }

        private void ExecutorsTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            _executors.Clear();
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                Array.ForEach(textBox.Text.Split(' ', ','), s => _executors.Add(s));
            }
            UpdateListBox();
        }

        private void ActionsTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            _actions.Clear();
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                Array.ForEach(textBox.Text.Split(' ', ','), s => _actions.Add(s));
            }
            UpdateListBox();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            var s = e.AddedItems[0];
            QueryTextBox.Text += " " + s;
            UpdateListBox();
        }

        private void UpdateListBox()
        {
            // TODO: @robert dokoncz / ulepsz to funkcje widzisz o co chodzi

            SuggestionListBox.Items.Clear();
            var openings = new List<string>(new[] { "always", "possibly", "typically" });
            var logicBinOp = new List<string>(new[] { "or,and" });
            if (QueryTextBox.Text == String.Empty)
            {
                openings.ForEach(s => SuggestionListBox.Items.Add(s))
                ;
                _lastSuggestions = SuggestionListBox.Items.Cast<string>().ToList();
            }

            var splited = QueryTextBox.Text.Split(' ').Where(s => s.Length > 0);
            var last = splited.LastOrDefault();
            if (string.IsNullOrEmpty(last))
            {
                return;
            }

            // nothing fits
            var keywords =
                new List<string>(new[] { "always", "possibly", "typically", "not", "or", "and", "accessible", "involved", "executable", "from", "in", "by", "after" });
            keywords.AddRange(_actions);
            keywords.AddRange(_fluents);
            keywords.AddRange(_executors);
            if (!keywords.Contains(last))
            {
                var tmp = last;
                if (tmp.EndsWith(","))
                {
                    last=last.Substring(0, last.Length - 1);
                }
                if (!keywords.Contains(tmp))
                {
                    //then maybe starts with
                    _lastSuggestions.Where(s => s.StartsWith(tmp))
                        .ToList()
                        .ForEach(i => SuggestionListBox.Items.Add(i));

                    return;
                }
            }
            //----------

            SuggestionListBox.Items.Clear();
            if (openings.Contains(last))
            {
                SuggestionListBox.Items.Add("not");
                _fluents.ForEach(s => SuggestionListBox.Items.Add(s));
                Array.ForEach(new[] { "involved", "accessible" }, s => SuggestionListBox.Items.Add(s));
                if (last != "typically")
                {
                    SuggestionListBox.Items.Add("executable");
                }
            }
            else
            {
                bool com;
                if ((com = QueryTextBox.Text.Last() == ',') || QueryTextBox.Text.Last() == ' ')
                {
                    if (last.Contains(","))
                    {
                        last = last.Substring(0, last.Length - 1);
                    }
                    if (_actions.FirstOrDefault(s => s.Contains(last)) != null)
                    {
                        _actions.ForEach(s => SuggestionListBox.Items.Add(s));
                    }
                    if (_fluents.FirstOrDefault(s => s.Contains(last)) != null)
                    {
                        _fluents.ForEach(s => SuggestionListBox.Items.Add(s));
                    }
                    if (_executors.FirstOrDefault(s => s.Contains(last)) != null)
                    {
                        _executors.ForEach(s => SuggestionListBox.Items.Add(s));
                    }
                    if (com)
                    {
                        _lastSuggestions = SuggestionListBox.Items.Cast<string>().ToList();
                        return;
                    }
                }
                if (last == "not" || logicBinOp.Contains(last))
                {
                    _fluents.ForEach(s => SuggestionListBox.Items.Add(s));
                }
                if (last == "from")
                {
                    _fluents.ForEach(s => SuggestionListBox.Items.Add(s));
                }
                if (_fluents.Contains(last))
                {
                    _fluents.ForEach(f => SuggestionListBox.Items.Add(", " + f));
                    logicBinOp.ForEach(op => SuggestionListBox.Items.Add(op));
                    if (!QueryTextBox.Text.Contains("from") && !QueryTextBox.Text.Contains("after"))
                    {
                        SuggestionListBox.Items.Add("after");
                    }
                }
                if (last == "executable" || last == "in")
                {
                    _actions.ForEach(a => SuggestionListBox.Items.Add(a));
                }
                if (_actions.Contains(last))
                {
                    _actions.ForEach(a => SuggestionListBox.Items.Add(", " + a));
                    SuggestionListBox.Items.Add("by");
                }
                if (last == "by" || last == "involved")
                {
                    _executors.ForEach(e => SuggestionListBox.Items.Add(e));
                }
                if (!_executors.Contains("in") && _executors.Contains(last) && QueryTextBox.Text.Contains("involved"))
                {
                    SuggestionListBox.Items.Add("in");
                }

                if (!QueryTextBox.Text.Contains("involved") && (_executors.Contains(last) || _actions.Contains(last) || _fluents.Contains(last)))
                {
                    SuggestionListBox.Items.Add("from");
                }

            }
            _lastSuggestions = SuggestionListBox.Items.Cast<string>().ToList();
        }

        private void QueryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateListBox();
        }*/

    }
}
