using MVVMTest.DataAccess;
using MVVMTest.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MVVMTest.ViewModel
{
    public class AllCustomersViewModel : WorkspaceViewModel
    {
        readonly CustomerRepository _customerRepository;

        public AllCustomersViewModel(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _customerRepository.CustomerAdded += this.OnCustomerAddedToRepository;
            base.DisplayName = "AllCustomersViewModelDisplayName";

            this.CreateAllCustomers();
        }

        void CreateAllCustomers()
        {
            List<CustomerViewModel> all =
                (from cust in _customerRepository.GetCustomers()
                 select new CustomerViewModel(cust, _customerRepository)).ToList();

            foreach (CustomerViewModel cvm in all)
                cvm.PropertyChanged += this.OnCustomerViewModelPropertyChanged;

            this.AllCustomers = new ObservableCollection<CustomerViewModel>(all);
            this.AllCustomers.CollectionChanged += this.OnCollectionChanged;
        }

        public double TotalSelectedSales
        {
            get
            {
                return this.AllCustomers.Sum(custVM => custVM.IsSelected ? custVM.TotalSales : 0.0);
            }
        }

        void OnCustomerAddedToRepository(object sender, CustomerAddedEventArgs e)
        {
            var viewModel = new CustomerViewModel(e.NewCustomer, _customerRepository);
            this.AllCustomers.Add(viewModel);
        }

        void OnCustomerViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.OnPropertyChanged("TotalSelectedSales");
        }
        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (CustomerViewModel custVM in e.NewItems)
                    custVM.PropertyChanged += this.OnCustomerViewModelPropertyChanged;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (CustomerViewModel custVM in e.OldItems)
                    custVM.PropertyChanged -= this.OnCustomerViewModelPropertyChanged;
        }
        /// <summary>
        /// Returns a collection of all the CustomerViewModel objects.
        /// </summary>
        public ObservableCollection<CustomerViewModel> AllCustomers { get; private set; }
    }
}
