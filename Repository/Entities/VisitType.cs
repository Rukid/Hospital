using System.Collections.Generic;

namespace Repository.Entities
{
    public class VisitType
    {
        public int Id { get; set; }
        public string VisitName { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
