using System.Collections.ObjectModel;
using Caliburn.Micro.WinRT.Autofac.Sample.Entities;
using Caliburn.Micro.WinRT.Autofac.Sample.Events;

namespace Caliburn.Micro.WinRT.Autofac.Sample.ViewModels
{
    public class MainPageViewModel : Screen, IHandle<ClearListEvent>
    {
        private readonly IEventAggregator _messenger;

        public MainPageViewModel(IEventAggregator messenger, RightMenuViewModel rightMenuViewModel)
        {
            _messenger = messenger;
            RightMenu = rightMenuViewModel;
            People = new ObservableCollection<Person>();
            _messenger.Subscribe(this);
        }

        private string _loadMessage;
        public string LoadMessage
        {
            get { return _loadMessage; }
            set { _loadMessage = value; NotifyOfPropertyChange(() => LoadMessage); }
        }

        private ObservableCollection<Person> _people;
        public ObservableCollection<Person> People
        {
            get { return _people; }
            set
            {
                _people = value;
                NotifyOfPropertyChange(() => People);
            }
        }

        public RightMenuViewModel RightMenu { get; set; }

        public void LoadPeople()
        {
            People = GetPeople();
        }

        private ObservableCollection<Person> GetPeople()
        {
            var people = new ObservableCollection<Person>();
            for (int i = 0; i < 10; i++)
            {
                var person = new Person
                                 {
                                     Name = string.Format("Name-{0}", i),
                                     Age = 10 + i
                                 };

                people.Add(person);
            }

            return people;
        }

        public void Handle(ClearListEvent message)
        {
            People.Clear();
        }
    }
}
