using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace HotelManager.UI.Behaviors
{
    // реализует перетаскивание окна
    public class DragWindowBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseDown += OnMouseDown;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            // запускает процесс перетаскивания окна, если была зажата левая кнопка мыши
            if (e.ChangedButton == MouseButton.Left)
                Window.GetWindow(AssociatedObject)?.DragMove();
        }
    }
}