using System;
using System.Collections.Generic;

namespace Repository.Entities
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public DateTime BurthDate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int SexId { get; set; }

        public Sex Sex { get; set; }
        public ICollection<Visit> Visits { get; set; }
    }
}
