using CleanCode.Comments;
using System;
using System.Collections.Generic;

namespace CleanCode.OutputParameters
{
    public class CustomersResult
    {

        public IEnumerable<Customer> Customers { get; set; }
        public int TotalCount { get; set; }
    }

    public class OutputParameters
    {
        public void DisplayCustomers()
        {
            var customersResult = GetCustomers(pageIndex: 1);

            Console.WriteLine("Total customers: " + customersResult.TotalCount);
            foreach (var c in customersResult.Customers)
                Console.WriteLine(c);
        }

        public CustomersResult GetCustomers(int pageIndex)
        {
            var totalCount = 100;
            return new CustomersResult(){TotalCount = totalCount, Customers = new List<Customer>()};
        }
    }
}
