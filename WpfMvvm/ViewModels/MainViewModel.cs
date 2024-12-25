namespace WpfMvvm.ViewModels;

using System.Collections.ObjectModel;
using System.Windows;
using WpfMvvm.Commands;
using WpfMvvm.Models;


public class MainViewModel : ViewModelBase
{
    private string? _hobbyInput;

    public ObservableCollection<Hobby> Hobbies { get; set; }

    public string? HobbyInput
    {
        get => _hobbyInput;
        set
        {
            if (_hobbyInput != value)
            {
                _hobbyInput = value;
                OnPropertyChanged();
                AddHobbyCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public DelegateCommand AddHobbyCommand { get; }

    public MainViewModel()
    {
        Hobbies = new ObservableCollection<Hobby>
        {
            new Hobby { Name = "Reading" },
            new Hobby { Name = "Gaming" },
            new Hobby { Name = "Cycling" }
        };

        AddHobbyCommand = new DelegateCommand(AddHobby, CanAddHobby);
    }

    private void AddHobby(object? parameter)
    {
        if (Hobbies.Any(h => h.Name.Equals(HobbyInput, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show("This hobby already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        else
        {
            Hobbies.Add(new Hobby { Name = HobbyInput });
            HobbyInput = string.Empty;
        }
    }

    private bool CanAddHobby(object? parameter) => !string.IsNullOrWhiteSpace(HobbyInput);
}
