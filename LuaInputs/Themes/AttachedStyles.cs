using System.Windows;
using System.Windows.Media;

namespace REghZyFramework.Themes {
    public static class AttachedStyles {
        public static readonly DependencyProperty SelectedBackgroundProperty = DependencyProperty.RegisterAttached("SelectedBackground", typeof(Brush), typeof(AttachedStyles), new FrameworkPropertyMetadata(Brushes.Transparent));
        public static Brush GetSelectedBackground(DependencyObject target) => (Brush)target.GetValue(SelectedBackgroundProperty);
        public static void SetSelectedBackground(DependencyObject target, Brush value) => target.SetValue(SelectedBackgroundProperty, value);

        public static readonly DependencyProperty SelectedBorderBrushProperty = DependencyProperty.RegisterAttached("SelectedBorderBrush", typeof(Brush), typeof(AttachedStyles), new FrameworkPropertyMetadata(Brushes.Transparent));
        public static Brush GetSelectedBorderBrush(DependencyObject target) => (Brush)target.GetValue(SelectedBorderBrushProperty);
        public static void SetSelectedBorderBrush(DependencyObject target, Brush value) => target.SetValue(SelectedBorderBrushProperty, value);
    }
}