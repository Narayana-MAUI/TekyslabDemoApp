using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekyslabDemo.Models;
using TekyslabDemo.Database;
using CommunityToolkit.Maui.Views;
using static SQLite.SQLite3;

namespace TekyslabDemo.ViewModels
{
    public partial class ItemPictureViewModel : ObservableObject
    {
        [ObservableProperty]
        private ImageSource _userImage;
        [ObservableProperty]
        private string _fileName;
        [ObservableProperty]
        private bool _isItemVisible;
        [ObservableProperty]
        private bool _isItemLoading;

        public string base64Image;
        public string fileName;

        

        public ItemPictureViewModel()
        {

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    base64Image = Preferences.Default.Get("PickerImage", "");
                    byte[] base64Stream = Convert.FromBase64String(base64Image);
                    UserImage = ImageSource.FromStream(() => new MemoryStream(base64Stream));

                    fileName = Preferences.Default.Get("Filename", "");
                    string result = fileName.Replace(".jpg", string.Empty);
                    FileName = result;
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("", ex.Message, "Ok");
                }
            });
                    
            
            

        }

        [RelayCommand]
        public async Task BackButton()
        {
            try
            {
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("", ex.Message, "Ok");
            }
            
        }

        [RelayCommand]
        public async Task SaveImages()
        {
            try
            {
                SaveImage imageData = new SaveImage();
                imageData.Base64Image = base64Image;
                imageData.ImageName = FileName;

                int i = new DBConnection().SetImagesData(imageData).Result;
                if (i > 0)
                {
                    {
                         var result = await Shell.Current.DisplayAlert("","Image saved succesfully","Yes","Cancel");
                        if ((bool)result)
                      {
                            await Shell.Current.GoToAsync("..");
                        }
                        
                    }

                }
                else
                {
                    await Shell.Current.DisplayAlert("", "Something went wrong", "Ok");
                }
            }
            catch (Exception ex) 
            {
                await Shell.Current.DisplayAlert("", ex.Message, "Ok");
            }
           
        }
    }
}
