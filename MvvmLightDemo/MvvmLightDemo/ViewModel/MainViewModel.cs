using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmLightDemo.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MvvmLightDemo.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        private ObservableCollection<FriendViewModel> _friends;
        
        public ObservableCollection<FriendViewModel> Friends
        {
            get
            {
                if (_friends == null)
                {
                    _friends = new ObservableCollection<FriendViewModel>();

                }
                return _friends;
            }
        }
        
        
        
        /// <summary>
        /// The <see cref="WelcomeTitle" /> property's name.
        /// </summary>
        public const string WelcomeTitlePropertyName = "WelcomeTitle";

        private string _welcomeTitle = string.Empty;

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string WelcomeTitle
        {
            get
            {
                return _welcomeTitle;
            }
            set
            {
                Set(ref _welcomeTitle, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;

            //_dataService.GetData(
            //    (item, error) =>
            //    {
            //        if (error != null)
            //        {
            //            // Report error here
            //            return;
            //        }

            //        WelcomeTitle = item.Title;
            //    });
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}


        private RelayCommand _refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand
                  ?? (_refreshCommand =
                  new RelayCommand(ExecuteRefreshCommand));
            }
        }
        private async void ExecuteRefreshCommand()
        {
            var friends = await _dataService.GetFriends();
            if (friends != null)
            {
                _friends.Clear();
                foreach (var friend in friends)
                {
                    _friends.Add(new FriendViewModel(friend, null));
                }
            }
        }
    }
}