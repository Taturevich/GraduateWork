using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace AimlBotUI.Infrastructure.Behaviours
{
    public class TextBoxFocusBehavior : Behavior<TextBox>
    {
        public static readonly DependencyProperty IsWatermarkEnabled =
            DependencyProperty.RegisterAttached("IsWatermarkEnabled",
                typeof(bool), typeof(TextBoxFocusBehavior),
                new UIPropertyMetadata(false, OnIsWatermarkEnabled));

        public static readonly DependencyProperty WatermarkText =
            DependencyProperty.RegisterAttached("WatermarkText",
                typeof(string), typeof(TextBoxFocusBehavior),
                new UIPropertyMetadata(string.Empty, OnWatermarkTextChanged));

        private static void OnWatermarkTextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb != null)
            {
                tb.Text = (string)e.NewValue;
            }
        }

        public static string GetWatermarkText(DependencyObject obj)
        {
            return (string)obj.GetValue(WatermarkText);
        }


        private static void OnInputTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            var tb = e.OriginalSource as TextBox;
            if (tb != null)
            {
                if (string.IsNullOrEmpty(tb.Text))
                    tb.Text = GetWatermarkText(tb);
            }
        }

        private static void OnInputTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            var tb = e.OriginalSource as TextBox;
            if (tb != null)
            {
                if (tb.Text == GetWatermarkText(tb))
                {
                    tb.Text = string.Empty;
                }
            }
        }

        private static void OnIsWatermarkEnabled(object sender, DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                bool isEnabled = (bool)e.NewValue;
                if (isEnabled)
                {
                    tb.GotFocus += OnInputTextBoxGotFocus;
                    tb.LostFocus += OnInputTextBoxLostFocus;
                }
                else
                {
                    tb.GotFocus -= OnInputTextBoxGotFocus;
                    tb.LostFocus -= OnInputTextBoxLostFocus;
                }
            }
        }
    }
}
