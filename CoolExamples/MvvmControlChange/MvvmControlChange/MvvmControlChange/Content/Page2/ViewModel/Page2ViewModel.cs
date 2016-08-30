using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace MvvmControlChange.Content.Page2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class Page2ViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the Page2ViewModel class.
        /// </summary>
        public Page2ViewModel()
        {
            Page3Command = new RelayCommand(() => GoToPage3());
        }

        public string ApplicationTitle
        {
            get
            {
                return "MVVM LIGHT p2";
            }
        }

        public string PageName
        {
            get
            {
                return "Page 2";
            }
        }

        public string Welcome
        {
            get
            {
                return "Welcome to Page 2";
            }
        }

        public RelayCommand Page3Command
        {
            get;
            private set;
        }

        private object GoToPage3()
        {
            var msg = new GoToPageMessage() { PageName = "Page3View" };
            Messenger.Default.Send<GoToPageMessage>(msg);

            //System.Windows.MessageBox.Show("Navigate to Page 2!");
            return null;
        }

    }
}