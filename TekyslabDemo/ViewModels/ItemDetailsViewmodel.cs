using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TekyslabDemo.Views;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekyslabDemo.Models;
using TekyslabDemo.Database;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Storage;
using TekyslabDemo.Services.IService;
using CommunityToolkit.Maui.Views;
using TekyslabDemo.Models.ApiResponseModels;
using TekyslabDemo.Services.Service;

namespace TekyslabDemo.ViewModels
{
    public partial class ItemDetailsViewmodel : ObservableObject
    {
        [ObservableProperty]
        private bool _isOpenBottomsheet;

        [ObservableProperty]
        private bool _isItemsVisible;
        [ObservableProperty]
        private bool _isItemsLoading;



        public readonly DBConnection _userDatabase;
        public readonly IImageDataService _imageDataService;

        public ICommand DeleteImageCommand { get; set; }

        public ObservableCollection<DisplayImages> DisplayingImages { get; set; } = new ObservableCollection<DisplayImages>();

        public ItemDetailsViewmodel(DBConnection userDatabase, IImageDataService imageDataService)
        {
            _userDatabase = userDatabase;
            _imageDataService = imageDataService;
            DeleteImageCommand = new Command<Object>(async (Object) => await DeleteImage(Object));
            
        }

        public async Task RefreshImageData()
        {
            
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    IsItemsVisible = false;
                    IsItemsLoading = true;

                    List<SaveImage> images = await _userDatabase.GetImagesData();
                    DisplayingImages.Clear();
                    foreach (SaveImage savedImage in images)
                    {
                        byte[] base64Stream = Convert.FromBase64String(savedImage.Base64Image);
                        var image = ImageSource.FromStream(() => new MemoryStream(base64Stream));
                        DisplayImages displayImages = new DisplayImages();
                        displayImages.Base64String = savedImage.Base64Image;
                        displayImages.Image = image;
                        displayImages.Id = savedImage.Id;
                        displayImages.ImageName = savedImage.ImageName;
                        DisplayingImages.Add(displayImages);
                    }
                    IsItemsVisible = true;
                    IsItemsLoading = false;
                }
                catch (Exception ex) 
                {
                    await Shell.Current.DisplayAlert("", ex.Message, "Ok");
                }
                
            });
            
        }

        [RelayCommand]
        public async Task Capture()
        {
            IsOpenBottomsheet = true;
            
        }

        //implemented the code for the way to call Api's

        [RelayCommand]
        public async Task Submit()
        {
            try
            {
                
                    RequestImagesModel imagesModel = new RequestImagesModel();
                    imagesModel.Name = "Narayana"; //base64 string for image
                    imagesModel.Job = "Maui/ Xamarin developer"; //image name
                    ResponseMessage response = await ImageDataService.PostImageData(_imageDataService, imagesModel);
                    await Shell.Current.DisplayAlert("", "The response that is coming from api \n" + response.Name + " \n" + response.Job + "\n " + response.UpdatedAt + " ", "Ok");

                   /* if (200 == 200)
                    {
                        // Do the openration on success of response
                    }*/
                    /*else if (208 == 208)
                    {
                        // do operation on unsuccess message from response
                    }
                    else
                    {
                        //do operations in else part
                    }*/
                
            }
            catch (Exception ex) 
            {
                await Shell.Current.DisplayAlert("", ex.Message, "Ok");
            }
           
        }

        [RelayCommand]
        public async Task OpenCamera()
        {
            try
            {
                FileResult result = await MediaPicker.Default.CapturePhotoAsync();
                if (result == null)
                {
                    return;
                }
                var fileName = result.FileName;
                Stream stream = await result.OpenReadAsync();
                var bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, (int)stream.Length);
                 var base64 = System.Convert.ToBase64String(bytes);
                Preferences.Default.Set("Filename", fileName);
                Preferences.Default.Set("PickerImage", base64);
               
                await Shell.Current.GoToAsync($"{nameof(ItemPicturePage)}");

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("", ex.Message, "Ok");
            }
            IsOpenBottomsheet = false;
            

        }

        [RelayCommand]
        public async Task OpenGallery()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync();
                {
                    if (result == null)
                    {
                        return;
                    }
                    var fileName = result.FileName;
                    using Stream stream = await result.OpenReadAsync();
                    var bytes = new byte[stream.Length];
                    await stream.ReadAsync(bytes, 0, (int)stream.Length);
                    var base64 = System.Convert.ToBase64String(bytes);
                    Preferences.Default.Set("Filename", fileName);
                    Preferences.Default.Set("PickerImage", base64);
                    
                    await Shell.Current.GoToAsync($"{nameof(ItemPicturePage)}");

                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("", ex.Message, "Ok");
            }
            IsOpenBottomsheet = false;

        }

        private async Task DeleteImage(object obj)
        {
            try
            {
                var data = (obj as DisplayImages);


                SaveImage imageData = new SaveImage();
                imageData.Base64Image = data.Base64String;
                imageData.Id = data.Id;
                imageData.ImageName = data.ImageName;

                int i = new Database.DBConnection().DeleteImageData(imageData).Result;
                if (i > 0)
                {
                    await RefreshImageData();
                    await Shell.Current.DisplayAlert("", "Image deleted Succesfully", "Ok");
                }
                else
                {
                    await Shell.Current.DisplayAlert("", "Something went wrong", "Ok");
                }
            }
            catch (Exception ex)
            {

            }
            
        }
    }
}
