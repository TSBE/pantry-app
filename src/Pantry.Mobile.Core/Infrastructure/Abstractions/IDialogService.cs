namespace Pantry.Mobile.Core.Infrastructure.Abstractions;

public interface IDialogService
{
    Task ShowMessage(string message);
}
