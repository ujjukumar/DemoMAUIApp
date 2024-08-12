namespace DemoMAUIApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MonkeyDetailsPage), typeof(MonkeyDetailsPage));
        }
    }
}
