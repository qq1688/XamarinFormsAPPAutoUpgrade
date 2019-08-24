using System;
using Xamarin.Forms;
using AppAutoUpdTest.AutoUpdate;

namespace AppAutoUpdTest
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        private async void updateAPP_Clicked(object sender, EventArgs e)
        {
            //add your version checking here
            //version num added in MainActivity

            await DisplayAlert("", "Auto download new version is running, please wait for the prompt out dialog to install new version", "OK");

            var updHelper = new UpdateHelper();

            var apkFileStream = updHelper.DownloadAPKAsStream(@"http://192.168.0.105:8000/com.companyname.TestAutoUpd.Android.apk");  //please change the link when you testing
            updHelper.GenApkFileAndDoUpdate(apkFileStream);            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
