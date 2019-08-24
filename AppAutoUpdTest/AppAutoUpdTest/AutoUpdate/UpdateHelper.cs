using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using Android.Content;
using Android.OS;
using Android.Content.PM;
using Android;

namespace AppAutoUpdTest.AutoUpdate
{
    public class UpdateHelper
    {
        string fileName = "com.companyname.TestAutoUpd.Android.apk";
        string fullFilename = "";
        FileStream fooStream;
        public Stream DownloadAPKAsStream(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            return stream;
        }

        public void GenApkFileAndDoUpdate(Stream downloadStream)
        {
            var DownlaodPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).ToString();
            //DownlaodPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            fullFilename = Path.Combine(DownlaodPath, fileName);

            if (File.Exists(fullFilename) == false)
            {
                Directory.CreateDirectory(DownlaodPath);
                fooStream = File.Create(fullFilename);
            }
            else
            {
                fooStream = File.OpenWrite(fullFilename);
            }


            //DownlaodPath = Android.OS.Environment.DirectoryDownloads;
            using (FileStream fs = fooStream)
            {                
                downloadStream.CopyTo(fs);

                InstallAPK();
            }
        }

        public void InstallAPK()
        {
            var context = Android.App.Application.Context;            

           Intent install = new Intent(Intent.ActionInstallPackage);

            install.SetDataAndType(Android.Net.Uri.FromFile(new Java.IO.File(fullFilename)), "application/vnd.android.package-archive");
            install.SetFlags(ActivityFlags.ClearTask | ActivityFlags.NewTask);
            install.AddFlags(ActivityFlags.GrantReadUriPermission);
            context.StartActivity(install);
        }        
    }
}
