using System;
using HospitalUi.ViewModels;

namespace HospitalUi.Models
{
    public class DoctorDTO : BaseViewModel
    {
        public Guid Id { get; set; }
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _surname;
        public string Surname
        {
            get => _surname;
            set => SetProperty(ref _surname, value);
        }

        private string _middleName;
        public string MiddleName
        {
            get => _middleName;
            set => SetProperty(ref _middleName, value);
        }

        public string Speciality { get; set; }
        public string Sex { get; set; }
    }
}
