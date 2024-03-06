using System;
using System.Collections.Generic;

namespace ArturTI225.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public Client Client { get; set; } // Clientul care a plasat comanda
        public List<Product> Products { get; set; } // Lista de produse din comandă
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        // Alte proprietăți specifice comenzii
    }
}