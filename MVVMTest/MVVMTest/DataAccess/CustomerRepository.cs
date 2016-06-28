using MVVMTest.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Resources;
using System.Xml;
using System.Xml.Linq;

namespace MVVMTest.DataAccess
{
    public class CustomerRepository
    {
        readonly List<Customer> _customers;

        public event EventHandler<CustomerAddedEventArgs> CustomerAdded;
        /// <summary>
        /// Creates a new repository of customers.
        /// </summary>
        /// <param name="customerDataFile">The relative path to an XML resource file that contains customer data.</param>
        public CustomerRepository(string customerDataFile)
        {
            _customers = LoadCustomers(customerDataFile);
        }

        /// <summary>
        /// Returns a shallow-copied list of all customers in the repository.
        /// </summary>
        public List<Customer> GetCustomers()
        {
            return new List<Customer>(_customers);
        }

        public bool ContainsCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentException("customer");

            return _customers.Contains(customer);
        }

        public void AddCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentException("null customer");

            if(!_customers.Contains(customer))
            {
                _customers.Add(customer);

                if (this.CustomerAdded != null)
                    this.CustomerAdded(this, new CustomerAddedEventArgs(customer));
            }
        }

        #region Import Customer Data
        static List<Customer> LoadCustomers(string customerDataFile)
        {
            // In a real application, the data would come from an external source,
            // but for this demo let's keep things simple and use a resource file.
            using (Stream stream = GetResourceStream(customerDataFile))
            using (XmlReader xmlRdr = new XmlTextReader(stream))
                return
                    (from customerElem in XDocument.Load(xmlRdr).Element("customers").Elements("customer")
                     select Customer.CreateCustomer(
                        
                        (string)customerElem.Attribute("firstName"),
                        (string)customerElem.Attribute("lastName"),
                        (string)customerElem.Attribute("email"),
                        (double)customerElem.Attribute("totalSales"),
                        (bool)customerElem.Attribute("isCompany")
                         )).ToList();
        }
        static Stream GetResourceStream(string resourceFile)
        {
            Uri uri = new Uri(resourceFile, UriKind.RelativeOrAbsolute);

            StreamResourceInfo info = Application.GetResourceStream(uri);
            if (info == null || info.Stream == null)
                throw new ApplicationException("Missing resource file: " + resourceFile);

            return info.Stream;
        }
        #endregion

    }
}
