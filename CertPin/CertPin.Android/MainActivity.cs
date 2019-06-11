using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using FormsAppCompatActivity = Xamarin.Forms.Platform.Android.FormsAppCompatActivity;

namespace CertPin.Droid
{
    [Activity(Label = "CertPin",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);

            try
            {
                LoadApplication(new App());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}