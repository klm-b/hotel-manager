using System;
using System.Windows;
using System.Windows.Data;

namespace HotelManager.UI.Behaviors
{
    // метод расширения, для получения значения свойства зависимостей 
    public static class BindingMixins
    { 
        private static readonly DependencyProperty EvalProperty = DependencyProperty.RegisterAttached(
            "Eval", typeof(object), typeof(BindingMixins), new PropertyMetadata(default(object)));

        public static T Evaluate<T>(this BindingBase binding, DependencyObject target)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            if (target == null) throw new ArgumentNullException(nameof(target));

            BindingOperations.SetBinding(target, EvalProperty, binding);

            return target.GetValue(EvalProperty) is T value ? value : default;
        }
    }
}