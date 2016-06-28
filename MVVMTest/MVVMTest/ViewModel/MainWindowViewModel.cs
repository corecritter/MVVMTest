using MVVMTest.DataAccess;
using MVVMTest.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MVVMTest.ViewModel
{
    public class MainWindowViewModel : WorkspaceViewModel
    {
        ObservableCollection<WorkspaceViewModel> _workspaces;
        ReadOnlyCollection<CommandViewModel> _commands;
        readonly CustomerRepository _customerRepository;

        public MainWindowViewModel(string customerDataFile)
        {
            base.DisplayName = "Set From MainViewModel";

            _customerRepository = new CustomerRepository(customerDataFile);
        }

        #region Workspaces

        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if(_workspaces == null)
                {
                    _workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _workspaces.CollectionChanged += this.OnWorkSpacesChanged;
                }
                return _workspaces;
            }
        }
        void OnWorkSpacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkSpaceRequestClose;
            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkSpaceRequestClose;

        }

        void OnWorkSpaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            workspace.Dispose();
            this.Workspaces.Remove(workspace);
        }

        void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);

            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }
        #endregion

        #region Commands

        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if(_commands == null)
                {
                    List<CommandViewModel> cmds = this.CreateCommands();
                    _commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _commands;
            }
        }

        List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel("View All Customers", new RelayCommand(param => this.ShowAllCustomers())),
                new CommandViewModel("Create New", new RelayCommand(param => this.CreateNewCustomer()))
            };
        }

        #endregion

        #region Helpers

        void CreateNewCustomer()
        {
            Customer newCustomer = Customer.CreateNewCustomer();
            CustomerViewModel workspace = new CustomerViewModel(newCustomer, _customerRepository);
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }

        void ShowAllCustomers()
        {
            //Ensures only one AllCustomersView can be shown at a time
            AllCustomersViewModel workspace =
                this._workspaces.FirstOrDefault(vm => vm is AllCustomersViewModel)
                as AllCustomersViewModel;

            if (workspace == null)
            {
                workspace = new AllCustomersViewModel(_customerRepository);
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        #endregion
    }

}
