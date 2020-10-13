using System;
using System.ComponentModel.DataAnnotations;
using HospitalUi.Validation;

namespace HospitalUi.Models
{
    public class ClientDTO : PropertyValidateModel
    {
        public Guid Id { get; set; }

        private string _name;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(50, ErrorMessage = "Длина имени не больше 50 символов")]
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _surname;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(50, ErrorMessage = "Длина фамилии не больше 50 символов")]
        public string Surname
        {
            get => _surname;
            set => SetProperty(ref _surname, value);
        }
       
        private string _middleName;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(50, ErrorMessage = "Длина фамилии не больше 50 символов")]
        public string MiddleName
        {
            get => _middleName;
            set => SetProperty(ref _middleName, value);
        }
      
        private DateTime _burthDate = new DateTime(1950, 1, 1);
        [Required]
        public DateTime BurthDate
        {
            get => _burthDate;
            set => SetProperty(ref _burthDate, value);
        }

        private string _address;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(50, ErrorMessage = "Длина адреса не больше 200 символов")]
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }
    
        private string _phoneNumber;
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [RegularExpression(@"\+?[0-9]{10}", ErrorMessage = "Введите корректный номер телефона (10 символов)")]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        private int _sexId;
        public int SexId
        {
            get => _sexId;
            set => SetProperty(ref _sexId, value);
        }
    }
}
