using System;
using System.Windows;
using System.Windows.Controls;

namespace CAL25
{
    public partial class MainWindow : Window
    {
        private double currentValue = 0;
        private double lastValue = 0;
        private string currentOperation = "";
        private bool isNewEntry = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string content = button.Content.ToString();

            if (double.TryParse(content, out double number))
            {
                HandleNumberInput(content);
            }
            else
            {
                HandleOperationInput(content);
            }
        }

        private void HandleNumberInput(string number)
        {
            if (isNewEntry)
            {
                Display.Text = number;
                isNewEntry = false;
            }
            else
            {
                Display.Text += number;
            }
        }

        private void HandleOperationInput(string operation)
        {
            switch (operation)
            {
                case "C":
                    ClearAll();
                    break;
                case "CE":
                    ClearEntry();
                    break;
                case "+":
                case "-":
                case "*":
                case "/":
                    ExecutePendingOperation();
                    currentOperation = operation;
                    lastValue = currentValue;
                    isNewEntry = true;
                    break;
                case "=":
                    ExecutePendingOperation();
                    currentOperation = "";
                    break;
                case "sin":
                case "cos":
                case "tan":
                case "√":
                case "x²":
                    PerformFunction(operation);
                    break;
                case ".":
                    AddDecimalPoint();
                    break;
            }
        }

        private void ExecutePendingOperation()
        {
            if (!double.TryParse(Display.Text, out currentValue)) return;

            switch (currentOperation)
            {
                case "+":
                    currentValue = lastValue + currentValue;
                    break;
                case "-":
                    currentValue = lastValue - currentValue;
                    break;
                case "*":
                    currentValue = lastValue * currentValue;
                    break;
                case "/":
                    if (currentValue != 0)
                        currentValue = lastValue / currentValue;
                    else
                    {
                        Display.Text = "Error";
                        return;
                    }
                    break;
            }

            Display.Text = currentValue.ToString();
            isNewEntry = true;
        }

        private void PerformFunction(string function)
        {
            if (!double.TryParse(Display.Text, out currentValue)) return;

            switch (function)
            {
                case "sin":
                    currentValue = Math.Sin(currentValue * Math.PI / 180);
                    break;
                case "cos":
                    currentValue = Math.Cos(currentValue * Math.PI / 180);
                    break;
                case "tan":
                    currentValue = Math.Tan(currentValue * Math.PI / 180);
                    break;
                case "√":
                    if (currentValue >= 0)
                        currentValue = Math.Sqrt(currentValue);
                    else
                    {
                        Display.Text = "Error";
                        return;
                    }
                    break;
                case "x²":
                    currentValue = Math.Pow(currentValue, 2);
                    break;
            }

            Display.Text = currentValue.ToString();
            isNewEntry = true;
        }

        private void ClearAll()
        {
            currentValue = 0;
            lastValue = 0;
            currentOperation = "";
            Display.Text = "0";
        }

        private void ClearEntry()
        {
            Display.Text = "0";
        }

        private void AddDecimalPoint()
        {
            if (!Display.Text.Contains("."))
                Display.Text += ".";
        }
    }
}
