using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HotelManager.BLL.Interfaces;
using HotelManager.BLL.Models;
using Microsoft.Win32;
using Brushes = System.Drawing.Brushes;
using Size = System.Drawing.Size;

namespace HotelManager.UI.ViewModels.Dialogs
{
    public class CheckDialogViewModel : ViewModelBase
    {
        public CheckDialogViewModel(Hotel hotel, Residence residence)
        {
            Residence = residence;
            Hotel = hotel;
        }

        private Residence _residence;
        public Residence Residence
        {
            get => _residence;
            set
            {
                Set(ref _residence, value);
                RaisePropertyChanged(nameof(TotalCost));
            }
        }

        public double TotalCost =>
            (Residence.CheckOutDate - Residence.CheckInDate).TotalDays * Residence.HotelRoom.Price;

        private Hotel _hotel;
        public Hotel Hotel
        {
            get => _hotel;
            set => Set(ref _hotel, value);
        }

        private IDocumentPaginatorSource _check;
        public IDocumentPaginatorSource Check
        {
            get => _check;
            set => Set(ref _check, value);
        }


        // печать чека
        private RelayCommand<FlowDocument> _printCheckCommand;
        public ICommand PrintCheckCommand
            => _printCheckCommand ?? (_printCheckCommand = new RelayCommand<FlowDocument>(ExecutePrintCheckCommand));

        private void ExecutePrintCheckCommand(FlowDocument document)
        {
            // создание копии документа
            var str = XamlWriter.Save(document);
            var stringReader = new System.IO.StringReader(str);
            var xmlReader = XmlReader.Create(stringReader);
            var docClone = XamlReader.Load(xmlReader) as FlowDocument;

            // печать
            var pd = new PrintDialog();
            if (pd.ShowDialog().Value)
            {
                docClone.PageHeight = 350;
                docClone.PageWidth = 600;
                IDocumentPaginatorSource source = docClone;

                pd.PrintDocument(source.DocumentPaginator, $"{Residence.Client.Surname} - {Residence.Client.Patronymic} - Чек");
            }
        }


        // сохранение чека в файл
        private RelayCommand<FlowDocument> _saveCheckCommand;
        public ICommand SaveCheckCommand
            => _saveCheckCommand ?? (_saveCheckCommand = new RelayCommand<FlowDocument>(ExecuteSaveCheckCommand));

        private void ExecuteSaveCheckCommand(FlowDocument document)
        {
            var image = FlowDocumentToBitmap(document, new System.Windows.Size(600, 350));

            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "PNG|.png"
            };

            if (dialog.ShowDialog() == true)
            {
                Bitmap bmp = GetBitmap(image);
                bmp.Save(dialog.FileName, ImageFormat.Png);
            }
        }

        private Bitmap GetBitmap(BitmapSource source)
        {
            Bitmap bmp = new Bitmap
            (
                source.PixelWidth,
                source.PixelHeight,
                System.Drawing.Imaging.PixelFormat.Format32bppPArgb
            );

            BitmapData data = bmp.LockBits
            (
                new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size),
                ImageLockMode.WriteOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppPArgb
            );

            source.CopyPixels
            (
                Int32Rect.Empty,
                data.Scan0,
                data.Height * data.Stride,
                data.Stride
            );

            bmp.UnlockBits(data);

            return bmp;
        }

        public BitmapSource FlowDocumentToBitmap(FlowDocument document, System.Windows.Size size)
        {
            document = CloneDocument(document);

            var paginator = ((IDocumentPaginatorSource)document).DocumentPaginator;
            paginator.PageSize = size;

            var visual = new DrawingVisual();
            using (var drawingContext = visual.RenderOpen())
            {
                // белый фон
                drawingContext.DrawRectangle(System.Windows.Media.Brushes.White, null, new Rect(size));
            }
            visual.Children.Add(paginator.GetPage(0).Visual);

            var bitmap = new RenderTargetBitmap((int)size.Width, (int)size.Height,
                96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            return bitmap;
        }

        public FlowDocument CloneDocument(FlowDocument document)
        {
            // создание копии документа
            var str = XamlWriter.Save(document);
            var stringReader = new StringReader(str);
            var xmlReader = XmlReader.Create(stringReader);
            var docClone = XamlReader.Load(xmlReader) as FlowDocument;

            return docClone;
        }
    }
}
