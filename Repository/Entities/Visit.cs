using System;

namespace Repository.Entities
{
    public class Visit
    {
        public Guid Id { get; set; }
        public DateTime VisitDate { get; set; }
        public string Diagnosis { get; set; }
        public int VisitId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid ClientId { get; set; }
        public VisitType VisitType { get; set; }
        public Client Client { get; set; }
        public Doctor Doctor { get; set; }
    }
}
