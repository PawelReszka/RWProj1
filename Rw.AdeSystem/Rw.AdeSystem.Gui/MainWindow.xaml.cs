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
        }

        private void FluentsTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
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
        }

    }
}
