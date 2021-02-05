using System.Windows;

namespace HotelManager.UI.Behaviors
{
    // вспомогательный класс, для настройки MaterialDesignWindow
    // позволяет задавать значения свойств ShowMinimizeButton и ShowMaximizeButton,
    // отвечающих за отображение кнопок разворачивания и сворачивания окна
    public static class WindowEx
    {
        public static readonly DependencyProperty ShowMinimizeButtonProperty = DependencyProperty.RegisterAttached(
            "ShowMinimizeButton", typeof(bool), typeof(WindowEx), new PropertyMetadata(true));

        public static readonly DependencyProperty ShowMaximizeButtonProperty = DependencyProperty.RegisterAttached(
            "ShowMaximizeButton", typeof(bool), typeof(WindowEx), new PropertyMetadata(true));

        public static void SetShowMinimizeButton(DependencyObject element, bool value)
            => element.SetValue(ShowMinimizeButtonProperty, value);
        

        public static bool GetShowMinimizeButton(DependencyObject element)
            => (bool) element.GetValue(ShowMinimizeButtonProperty);
        

        public static void SetShowMaximizeButton(DependencyObject element, bool value)
            => element.SetValue(ShowMaximizeButtonProperty, value);
        

        public static bool GetShowMaximizeButton(DependencyObject element)
            => (bool) element.GetValue(ShowMaximizeButtonProperty);
    }
}