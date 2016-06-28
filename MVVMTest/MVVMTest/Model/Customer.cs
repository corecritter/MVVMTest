using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MVVMTest.Model
{
    public class Customer : IDataErrorInfo
    {
        #region Creation
        public static Customer CreateNewCustomer()
        {
            return new Customer();
        }
        public static Customer CreateCustomer(string fName, string lName, string email, double totalSales, bool isCompany)
        {
            return new Customer()
            {
                FirstName = fName,
                LastName = lName,
                Email = email,
                TotalSales = totalSales,
                IsCompany = isCompany
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

        public bool IsCompany { get; set; }

        public string Email { get; set; }

        public double TotalSales { get; set; }
        #endregion

        #region IDataErrorInfo
        public string this[string propertyName]
        {
            get
            {
                return this.GetValidationError(propertyName);
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

        #region Validation

        static readonly string[] ValidatedProperties =
        {
            "FirstName",
            "LastName",
            "Email"
        };

        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                    if (GetValidationError(property) != null)
                        return false;
                return true;
            }
        }

        string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;
            string error = null;

            switch(propertyName)
            {
                case "FirstName":
                    error = this.ValidateFirstName();
                    break;

                case "LastName":
                    error = this.ValidateLastName();
                    break;

                case "Email":
                    error = this.ValidateEmail();
                    break;

                //default:
            }

            return error;
        }

        string ValidateFirstName()
        {
            if (IsStringMissing(this.FirstName))
                return "Invalid First Name";
            return null;
        }

        string ValidateLastName()
        {
            if (IsStringMissing(this.LastName))
                return "Invalid Last Name";
            return null;
        }

        string ValidateEmail()
        {
            if (IsStringMissing(this.Email))
                return "Enter Email";
            else if (!IsValidEmailAddress(this.Email))
                return "Invalid Email";
            return null;
        }

        static bool IsStringMissing(string value)
        {
            return String.IsNullOrEmpty(value) || value.Trim() == String.Empty;
        }

        static bool IsValidEmailAddress(string email)
        {
            if (IsStringMissing(email))
                return false;

            // This regex pattern came from: http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
        #endregion
    }
}
