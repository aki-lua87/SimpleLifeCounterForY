using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleLifeCounterForY.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
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

        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Main Page";

            Left1000UpCommand = new DelegateCommand(() => LeftLifePoint += 1000);
            Left100UpCommand = new DelegateCommand(() => LeftLifePoint += 100);
            Left10UpCommand = new DelegateCommand(() => LeftLifePoint += 10);
            Left1UpCommand = new DelegateCommand(() => LeftLifePoint += 1);

            Left1000DownCommand = new DelegateCommand(() => LeftLifePoint -= 1000);
            Left100DownCommand = new DelegateCommand(() => LeftLifePoint -= 100);
            Left10DownCommand = new DelegateCommand(() => LeftLifePoint -= 10);
            Left1DownCommand = new DelegateCommand(() => LeftLifePoint -= 1);

            Right1000UpCommand = new DelegateCommand(() => RightLifePoint += 1000);
            Right100UpCommand = new DelegateCommand(() => RightLifePoint += 100);
            Right10UpCommand = new DelegateCommand(() => RightLifePoint += 10);
            Right1UpCommand = new DelegateCommand(() => RightLifePoint += 1);

            Right1000DownCommand = new DelegateCommand(() => RightLifePoint -= 1000);
            Right100DownCommand = new DelegateCommand(() => RightLifePoint -= 100);
            Right10DownCommand = new DelegateCommand(() => RightLifePoint -= 10);
            Right1DownCommand = new DelegateCommand(() => RightLifePoint -= 1);

            LifeResetCommand = new DelegateCommand(LifeReset);

            LifeReset();
        }

        private void LifeReset()
        {
            RightLifePoint = 8000;
            LeftLifePoint = 8000;
        }
    }
}
