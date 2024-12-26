using TekyslabDemo.ViewModels;

namespace TekyslabDemo.Views;

public partial class ItemDetailsPage : ContentPage
{
	public ItemDetailsPage(ItemDetailsViewmodel item)
	{
		InitializeComponent();
		this.BindingContext = item;
        SetDynamicWidth();

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await (this.BindingContext as ItemDetailsViewmodel).RefreshImageData();
    }

    private void SetDynamicWidth()
    {
        var displayInfo = DeviceDisplay.MainDisplayInfo;
        var screenWidth = displayInfo.Width / displayInfo.Density;
        widthrequest.WidthRequest = screenWidth *1.05; 
    }


}