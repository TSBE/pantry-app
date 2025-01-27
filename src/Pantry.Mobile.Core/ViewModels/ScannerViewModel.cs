using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Helpers;
using ZXing.Net.Maui;

namespace Pantry.Mobile.Core.ViewModels;

[QueryProperty(nameof(BackTargetPage), nameof(BackTargetPage))]
[QueryProperty(nameof(ActiveBarcodeFormat), nameof(ActiveBarcodeFormat))]
public partial class ScannerViewModel : BaseViewModel
{
    private readonly INavigationService _navigation;
    
    private string _lastBarcode = string.Empty;

    public ScannerViewModel(INavigationService navigation, IDeviceInfo deviceInfo)
    {
        _navigation = navigation;
        IsSimulator = deviceInfo.DeviceType == DeviceType.Virtual;
    }

    [ObservableProperty] private bool isSimulator;
    
    [ObservableProperty] private string backTargetPage = string.Empty;

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(InitCommand))]
    private string? activeBarcodeFormat;

    [ObservableProperty] private bool isTorchOn;

    [ObservableProperty] private bool isDetecting = true;
    
    [ObservableProperty] private string simulatorBarcode = "5745000121045";

    [ObservableProperty] private BarcodeReaderOptions barcodeReaderOptions = new()
    {
        Formats = BarcodeFormat.Ean13, //| BarcodeFormat.QrCode,
        AutoRotate = true,
        Multiple = false
    };

    [RelayCommand]
    private void Init()
    {
        var flag = Enum.TryParse(ActiveBarcodeFormat, out BarcodeFormat barcodeFormat);
        BarcodeReaderOptions = new BarcodeReaderOptions
        {
            Formats = flag?barcodeFormat: BarcodeFormat.QrCode,
            AutoRotate = true,
            Multiple = false,
        };
        SimulatorBarcode = barcodeFormat == BarcodeFormat.Ean13 ? "5745000121045" : "fe82f4ad-ace3-4427-933c-d96cd2dca74c";
    }

    [RelayCommand]
    private async Task SimulatorScan()
    {
        if (string.IsNullOrEmpty(BackTargetPage))
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
                _navigation.GoToAsync($"..?Barcode={SimulatorBarcode}"));
        }
        else
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
                _navigation.GoToAsync($"../{BackTargetPage}?Barcode={SimulatorBarcode}"));
        }
    }

    [RelayCommand]
    private async Task BarcodesDetected(BarcodeDetectionEventArgs e)
    {
        try
        {
            IsDetecting = false;
            var barcode = e.Results.FirstOrDefault();

            if (_lastBarcode.Equals(barcode?.Value))
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

            _lastBarcode = barcode?.Value ?? string.Empty;

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
    private async Task DetectionFinished(IReadOnlySet<BarcodeResult> results)
    {
        try
        {
            IsDetecting = false;
            
            var barcode = results.FirstOrDefault(x => x.Format.Equals(BarcodeFormat.Ean13));

            if (_lastBarcode.Equals(barcode?.Value))
            {
                return;
            }

            if (!GtinChecker.IsValidEAN13(barcode?.Value ?? string.Empty)
                && barcode?.Format == BarcodeFormat.Ean13)
            {
                return;
            }

            // if (!Guid.TryParse(barcode?.DisplayValue, out _) && barcode?.BarcodeFormat == BarcodeScanner.Mobile.BarcodeFormats.QRCode)
            // {
            //     return;
            // }

            _lastBarcode = barcode?.Value ?? string.Empty;

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
}
