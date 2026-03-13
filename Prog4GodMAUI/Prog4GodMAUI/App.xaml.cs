namespace Prog4GodMAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }


        //protected override Window CreateWindow(IActivationState? activationState)
        //{
        //    return new Window(new AppShell());
        //}

        //starting with MainPage instead of AppShell
        //public App()
        //{
        //    InitializeComponent();
        //    MainPage = new MainPage();
        //}
    }
}