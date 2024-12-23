using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Notes.ViewModels;

internal class AboutViewModel
{
    public string Title => "Isaac";
    public string Version => "Puga";
    public string MoreInfoUrl => "https://learn.microsoft.com/es-es/dotnet/maui/tutorials/notes-mvvm/?view=net-maui-9.0";
    public string Message => "APLICACION MVVM MODIFICADA POR ISAAC PUGA ";
    public ICommand ShowMoreInfoCommand { get; }

    public AboutViewModel()
    {
        ShowMoreInfoCommand = new AsyncRelayCommand(ShowMoreInfo);
    }

    async Task ShowMoreInfo() =>
        await Launcher.Default.OpenAsync(MoreInfoUrl);
}