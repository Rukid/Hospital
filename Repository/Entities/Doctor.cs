using System;
using System.Collections.Generic;

namespace Repository.Entities
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string Speciality { get; set; }
        public int SexId { get; set; }
        public Sex Sex { get; set; }
        public ICollection<Visit> Visits { get; set; }
    }
}
