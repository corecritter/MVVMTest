using GalaSoft.MvvmLight;

namespace MvvmControlChange.Content.Page3.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class Page3ViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the Page3ViewModel class.
        /// </summary>
        public Page3ViewModel()
        {
        }
        public string InfoTitlePage3
        {
            get
            {
                return "MVVM LIGHT p3";
            }
        }

        public string PageTitle
        {
            get
            {
                return "Page 3";
            }
        }
    }
}