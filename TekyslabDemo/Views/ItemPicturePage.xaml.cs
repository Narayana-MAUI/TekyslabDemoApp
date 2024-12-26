using TekyslabDemo.ViewModels;

namespace TekyslabDemo.Views;

public partial class ItemPicturePage : ContentPage
{
	public ItemPicturePage()
	{
		InitializeComponent();
		this.BindingContext = new ItemPictureViewModel();
        SetDynamicWidth();

    }

    private void SetDynamicWidth()
    {
        var displayInfo = DeviceDisplay.MainDisplayInfo;
        var screenWidth = displayInfo.Width / displayInfo.Density;
        widthrequest.WidthRequest = screenWidth * 1.05; // Set width to 80% of screen width
    }
}