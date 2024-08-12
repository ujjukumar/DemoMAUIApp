
namespace DemoMAUIApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

#if WINDOWS

        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);

            window.X = 1400;
            window.Y = 100;

            window.Width = 450;
            window.Height = 900;

            return window;
        }
#endif
    }
}
