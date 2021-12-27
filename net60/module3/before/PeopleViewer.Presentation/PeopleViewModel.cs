using PeopleViewer.Common;
using PersonDataReader.Service;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PeopleViewer.Presentation;

public class PeopleViewModel : INotifyPropertyChanged
{
    protected IPersonReader DataReader; //Dependency

    private IEnumerable<Person> _people = new List<Person>();

    public IEnumerable<Person> People
    {
        get => _people;
        set { _people = value; RaisePropertyChanged(); }
    }

    // Dependency Inversion Principle
    // The view model is no longer responsible for
    // getting or managing the lifetime of the dependency.
    public PeopleViewModel(IPersonReader dataReader) //Inject the dependency using the constructor
    {
        DataReader = dataReader; //Inject the dependency using the constructor
    }

    public void RefreshPeople()
    {
        People = DataReader.GetPeople(); //Dependency
    }

    public void ClearPeople()
    {
        People = new List<Person>();
    }

    public string DataReaderType
    {
        get { return DataReader.GetType().ToString(); }
    }


    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler? PropertyChanged;
    private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}