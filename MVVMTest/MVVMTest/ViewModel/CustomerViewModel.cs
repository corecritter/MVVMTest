using MVVMTest.DataAccess;
using MVVMTest.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MVVMTest.ViewModel
{
    public class CustomerViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        #region Fields

        readonly Customer _customer;
        readonly CustomerRepository _customerRepository;
        //string _customerType;
        //string[] _customerTypeOptions;
        bool _isSelected;
        RelayCommand _saveCommand;

        #endregion

        #region Constructor

        public CustomerViewModel(Customer customer, CustomerRepository customerRepository)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            if (customerRepository == null)
                throw new ArgumentNullException("customerRepository");

            _customer = customer;
            _customerRepository = customerRepository;
        }

        #endregion

        #region Customer Properties

        public string FirstName
        {
            get { return _customer.FirstName; }
            set
            {
                if (value == _customer.FirstName)
                    return;

                _customer.FirstName = value;

                base.OnPropertyChanged("FirstName");
            }
        }
        bool isNewCustomer
        {
            get { return !_customerRepository.ContainsCustomer(_customer); }
        }
        //From ViewModelBase
        public override string DisplayName
        {
            get
            {
                if (this.isNewCustomer)
                    return "New Customer";
                else
                    return String.Format("{0}, {1}",_customer.LastName, _customer.FirstName);
            }
        }

        public string LastName
        {
            get { return _customer.LastName; }
            set
            {
                if (value == _customer.LastName)
                    return;

                _customer.LastName = value;

                base.OnPropertyChanged("LastName");
            }
        }
        public string Email
        {
            get { return _customer.Email; }
            set
            {
                if (value == _customer.Email)
                    return;

                _customer.Email = value;

                base.OnPropertyChanged("Email");
            }
        }

        public double TotalSales
        {
            get { return _customer.TotalSales; }
        }

        #endregion // Customer Properties

        #region IDataErrorInfo

        public string this[string columnName]
        {
            get
            {
                return "CutomerViewModel.this";//throw new NotImplementedException();
            }
        }

        public string Error
        {
            get
            {
                return "CutomerViewModel.Error"; //throw new NotImplementedException();
            }
        }

        #endregion
    }
}
