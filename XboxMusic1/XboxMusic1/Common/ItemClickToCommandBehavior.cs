using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace XboxMusic1.Common
{
    public static class ItemClickToCommandBehavior
    {
        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand),
            typeof(ItemClickToCommandBehavior),
            new PropertyMetadata(null, OnCommandChanged));

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ListViewBase lvb = d as ListViewBase;
            if (lvb == null) return;

            lvb.ItemClick += OnClick;
        }

        private static void OnClick(object sender, ItemClickEventArgs e)
        {
            ListViewBase lvb = sender as ListViewBase;
            ICommand cmd = lvb.GetValue(ItemClickToCommandBehavior.CommandProperty) as ICommand;
            if (cmd != null && cmd.CanExecute(e.ClickedItem))
                cmd.Execute(e.ClickedItem);
        }
    }
}
