using Pantry.Mobile.Core.ViewModels;
using ZXing.Net.Maui;

namespace Pantry.Mobile.Views;

public partial class ScannerPage : ContentPage
{
    public ScannerPage(/*ScannerViewModel vm*/)
    {
        InitializeComponent();
        //BindingContext = vm;

        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormat.Ean13,
            AutoRotate = true,
            Multiple = false
        };
    }

    protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        foreach (var barcode in e.Results)
        {
            Console.WriteLine($"Barcodes: {barcode.Format} -> {barcode.Value}");
        }
    }
}
