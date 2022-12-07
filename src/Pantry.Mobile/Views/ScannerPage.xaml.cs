using Pantry.Mobile.Core.Infrastructure.Abstractions;
using ZXing.Net.Maui;

namespace Pantry.Mobile.Views;

[QueryProperty(nameof(BackTargetPage), nameof(BackTargetPage))]
public partial class ScannerPage : ContentPage
{
    private readonly INavigationService _navigationService;

    public ScannerPage(INavigationService navigationService)
    {
        InitializeComponent();
        _navigationService = navigationService;

        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormat.Ean13 | BarcodeFormat.QrCode,
            AutoRotate = true,
            Multiple = false
        };
    }

    public string BackTargetPage { get; set; } = string.Empty;

    protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        foreach (var barcode in e.Results)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (string.IsNullOrEmpty(BackTargetPage))
                {
                    await _navigationService.GoToAsync($"..?Barcode={barcode.Value}");
                }
                else
                {
                    await _navigationService.GoToAsync($"../{BackTargetPage}?Barcode={barcode.Value}");
                }
            });
            break;
        }
    }
}
