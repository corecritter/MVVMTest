using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMTest.Model
{
    public class CustomerAddedEventArgs : EventArgs
    {
        public Customer NewCustomer { get; private set; }

        public CustomerAddedEventArgs(Customer newCustomer)
        {
            this.NewCustomer = newCustomer;
        }
    }
}
