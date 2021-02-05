using System.Linq;
using System.Windows;
using System.Windows.Data;
using Microsoft.Xaml.Behaviors;

namespace HotelManager.UI.Behaviors
{
    // вспомогательный класс, для создания привязки свойств, не являющихся свойствами зависимостей
    public class TemplateBindingBehavior : Behavior<DependencyObject>
    {
        public PropertyPath TargetTemplatePropertyPath { get; set; }

        public PropertyPath SourceProperty { get; set; }

        protected override void OnAttached()
        {
            var targetBinding = new Binding { RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent) };
            if (TargetTemplatePropertyPath != null && TargetTemplatePropertyPath.PathParameters.Count > 1)
            {
                var parameters = TargetTemplatePropertyPath.PathParameters
                    .Take(TargetTemplatePropertyPath.PathParameters.Count - 1)
                    .ToArray();
                string path = string.Join(".",
                    Enumerable.Range(0, parameters.Length).Select(x => $"({x})"));
                targetBinding.Path = new PropertyPath(path, parameters);
            }

            base.OnAttached();
            var target = targetBinding.Evaluate<DependencyObject>(this);

            if (target != null)
            {
                BindingOperations.SetBinding(target, (DependencyProperty)TargetTemplatePropertyPath.PathParameters.Last(), new Binding
                {
                    Path = SourceProperty,
                    Source = AssociatedObject,
                    Mode = BindingMode.OneWay
                });
            }
        }
    }
}