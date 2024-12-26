using TekyslabDemo.Views;
namespace TekyslabDemo
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ItemDetailsPage), typeof(ItemDetailsPage));
            Routing.RegisterRoute(nameof(ItemPicturePage), typeof(ItemPicturePage));
        }
    }
}
