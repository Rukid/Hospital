using System.Collections.Generic;
using AutoMapper;
using HospitalUi.Models;
using Repository;
using Repository.Entities;

namespace HospitalUi.ViewModels
{
    public class EditClientViewModel : BaseViewModel
    {
        public ClientDTO Client { get; }
        public List<SexDTO> SexColl { get; }

        public EditClientViewModel(IRepository<Sex> sexRepository, IMapper mapper, ClientDTO client)
        {
            SexColl = mapper.Map<List<SexDTO>>(sexRepository.Get());
            Client = client;
        }
    }
}
