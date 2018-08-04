using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Prism;
using Prism.Ioc;
using SimpleLifeCounterForY.Models;

namespace SimpleLifeCounterForY.Droid
{
    [Activity(Label = "SimpleLifeCounterForY", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            // 横画面に固定
            this.RequestedOrientation = ScreenOrientation.SensorLandscape;

            // スリープモードにしない制御
            this.Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));

            //var intent = new Intent(Intent.ActionOpenDocument);
            //intent.AddCategory(Intent.CategoryOpenable);
            //intent.SetType("image/*");
            //StartActivity(intent);
        }

        // 以下コピペ
        public event EventHandler<PreferenceManager.ActivityResultEventArgs> ActivityResult = delegate { };
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // resultEventArgsの第1引数(Handled)は、ハンドル後に特に使う用事がなければtrueでもfalseでも問題なさそう
            var resultEventArgs = new PreferenceManager.ActivityResultEventArgs(true, requestCode, resultCode, data);
            ActivityResult(this, resultEventArgs);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
            container.Register<IFileIO, FileIo>();
            // Register any platform specific implementations
        }
    }
}

