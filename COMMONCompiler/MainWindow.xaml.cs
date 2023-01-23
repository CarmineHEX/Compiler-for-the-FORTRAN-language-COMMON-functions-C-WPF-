using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace COMMONCompiler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Grammar g = new Grammar();
        private TaskWindow task;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InputLine_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void InputLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter && InputLine.Text!="")
            {
                TextList.Items.Add(InputLine.Text);
                InputLine.Text = "";
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            COMMONCompiler.Grammar.listTextbox.Clear();
            COMMONCompiler.Grammar.line = 1;
            ErrorList.Items.Clear();
            g.GetError(TextList, ErrorList);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            COMMONCompiler.Grammar.line = 1;
            COMMONCompiler.Grammar.listTextbox.Clear();
            TextList.Items.Clear();
            ErrorList.Items.Clear();
        }

        private void Task_Click(object sender, RoutedEventArgs e)
        {
            task = new TaskWindow();
            task.Show();
        }
    }
}
