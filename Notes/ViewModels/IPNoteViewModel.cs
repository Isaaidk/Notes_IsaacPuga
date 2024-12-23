using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace Notes.ViewModels;

internal class IPNoteViewModel : ObservableObject, IQueryAttributable
{
    private Models.Note _note;

    public string Text
    {
        get => _note.Text; //Nos permite leer y escribir el valor de 
        set
        {
            if (_note.Text != value)
            {
                _note.Text = value;
                OnPropertyChanged();
            }
        }
    }

    public DateTime Date => _note.Date; //devuelve el valor de date 

    public string Identifier => _note.Filename; //devuelve el valor de el identificador k es el nombre del file

    public ICommand SaveCommand { get; private set; } //icommand usado para para contener un comando 
    public ICommand DeleteCommand { get; private set; }

    public IPNoteViewModel()
    {
        _note = new Models.Note(); //instancamos nota
        SaveCommand = new AsyncRelayCommand(Save); //comandos asincronicos de save y delete 
        DeleteCommand = new AsyncRelayCommand(Delete);
    }

    public IPNoteViewModel(Models.Note note)
    {
        _note = note;
        SaveCommand = new AsyncRelayCommand(Save);
        DeleteCommand = new AsyncRelayCommand(Delete);
    }

    private async Task Save()
    {
        _note.Date = DateTime.Now; //Agarra la fecha exacta en la que se guarda 
        _note.Save();
        //Espera a que la navegacion se complete, seguido el shell permite una navegacion
        //asincronica ,luego se define la ruta y lo que se va a pasar k es el filename
        await Shell.Current.GoToAsync($"..?saved={_note.Filename}");
    }

    private async Task Delete()
    {
        _note.Delete();
        await Shell.Current.GoToAsync($"..?deleted={_note.Filename}");
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("load")) //pregunta la clave del query , si la encuentra carga la nota 
        {
            _note = Models.Note.Load(query["load"].ToString());
            RefreshProperties();
        }
    }

    public void Reload()
    {
        _note = Models.Note.Load(_note.Filename);
        RefreshProperties();
    }//carga la nota

    private void RefreshProperties()
    {
        //notifica que la propiedad a cambiado y hace que la ui se actualize 
        OnPropertyChanged(nameof(Text));
        OnPropertyChanged(nameof(Date));
    }
}