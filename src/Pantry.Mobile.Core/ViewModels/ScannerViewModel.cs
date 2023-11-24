using BarcodeScanner.Mobile;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Helpers;
using ZXing.Net.Maui;

namespace Pantry.Mobile.Core.ViewModels;

[QueryProperty(nameof(BackTargetPage), nameof(BackTargetPage))]
public partial class ScannerViewModel : BaseViewModel
{
    private readonly INavigationService _navigation;

    public ScannerViewModel(INavigationService navigation)
    {
        _navigation = navigation;
    }
    [ObservableProperty]
    public string backTargetPage = string.Empty;

    [ObservableProperty]
    public bool isTorchOn = false;

    [ObservableProperty]
    public bool isDetecting = true;

    [ObservableProperty]
    public BarcodeReaderOptions barcodeReaderOptions = new BarcodeReaderOptions
    {
        Formats = BarcodeFormat.Ean13, //| BarcodeFormat.QrCode,
        AutoRotate = true,
        Multiple = false,
    };

    [RelayCommand]
    public async Task BarcodesDetected(BarcodeDetectionEventArgs e)
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

            lastBarcode = barcode?.Value ?? string.Empty;

            if (string.IsNullOrEmpty(BackTargetPage))
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                    _navigation.GoToAsync($"..?Barcode={barcode?.Value}"));
            }
            else
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                    _navigation.GoToAsync($"../{BackTargetPage}?Barcode={barcode?.Value}"));
            }
        }
        finally
        {
            IsDetecting = true;
        }
    }

    [RelayCommand]
    public async Task Detected(OnDetectedEventArg e)
    {
        try
        {
            IsDetecting = false;

            List<BarcodeScanner.Mobile.BarcodeResult> obj = e.BarcodeResults;

            var barcode = obj.Where(x => x.BarcodeFormat.Equals(BarcodeScanner.Mobile.BarcodeFormats.Ean13)
            && x.BarcodeType.Equals(BarcodeTypes.Product)).FirstOrDefault();

            if (lastBarcode.Equals(barcode?.DisplayValue))
            {
                return;
            }

            if (!GtinChecker.IsValidEAN13(barcode?.DisplayValue ?? string.Empty)
                && barcode?.BarcodeFormat == BarcodeScanner.Mobile.BarcodeFormats.Ean13)
            {
                return;
            }

            if (!Guid.TryParse(barcode?.DisplayValue, out _) && barcode?.BarcodeFormat == BarcodeScanner.Mobile.BarcodeFormats.QRCode)
            {
                return;
            }

            lastBarcode = barcode?.DisplayValue ?? string.Empty;

            if (string.IsNullOrEmpty(BackTargetPage))
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                    _navigation.GoToAsync($"..?Barcode={barcode?.DisplayValue}"));
            }
            else
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                    _navigation.GoToAsync($"../{BackTargetPage}?Barcode={barcode?.DisplayValue}"));
            }
        }
        finally
        {
            IsDetecting = true;
        }
    }

    private string lastBarcode = string.Empty;
}
