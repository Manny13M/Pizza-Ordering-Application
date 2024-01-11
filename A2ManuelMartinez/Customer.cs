using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2ManuelMartinez
{
    internal class Customer
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public int ContactNo { get; set; }
        public string Email { get; set; }

        public string PaymentMehtod { get; set; }
        public int CardNo { get; set; }
        public double AmountDue { get; set; }
        public double AmountPaid { get; set; }
        public double Change { get; set; }


        public Customer(string fname, string lname, string address, string province, string city, string postalCode, 
            int contactNo, string email, string paymentMethod, int cardNo, double amountDue, double amountPaid) 
        {
            this.Fname = fname;
            this.Lname = lname;
            this.Address = address;
            this.Province = province;
            this.City = city;
            this.PostalCode = postalCode;
            this.ContactNo = contactNo;
            this.Email = email;

            this.PaymentMehtod = paymentMethod;
            this.CardNo = cardNo;
            this.AmountDue = amountDue;
            this.AmountPaid = amountPaid;
            this.Change = amountPaid - amountDue;
        }
    }
}
