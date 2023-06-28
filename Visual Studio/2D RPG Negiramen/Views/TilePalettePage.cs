namespace _2D_RPG_Negiramen.Views;

public class TilePalettePage : ContentPage
{
	public TilePalettePage()
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}
			}
		};
	}
}