using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using AutoMapper;
using HospitalUi.Models;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Repository;
using Repository.Entities;

namespace HospitalUi.ViewModels
{
    public class EditVisitViewModel : BaseViewModel
    {
        public VisitDTO Visit { get; set; }
        public List<DoctorDTO> DoctorsColl { get; }
        public List<SexDTO> SexColl { get; }
        public List<ClientDTO> ClientsColl { get; }
        public List<VisitTypeDTO> VisitTypesColl { get; }
        public ClientDTO FindClient { get; set; } = new ClientDTO();
        public CollectionView ClientsView { get; private set; }
        public DelegateCommand CloseWindow { get; private set; }
        

        public EditVisitViewModel(
            IRepository<Client> clientRepository, 
            IRepository<Sex> sexRepository,
            IRepository<Doctor> doctorRepository,
            IRepository<VisitType> visitTypeRepository,
            IMapper mapper,
            VisitDTO visit)
        {
            Visit = visit;
            SexColl = mapper.Map<List<SexDTO>>(sexRepository.Get());
            DoctorsColl = mapper.Map<List<DoctorDTO>>(doctorRepository.Get());
            ClientsColl = mapper.Map<List<ClientDTO>>(clientRepository.Get());
            VisitTypesColl = mapper.Map<List<VisitTypeDTO>>(visitTypeRepository.Get());
            FindClient.PropertyChanged += FindClientOnPropertyChanged;
            ClientsView = (CollectionView)CollectionViewSource.GetDefaultView(ClientsColl);
            ClientsView.Filter = OnFilterClients;
            if (Visit.ClientId == Guid.Empty)
                Visit.ClientId = ClientsColl?.FirstOrDefault()?.Id ?? Guid.NewGuid();

            CloseWindow = new DelegateCommand(() =>
            {
                Visit.VisitType = VisitTypesColl.FirstOrDefault(x => x.Id == Visit.VisitId)?.VisitName;
                var client = ClientsColl.FirstOrDefault(x => x.Id == Visit.ClientId);
                Visit.ClientName = string.Join(" ", client?.Surname, client?.Name, client?.MiddleName);
                var doctor = DoctorsColl.FirstOrDefault(x => x.Id == Visit.DoctorId);
                Visit.DoctorName = string.Join(" ", doctor?.Surname, doctor?.Name, doctor?.MiddleName);
                DialogHost.CloseDialogCommand.Execute(true, null);
            });
        }

        private void FindClientOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ClientsView.Refresh();
        }

        private bool OnFilterClients(object obj)
        {
            var client = (ClientDTO)obj;
            return
                 (string.IsNullOrWhiteSpace(FindClient.Surname) || client.Surname.ToLower().Contains(FindClient.Surname.ToLower())) &&
                 (string.IsNullOrWhiteSpace(FindClient.Name) || (client.Name.ToLower().Contains(FindClient.Name.ToLower())) &&
                 (string.IsNullOrWhiteSpace(FindClient.MiddleName) || client.MiddleName.ToLower().Contains(FindClient.MiddleName.ToLower())) &&
                 (string.IsNullOrWhiteSpace(FindClient.PhoneNumber) || client.PhoneNumber.Contains(FindClient.PhoneNumber)));
        }
    }
}
