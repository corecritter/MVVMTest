using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MVVMTest.Model
{
    public class Customer : IDataErrorInfo
    {
        #region Creation
        public static Customer CreateNewCustomer()
        {
            return new Customer();
        }
        public static Customer CreateCustomer(string fName, string lName)
        {
            return new Customer()
            {
                FirstName = fName,
                LastName = lName
            };
        }
        #endregion
        #region Properties

        /// <summary>
        /// Gets/sets the customer's first name.  If this customer is a 
        /// company, this value stores the company's name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets/sets the customer's last name.  If this customer is a 
        /// company, this value is not set.
        /// </summary>
        public string LastName { get; set; }

        #endregion

        #region IDataErrorInfo
        public string this[string columnName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Error
        {
            get
            {
                return "Customer.IDataErrorInfo";// throw new NotImplementedException();
            }
        }
        #endregion
    }
}
