using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.FileProviders;

namespace myWebApp.Models
{
    public class Paginator
    {
        const int VALUES_PER_LINE = 16;

        public int PageNumber;
        public int LinesPerPage = 30;
        byte[] byteArray;
        IFileInfo file;


        int PageLength() => LinesPerPage * VALUES_PER_LINE;
        int Offset() => (PageNumber - 1) * PageLength();
        int PageLimit() => Offset() + PageLength();
        public int NumberOfPages() => (byteArray.Length / PageLength()) +1;
        public int LineNumber { get; set; }

        public void LoadFile(IFileInfo _file, int _pageNumber = 1)
        {
            file = _file;
            byteArray = File.ReadAllBytes(_file.PhysicalPath);
            PageNumber = _pageNumber <= NumberOfPages() ?
                _pageNumber : NumberOfPages();
            LineNumber = Offset();
        }

        public string[][] GetPage()
        {
            string[][] Lines = new string[LinesPerPage][];
            int lineNumber = 0;

            for (int i = Offset(); i < byteArray.Length && i < PageLimit(); i++)
            {
                int LineByteNumber = i % VALUES_PER_LINE;
                if (LineByteNumber == 0)
                {
                    if (i > Offset()) lineNumber++;
                    Lines[lineNumber] = new string[VALUES_PER_LINE];
                }
                Lines[lineNumber][LineByteNumber] = string.Format("{0:X2}", byteArray[i]);
            }
            return Lines;
        }

        public void Update(string[] hexArray)
        {
            for (int i = 0; i < hexArray.Length; i++)
            {
                byte b = Convert.ToByte(hexArray[i], 16);
                byteArray[Offset() + i] = b;
            }
            File.WriteAllBytes(file.PhysicalPath, byteArray);
        }

        public int Previous() =>
            PageNumber > 1 ? (PageNumber - 1) : 1;

        public int Next() 
            => PageNumber < NumberOfPages() ? (PageNumber + 1) : NumberOfPages();

        public double Progress()
            => (((double)PageNumber / (double)NumberOfPages()) * 100);

        //public int[] PageLinks()
        //{
        //    int extra = 0;
        //    int start = 1;
        //    int end = NumberOfPages();
        //    if (PageNumber > 5) {
        //        start = PageNumber - 5;
        //    }
        //    if (NumberOfPages() > 10)
        //    {
        //            if (PageNumber < NumberOfPages()) end = PageNumber + 5;
        //            if (PageNumber <= 5)
        //            {
        //                extra = 6 - PageNumber;
        //            }
        //    }
        //    return Enumerable.Range(start, end - start + extra).ToArray();
        //}

    }
}