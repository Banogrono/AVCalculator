using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using AVCalculator.Controller;

namespace AVCalculator.View
{
    public class MainWindow : Window
    {
        public MainWindowController
            MwController; // This is a reference, not an actual instance - check out MainWindow constructor in Program.cs to get how I did this.  

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        /*
         * Here is where magic happens -> Keyboard handing with Avalonia!
         */
        private void InputElement_OnKeyUp(object? sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D0 or Key.NumPad0:
                {
                    MwController.Button0_Click();
                    break;
                }
                case Key.D1 or Key.NumPad1:
                {
                    MwController.Button1_Click();
                    break;
                }
                case Key.D2 or Key.NumPad2:
                {
                    MwController.Button2_Click();
                    break;
                }
                case Key.D3 or Key.NumPad3:
                {
                    MwController.Button3_Click();
                    break;
                }
                case Key.D4 or Key.NumPad4:
                {
                    MwController.Button4_Click();
                    break;
                }
                case Key.D5 or Key.NumPad5:
                {
                    MwController.Button5_Click();
                    break;
                }
                case Key.D6 or Key.NumPad6:
                {
                    MwController.Button6_Click();
                    break;
                }
                case Key.D7 or Key.NumPad7:
                {
                    MwController.Button7_Click();
                    break;
                }
                case Key.D8 or Key.NumPad8:
                {
                    MwController.Button8_Click();
                    break;
                }
                case Key.D9 or Key.NumPad9:
                {
                    MwController.Button9_Click();
                    break;
                }
                case Key.Add:
                {
                    MwController.ButtonPlus_Click();
                    break;
                }
                case Key.Subtract:
                {
                    MwController.ButtonMinus_Click();
                    break;
                }
                case Key.Divide:
                {
                    MwController.ButtonDivide_Click();
                    break;
                }
                case Key.Multiply:
                {
                    MwController.ButtonMultiply_Click();
                    break;
                }
                case Key.OemPeriod:
                {
                    MwController.ButtonDot_Click();
                    break;
                }
                case Key.Enter:
                {
                    MwController.ButtonEquals_Click();
                    break;
                }
                case Key.Back:
                {
                    MwController.RemoveLastDigit();
                    break;
                }
            }
        }
    }
}