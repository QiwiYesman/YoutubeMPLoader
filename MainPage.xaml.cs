using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using YoutubeExplode.Videos.Streams;

namespace MauiApp1;

public partial class MainPage : ContentPage
{

    YoutubeExploder downloader;
    FileNameStruct fileName;
    public MainPage()
	{
		InitializeComponent();
        downloader = new() { progress=progress};
        fileName = new();

    }

	async private void OnPaste(object sender, EventArgs e)
	{
		link_input.Text = await Clipboard.GetTextAsync();
	}

    private void OnDownload(object sender, EventArgs e)
    {
        download();
    }
   
    void progress(double value)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            progressBar.Progress = value;
        });
    }

    int getResolution()
    {
        foreach(var button in new[] { radio_360, radio_480, radio_720 })
        {
            if(button.IsChecked)
            {
                return int.Parse(button.Text.Replace("p", ""));
            }
        }
        return 720;
    }
    void message(string text, bool clear=true)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if(clear)
            {
                label.Text = "";
            }
            label.Text += text;
        });
    } 

    async Task<bool> GetPermissionsAsync()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
        if(status != PermissionStatus.Granted)
        {
            await Permissions.RequestAsync<Permissions.StorageWrite>();
        }
        status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.StorageRead>();
        }
         
        return status == PermissionStatus.Granted;
    }
  
    string getDownloadFolder()
    {
        return "/storage/emulated/0/Download";
    }
    void switchDownloadDisabled()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            download_button.IsEnabled = !download_button.IsEnabled;

        });
    }
    void SwitchVisibleResolutionGroup(bool value)
    {
        ResolutionGroup.IsVisible = value;
    }
    IStreamInfo GetChosenStream(StreamManifest manifest)
    {
        if(mp4_radio.IsChecked)
        {
            return downloader.GetVideoStream(manifest, getResolution());
        }
        else
        {
            return downloader.GetAudioStream(manifest);
        }
    }
    async void download()
    {
        if (await GetPermissionsAsync())
        {
            try {
                string link = link_input.Text;
                message("Getting info...");
                var info = await downloader.GetVideoInfoAsync(link);
                var manifest = await downloader.GetManifestAsync(link);
                var stream = GetChosenStream(manifest);

                fileName.Title = info.Title;
                fileName.Extension = stream.Container.Name;
                fileName.Folder = getDownloadFolder();
                new Task(async () =>
                {
                    message("Downloading...");
                    switchDownloadDisabled();
                    await downloader.DownloadStreamAsync(stream,
                        fileName.FullPath
                        );
                    switchDownloadDisabled();
                    if (mp3_radio.IsChecked)
                    {
                        message("Converting...");
                        await downloader.ConvertToMP3(fileName.FullPath, fileName.FullPathWithoutExt + ".mp3");
                        downloader.removeVideo(fileName.FullPath);
                    }
                    message("Downloaded");
                }).Start();
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
                reset();
            }
            }        
    }
    void reset()
    {
        download_button.IsEnabled = true;
        message("Try again");
    }



}

