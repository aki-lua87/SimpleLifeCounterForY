using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleLifeCounterForY.Models
{
    public interface IFileIO
    {
        string ShowImageList();
        void LoadImage();
        void SaveImage();
    }
}
