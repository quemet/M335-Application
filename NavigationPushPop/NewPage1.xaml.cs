namespace NavigationPushPop;

public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewPage2());
    }
}