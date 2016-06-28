using MVVMTest.DataAccess;
using MVVMTest.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MVVMTest.ViewModel
{
    public class CustomerViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        #region Fields

        readonly Customer _customer;
        readonly CustomerRepository _customerRepository;
        string _customerType;
        string[] _customerTypeOptions;
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
            _customerType = "Not Specified";
        }

        #endregion
        
        public string CustomerType
        {
            get { return _customerType; }
            set
            {
                if (value == _customerType || String.IsNullOrEmpty(value))
                    return;

                _customerType = value;

                if(_customerType.Equals("Person"))
                {
                    _customer.IsCompany = false;
                }
                else if(_customerType.Equals("Company"))
                {
                    _customer.IsCompany = true;
                }

                base.OnPropertyChanged("CustomerType");

                // Since this ViewModel object has knowledge of how to translate
                // a customer type (i.e. text) to a Customer object's IsCompany property,
                // it also must raise a property change notification when it changes
                // the value of IsCompany.  The LastName property is validated 
                // differently based on whether the customer is a company or not,
                // so the validation for the LastName property must execute now.
                base.OnPropertyChanged("LastName");
            }
        }

        public string[] CustomerTypeOptions
        {
            get
            {
                if (_customerTypeOptions == null)
                {
                    _customerTypeOptions = new string[]
                    {
                        "Not Specified",
                        "Person",
                        "Company"
                    };
                }

                return _customerTypeOptions;
            }
        }

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                if(_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(param => this.Save(), param => this.CanSave);
                }
                return _saveCommand;
            }
        }

        public void Save()
        {
            //if(!isValid)

            if (this.isNewCustomer)
                _customerRepository.AddCustomer(_customer);

            base.OnPropertyChanged("DisplayName");
        }
        #endregion

        #region Helpers
        //TODO: Implement Validation
        bool CanSave 
        {
            get { return true; }
        }
        bool isNewCustomer
        {
            get { return !_customerRepository.ContainsCustomer(_customer); }
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

        //From ViewModelBase
        public override string DisplayName
        {
            get
            {
                if (this.isNewCustomer)
                    return "New Customer";
                else if (_customer.IsCompany)
                    return _customer.FirstName;
                else
                    return String.Format("{0}, {1}", _customer.LastName, _customer.FirstName);
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

        public bool IsCompany
        {
            get { return _customer.IsCompany; }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value == _isSelected)
                    return;

                _isSelected = value;

                base.OnPropertyChanged("IsSelected");
            }
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
