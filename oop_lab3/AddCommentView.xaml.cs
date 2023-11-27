namespace oop_lab3;

public partial class AddComment : ContentPage
{
    public event EventHandler<string> InputValuesSubmitted;
    public AddComment()
	{
		InitializeComponent();
	}

    private void OnSubmitClicked(object sender, EventArgs e)
    {
        InputValuesSubmitted?.Invoke(this, $"{author.Text},{content.Text}");
        Application.Current.CloseWindow(this.Window);
    }
}