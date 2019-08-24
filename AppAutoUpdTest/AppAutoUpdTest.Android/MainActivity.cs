using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android;
using Xamarin.Essentials;

namespace AppAutoUpdTest.Droid
{
    [Activity(Label = "AppAutoUpdTest", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            var version = AppInfo.VersionString;

            // Application Build Number (1)
            var build = AppInfo.BuildString;

            CheckAppPermissions();

            StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();

            StrictMode.SetVmPolicy(builder.Build());

            builder.DetectFileUriExposure();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        private void CheckAppPermissions()
        {
            if ((int)Build.VERSION.SdkInt < 23)
            {
                return;
            }
            else
            {
                if (PackageManager.CheckPermission(Manifest.Permission.ReadExternalStorage, PackageName) != Permission.Granted
                    && PackageManager.CheckPermission(Manifest.Permission.WriteExternalStorage, PackageName) != Permission.Granted)
                {
                    var permissions = new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage };
                    RequestPermissions(permissions, 1);
                }

                if (PackageManager.CheckPermission(Manifest.Permission.AccessNetworkState, PackageName)
                    != Permission.Granted
                    && PackageManager.CheckPermission(Manifest.Permission.AccessWifiState, PackageName)
                    != Permission.Granted)
                {
                    var permissions = new string[] { Manifest.Permission.AccessNetworkState
                        , Manifest.Permission.AccessWifiState };
                    RequestPermissions(permissions, 1);
                }

                if (PackageManager.CheckPermission(Manifest.Permission.InstallPackages, PackageName) != Permission.Granted)
                {
                    var permissions = new string[]
                    {
                        Manifest.Permission.InstallPackages, Manifest.Permission.InstallPackages
                    };

                    RequestPermissions(permissions, 1);
                }
            }
        }
    }
}

