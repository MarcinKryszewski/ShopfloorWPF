﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Shopfloor.Controls.PlaceholderTextBox
{
    internal sealed class PlaceholderTextBoxControl : TextBox
    {
        public static readonly DependencyProperty IsEmptyProperty = _isEmptyPropertyKey.DependencyProperty;
        public static readonly DependencyProperty PlaceholderColorProperty =
            DependencyProperty.Register("PlaceholderColor", typeof(SolidColorBrush), typeof(PlaceholderTextBoxControl), new PropertyMetadata(new SolidColorBrush(Colors.Black)));
        public static readonly DependencyProperty PlaceholderProperty =
                                    DependencyProperty.Register("Placeholder", typeof(string), typeof(PlaceholderTextBoxControl), new PropertyMetadata(string.Empty));
        private static readonly DependencyPropertyKey _isEmptyPropertyKey =
                                DependencyProperty.RegisterReadOnly("IsEmpty", typeof(bool), typeof(PlaceholderTextBoxControl), new PropertyMetadata(false));
        static PlaceholderTextBoxControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceholderTextBoxControl), new FrameworkPropertyMetadata(typeof(PlaceholderTextBoxControl)));
        }
        public bool IsEmpty
        {
            get { return (bool)GetValue(IsEmptyProperty); }
            private set { SetValue(_isEmptyPropertyKey, value); }
        }
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }
        public SolidColorBrush PlaceholderColor
        {
            get { return (SolidColorBrush)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }
        protected override void OnInitialized(EventArgs e)
        {
            UpdateIsEmpty();
            base.OnInitialized(e);
        }
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            UpdateIsEmpty();
            base.OnTextChanged(e);
        }
        private void UpdateIsEmpty()
        {
            IsEmpty = string.IsNullOrEmpty(Text);
        }
    }
}