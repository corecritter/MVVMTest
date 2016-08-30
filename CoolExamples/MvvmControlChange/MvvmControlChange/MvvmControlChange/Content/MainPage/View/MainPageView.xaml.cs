using System.Windows;
using MvvmControlChange.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using MvvmControlChange.Content.Page2.View;
using MvvmControlChange.Content.Page3.View;

namespace MvvmControlChange.Content.MainPage.View
{
    /// <summary>
    /// Description for MainPageView.
    /// </summary>
    public partial class MainPageView : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainPageView class.
        /// </summary>
        public MainPageView()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();

            Messenger.Default.Register<GoToPageMessage>
                (
                    this,
                    (action) => ReceiveMessage(action)
                );
        }

        private Page2View _page2View;
        private Page2View Page2View
        {
            get
            {
                if (_page2View == null)
                    _page2View = new Page2View();
                return _page2View;
            }
        }

        private Page3View _page3View;
        private Page3View Page3View
        {
            get
            {
                if (_page3View == null)
                    _page3View = new Page3View();
                return _page3View;
            }
        }

        private object ReceiveMessage(GoToPageMessage action)
        {
            //            this.contentControl1.Content = this.Page2View;
            //this shows what pagename property is!!
            switch (action.PageName)
            {
                case "Page2View":
                    if (this.contentControl1.Content != this.Page2View)
                        this.contentControl1.Content = this.Page2View;
                    break;
                case "Page3View":
                    if (this.contentControl1.Content != this.Page3View)
                        this.contentControl1.Content = this.Page3View;
                    break;
                default:
                    break;
            }

//            string testII = action.PageName.ToString();
//           System.Windows.MessageBox.Show("You were successful switching to " + testII + ".");

            return null;
        }
    }
}