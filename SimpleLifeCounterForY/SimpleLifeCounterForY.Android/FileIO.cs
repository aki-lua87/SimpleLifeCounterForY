using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Preferences;
using Android.Provider;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using SimpleLifeCounterForY.Models;
using SimpleLifeCounterForY.ViewModels;
using Xamarin.Forms;

namespace SimpleLifeCounterForY.Droid
{
    public class FileIo :IFileIO
    {
        private MainActivity mainActivity;
        // private Android.Net.Uri url;
        string url;
        private bool ok = false;
        private ClipData clipData;
        // private string filePath;

        public FileIo()
        {
            mainActivity = Forms.Context as MainActivity;
            mainActivity.ActivityResult += HandleActivityResult;
        }

        public string ShowImageList()
        {
            ok = false;

            //var intent = new Intent(Intent.ActionOpenDocument);
            //intent.AddCategory(Intent.CategoryOpenable);
            //intent.SetType("image/*");
            //mainActivity.StartActivityForResult(intent, 334);

            //ギャラリーを表示する
            var imageIntent = new Intent();
            imageIntent.SetType("image/*");
            imageIntent.SetAction(Intent.ActionGetContent);
            mainActivity.StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), 334);

            return url;
        }

        public void LoadImage()
        {
            throw new NotImplementedException();
        }

        public void SaveImage()
        {
            throw new NotImplementedException();
        }

        private void HandleActivityResult(object sender, PreferenceManager.ActivityResultEventArgs args)
        {
            // SOME_REQUEST_CODEは、アクティビティ呼び出しのときに渡した任意のリクエストコード。
            if (args.RequestCode == 334)
            {
                if (args.ResultCode == Result.Ok)
                {
                    string filePath = String.Empty;
                    ICursor cur = null;
                    try
                    {
                        // 選択した画像のパスを取得する
                        string[] columns = { Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data };
                        cur = mainActivity.ContentResolver.Query(args.Data.Data, columns, null, null, null);
                        if (cur != null)
                        {
                            foreach (var s in columns)
                            {
                                MainPageViewModel.SetPickupImageSource(s);
                            }
                            while (cur.MoveToNext())
                            {
                                filePath = cur.GetString(0);
                                MainPageViewModel.SetPickupImageSource(filePath);
                            }
                        }
                        this.url = filePath;
                    }
                    finally
                    {
                        if (cur != null)
                        {
                            cur.Close();
                        }
                    }
                }
            }
        }

        //イメージギャラリーの戻り値から画像のファイルパスを取得する
        private string GetSelectedFilePath(Intent data)
        {
            string filePath = String.Empty;
            ICursor cur = null;
            try
            {
                // 選択した画像のパスを取得する
                string[] columns = { Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data };
                cur = mainActivity.ContentResolver.Query(data.Data, columns, null, null, null);
                if (cur != null &&
                    cur.MoveToFirst())
                {
                    filePath = cur.GetString(0);
                    MainPageViewModel.SetPickupImageSource(filePath);
                }
                return filePath;
            }
            finally
            {
                if (cur != null)
                {
                    cur.Close();
                }
            }
        }


    }
}