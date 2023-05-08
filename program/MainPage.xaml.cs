namespace program;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
    private void CheckInternet(object sender, EventArgs e)
	{
		var hasInternet = Connectivity.Current.NetworkAccess == NetworkAccess.Internet;

		DisplayAlert("Has internet?", $"{hasInternet}", "OK");
	}
}

