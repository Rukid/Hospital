using System;
using HospitalUi.ViewModels;

namespace HospitalUi.Models
{
    public class VisitDTO : BaseViewModel
    {
        public Guid Id { get; set; }

        private DateTime _visitDate = DateTime.Today;
        public DateTime VisitDate
        {
            get => _visitDate;
            set => SetProperty(ref _visitDate, value);
        }

        private string _clientName;
        public string ClientName
        {
            get => _clientName;
            set => SetProperty(ref _clientName, value);
        }

        private string _doctorName;
        public string DoctorName
        {
            get => _doctorName;
            set => SetProperty(ref _doctorName, value);
        }
       
        private string _visitType;
        public string VisitType
        {
            get => _visitType;
            set => SetProperty(ref _visitType, value);
        }

        private string _diagnosis;
        public string Diagnosis
        {
            get => _diagnosis;
            set => SetProperty(ref _diagnosis, value);
        }
      
        public int VisitId { get; set; }
        public Guid ClientId { get; set; }
        public Guid DoctorId { get; set; }
    }
}
