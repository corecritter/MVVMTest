using MVVMTest.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace MVVMTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow m = new MainWindow();

            string path = "Data/customers.xml";
            var viewModel = new MainWindowViewModel(path);

            EventHandler handler = null;
            handler = delegate
            {
                viewModel.RequestClose -= handler;
                m.Close();
            };
            viewModel.RequestClose += handler;

            m.DataContext = viewModel;
            m.Show();
        }
    }
}
