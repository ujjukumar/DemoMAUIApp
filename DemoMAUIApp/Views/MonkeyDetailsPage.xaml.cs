namespace DemoMAUIApp.Views;

public partial class MonkeyDetailsPage : ContentPage
{
	public MonkeyDetailsPage(MonkeyDetailsViewModel viewModel)
	{
        InitializeComponent();

        BindingContext = viewModel;
	}
}