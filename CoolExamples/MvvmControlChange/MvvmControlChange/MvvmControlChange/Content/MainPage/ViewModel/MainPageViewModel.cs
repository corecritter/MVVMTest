using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace MvvmControlChange.Content.MainPage.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainPageViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainPageViewModel class.
        /// </summary>
        public MainPageViewModel()
        {
            Page2Command = new RelayCommand(() => GoToPage2());
        }

        public string ApplicationTitle
        {
            get
            {
                return "MVVM LIGHT 1 more Edwin";
            }
        }

        public string PageName
        {
            get
            {
                return "Page 1.  Please message away from me";
            }
        }

        public string Welcome
        {
            get
            {
                return "Welcome to Page 1";
            }
        }

        public RelayCommand Page2Command
        {
            get;
            private set;
        }

        private object GoToPage2()
        {
            var msg = new GoToPageMessage() { PageName = "Page2View" };
            Messenger.Default.Send<GoToPageMessage>(msg);

            //System.Windows.MessageBox.Show("Navigate to Page 2!");
            return null;
        }

    }
}