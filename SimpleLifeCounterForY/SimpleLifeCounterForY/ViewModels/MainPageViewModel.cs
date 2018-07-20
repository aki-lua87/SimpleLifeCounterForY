using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prism.Services;
using SimpleLifeCounterForY.Models;
using Xamarin.Forms;

namespace SimpleLifeCounterForY.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private IFileIO FileIO { get; }
        private readonly IPageDialogService _pageDialogService;

        private static string filePath;


        private ImageSource _backgtoundImage;
        public ImageSource BackgtoundImage
        {
            get { return this._backgtoundImage; }
            set { this.SetProperty(ref this._backgtoundImage, value); }
        }

        // public ImageSource Source { get; set; }

        private Stack<(int, int)> UndoStack = new Stack<(int, int)>();

        private int _left;
        public int LeftLifePoint
        {
            get { return this._left; }
            set { this.SetProperty(ref this._left, value); }
        }

        private int _right;
        public int RightLifePoint
        {
            get { return this._right; }
            set { this.SetProperty(ref this._right, value); }
        }

        public DelegateCommand Left1000UpCommand { get; private set; }
        public DelegateCommand Left100UpCommand { get; private set; }
        public DelegateCommand Left10UpCommand { get; private set; }
        public DelegateCommand Left1UpCommand { get; private set; }

        public DelegateCommand Left1000DownCommand { get; private set; }
        public DelegateCommand Left100DownCommand { get; private set; }
        public DelegateCommand Left10DownCommand { get; private set; }
        public DelegateCommand Left1DownCommand { get; private set; }

        public DelegateCommand Right1000UpCommand { get; private set; }
        public DelegateCommand Right100UpCommand { get; private set; }
        public DelegateCommand Right10UpCommand { get; private set; }
        public DelegateCommand Right1UpCommand { get; private set; }

        public DelegateCommand Right1000DownCommand { get; private set; }
        public DelegateCommand Right100DownCommand { get; private set; }
        public DelegateCommand Right10DownCommand { get; private set; }
        public DelegateCommand Right1DownCommand { get; private set; }

        public DelegateCommand LifeResetCommand { get; private set; }
        public DelegateCommand BackgroungCommand { get; private set; }
        public DelegateCommand UndoCommand { get; private set; }

        public DelegateCommand LeftHerfCommanf { get; private set; }
        public DelegateCommand RightHerfCommanf { get; private set; }


        public MainPageViewModel(INavigationService navigationService, IFileIO filrIo, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            FileIO = filrIo;
            _pageDialogService = pageDialogService;

            var aaa = "CONTENT://COM.ANDROID.PROVIDERS.MEDIA.DOCUMENTS/DOCUMENT/IMAGE%3A226760";
            //Source = new UriImageSource
            //{
            //    // Uri = new Uri(aaa),
            //    Uri = new Uri(aaa),
            //    // CachingEnabled = false,
            //    // CacheValidity = new TimeSpan(5, 0, 0, 0)
            //};
            //Source = ImageSource.FromUri(new Uri(aaa));
            //Source = ImageSource.FromResource(@"SimpleLifeCounterForY.Resources.bg17.jpg");
            //Source = ImageSource.FromResource(@"SimpleLifeCounterForY.Resources.bg2.jpg");

            System.Diagnostics.Debug.WriteLine("ログ出力1");
            System.Diagnostics.Debug.WriteLine("ログ出力2");
            System.Diagnostics.Debug.WriteLine("ログ出力3");
            System.Diagnostics.Debug.WriteLine("ログ出力4");
            System.Diagnostics.Debug.WriteLine("ログ出力5");

            Left1000UpCommand = new DelegateCommand(() => ChangeLeftLife(1000));
            Left100UpCommand = new DelegateCommand(() => ChangeLeftLife(100));
            Left10UpCommand = new DelegateCommand(() => ChangeLeftLife(10));
            Left1UpCommand = new DelegateCommand(() => ChangeLeftLife(1));

            Left1000DownCommand = new DelegateCommand(() => ChangeLeftLife(-1000));
            Left100DownCommand = new DelegateCommand(() => ChangeLeftLife(-100));
            Left10DownCommand = new DelegateCommand(() => ChangeLeftLife(-10));
            Left1DownCommand = new DelegateCommand(() => ChangeLeftLife(-1));

            Right1000UpCommand = new DelegateCommand(() => ChangeRightLife(1000));
            Right100UpCommand = new DelegateCommand(() => ChangeRightLife(100));
            Right10UpCommand = new DelegateCommand(() => ChangeRightLife(10));
            Right1UpCommand = new DelegateCommand(() => ChangeRightLife(1));

            Right1000DownCommand = new DelegateCommand(() => ChangeRightLife(-1000));
            Right100DownCommand = new DelegateCommand(() => ChangeRightLife(-100));
            Right10DownCommand = new DelegateCommand(() => ChangeRightLife(-10));
            Right1DownCommand = new DelegateCommand(() => ChangeRightLife(-1));

            LifeResetCommand = new DelegateCommand(LifeReset);
            BackgroungCommand = new DelegateCommand(BackgroungChange);
            UndoCommand = new DelegateCommand(Undo);

            LeftHerfCommanf = new DelegateCommand(HerfLeftLife);
            RightHerfCommanf = new DelegateCommand(HerfRightLife);

            LifeReset();
        }

        private void LifeReset()
        {
            RightLifePoint = 8000;
            LeftLifePoint = 8000;
        }

        private void ChangeLeftLife(int damage)
        {
            UndoStack.Push((LeftLifePoint, RightLifePoint));
            LeftLifePoint += damage;
        }

        private void ChangeRightLife(int damage)
        {
            UndoStack.Push((LeftLifePoint, RightLifePoint));
            RightLifePoint += damage;
        }

        private void HerfLeftLife()
        {
            UndoStack.Push((LeftLifePoint, RightLifePoint));
            LeftLifePoint = Convert.ToInt32(Math.Ceiling((double)LeftLifePoint / 2));
        }

        private void HerfRightLife()
        {
            UndoStack.Push((LeftLifePoint, RightLifePoint));
            RightLifePoint = Convert.ToInt32(Math.Ceiling((double)RightLifePoint / 2));
        }

        private void Undo()
        {
            if (UndoStack.Count == 0)
            {
                return;
            }
            var que = UndoStack.Pop();
            LeftLifePoint = que.Item1;
            RightLifePoint = que.Item2;

            
        }

        private void BackgroungChange()
        {
            

            if (MainPageViewModel.filePath != null)
            {
                BackgtoundImage = ImageSource.FromFile(MainPageViewModel.filePath);
                LeftLifePoint += 100;
                return;
            }
            FileIO.ShowImageList();
            // BackgtoundImage.Source = ImageSource.FromFile(filePath);
            // BackgtoundImage.Source = ImageSource.FromFile(url);
        }

        public static void SetPickupImageSource(string filePath)
        {
            System.Diagnostics.Debug.WriteLine("ログ出力112345567");
            System.Diagnostics.Debug.WriteLine($"ログ114　{filePath} 514");
            // += 100;
            MainPageViewModel.filePath = filePath;
        }
    }
}
