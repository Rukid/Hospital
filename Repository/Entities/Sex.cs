using System.Collections.Generic;

namespace Repository.Entities
{
    public class Sex
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
