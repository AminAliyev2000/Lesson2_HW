using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Lesson2_HW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ButtonOperations _currentOperation;
        private bool _myInput1 = true;
        private bool _textchanged;
        private bool _done;
        public MainWindow()
        {
            InitializeComponent();
            _currentOperation = new ButtonOperations();
        }
        private void AddValueTxtbox(string value)
        {
            if (value != "," && txtbox.Text == "0")
                txtbox.Text = String.Empty;

            var temp = txtbox.Text + value;
            txtbox.Text = temp.ToString(CultureInfo.InvariantCulture);
        }
        private void RemoveLastValueFromTextBox()
        {
            if (string.IsNullOrWhiteSpace(txtbox.Text))
                return;

            txtbox.Text = txtbox.Text.Remove(txtbox.Text.Length - 1, 1);
        }
        private void ClearInput()
        {
            txtbox.Text = "0";
        }
        private void DoOperation(string oprtype)
        {
            if (String.IsNullOrWhiteSpace(txtbox.Text))
                return;

            if (String.IsNullOrWhiteSpace(_currentOperation.FirstValue))
            {
                _currentOperation.FirstValue = txtbox.Text;
                _myInput1 = true;
            }
            else if (!_done && String.IsNullOrWhiteSpace(_currentOperation.SecondValue))
            {
                Calculate();
            }

            _currentOperation.OperationType = _currentOperation.GetOperationType(oprtype);
        }
        private void Calculate()
        {
            if (!String.IsNullOrWhiteSpace(_currentOperation.Result))
            {
                _currentOperation.FirstValue = _currentOperation.Result;
            }

            _currentOperation.SecondValue = txtbox.Text;

            var result = String.Empty;

            try
            {
                result = _currentOperation.Calculate();
            }
            catch (Exception ex)
            {
                result = $"Error: {ex.Message}";
            }

            _currentOperation.Result = result;

            _currentOperation.SecondValue = String.Empty;

            txtbox.Text = _currentOperation.Result;

            _done = true;
            _myInput1 = true;
        }
        private void txtbox_textchanged(object sender, TextChangedEventArgs e)
        {
            _textchanged = true;
            _done = false;
        }
        private void NumreicButtons_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button))
                return;

            if (_myInput1)
            {
                _textchanged = false;
                _myInput1 = false;
                ClearInput();
            }

            AddValueTxtbox(button.Content.ToString());
        }
        private void OperatorChooseButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button))
                return;

            DoOperation(button.Content.ToString());
        }
        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (_done)
                return;
            RemoveLastValueFromTextBox();
            txtbox.Focus();
        }
        private void CEbutton_Click(object sender, RoutedEventArgs e)
        {
            ClearInput();
        }
        private void Cbutton_Click(object sender, RoutedEventArgs e)
        {
            ClearInput();
            _currentOperation.Clear();
        }
        private void plusminusbutton_Click(object sender, RoutedEventArgs e)
        {
            if (txtbox.Text == "0")
                return;

            if (_done)
                return;
            var newValue = txtbox.Text.StartsWith("-") ?
                txtbox.Text.Remove(0, 1) :
                txtbox.Text.Insert(0, "-");

            txtbox.Text = newValue.ToString(CultureInfo.InvariantCulture);
        }
        private void SquareRootButton_Click(object sender, RoutedEventArgs e)
        {
            double number;
            var isDouble = double.TryParse(txtbox.Text, out number);
            if (isDouble)
            {
              txtbox.Text =
                    string.Format(
                        "{0}{1} = {2}",
                        "\u221A",
                        this.txtbox.Text,
                        Math.Round(Math.Sqrt(number), 2));
            }
        }
        private void equalbutton_Click(object sender, RoutedEventArgs e)
        {
            if (!_done)
                Calculate();
        }
        private void dotbutton_Click(object sender, RoutedEventArgs e)
        {
            if (!txtbox.Text.Contains(","))
            {
                AddValueTxtbox(",");
            }
        }
    }
}
