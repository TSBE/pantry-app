using CommunityToolkit.Maui.Alerts;
using Pantry.Mobile.Core.Infrastructure.Abstractions;

namespace Pantry.Mobile.Interactors;

public class ToolkitDialogService : IDialogService
{
    public async Task ShowMessage(string message)
    {
        await Snackbar.Make(message).Show();
    }
}
