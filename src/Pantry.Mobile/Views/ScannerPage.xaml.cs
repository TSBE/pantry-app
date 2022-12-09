using System.ComponentModel;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Helpers;
using ZXing.Net.Maui;

namespace Pantry.Mobile.Views;

[QueryProperty(nameof(BackTargetPage), nameof(BackTargetPage))]
public partial class ScannerPage : ContentPage, INotifyPropertyChanged
{
    private readonly INavigationService _navigationService;

    public ScannerPage(INavigationService navigationService)
    {
        InitializeComponent();
        BindingContext = this;

        _navigationService = navigationService;

        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormat.Ean13 | BarcodeFormat.QrCode,
            AutoRotate = true,
            Multiple = false
        };
    }

    public string BackTargetPage { get; set; } = string.Empty;

    private bool isDetecting = true;
    public bool IsDetecting
    {
        get
        {
            return isDetecting;
        }
        set
        {
            isDetecting = value;
            OnPropertyChanged();
        }
    }

    private string lastBarcode = string.Empty;

    protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        try
        {
            IsDetecting = false;
            var barcode = e.Results.FirstOrDefault();

            if (lastBarcode.Equals(barcode?.Value))
            {
                return;
            }

            if (!GtinChecker.IsValidEAN13(barcode?.Value ?? string.Empty) && barcode?.Format == BarcodeFormat.Ean13)
            {
                return;
            }

            if (!Guid.TryParse(barcode?.Value, out _) && barcode?.Format == BarcodeFormat.QrCode)
            {
                return;
            }

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                lastBarcode = barcode?.Value ?? string.Empty;

                if (string.IsNullOrEmpty(BackTargetPage))
                {
                    await _navigationService.GoToAsync($"..?Barcode={barcode?.Value}");
                }
                else
                {
                    await _navigationService.GoToAsync($"../{BackTargetPage}?Barcode={barcode?.Value}");
                }
            });
        }
        finally
        {
            IsDetecting = true;
        }
    }
}
