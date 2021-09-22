using AutoClicker.scripts;
using DesktopWPFAppLowLevelKeyboardHook;
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
using System.Text.RegularExpressions;

namespace AutoClicker
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Utils utils;
        private LowLevelKeyboardListener listener;
        private AutoClick click;

        public MainWindow()
        {
            InitializeComponent();
            this.utils = new Utils(this);
        }

        private void HotKeyButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (!utils.canChangeHotKey)
            {
                utils.HotKeyChange(true);
                HotKeyButton.Content = e.Key;
            }

        }
        private void HotKeyButton_Click(object sender, RoutedEventArgs e)
        {
            if (utils.canChangeHotKey)
            {
                utils.HotKeyChange(false);
                HotKeyButton.Content = "Click!";
            }

        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (!AutoClick.isRunning)
            {
                click = new AutoClick(Double.Parse(ValueTextBox.Text), HotKeyButton.Content.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listener = new LowLevelKeyboardListener();
            listener.OnKeyPressed += Listener_OnKeyPressed;

            listener.HookKeyboard();
        }

        private void Listener_OnKeyPressed(object sender, KeyPressedArgs e)
        {
            if (click == null)
            {
                return;
            }

            if (e.KeyPressed.ToString().Equals(click.key))
            {
                switch (AutoClick.isRunning)
                {
                    case true:
                        click.Stop();
                        break;
                    case false:
                        click.Start();
                        break;
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listener.UnHookKeyboard();
            click.Stop();
        }

        private void ValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isNumeric = int.TryParse(ValueTextBox.Text, out int n);

            if (!isNumeric)
            {
                ValueTextBox.Text = Regex.Replace(ValueTextBox.Text, "[^0-9.]", "");
            }
        }
    }
}
