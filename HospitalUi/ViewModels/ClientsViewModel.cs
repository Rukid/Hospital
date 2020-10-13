using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using AutoMapper;
using HospitalUi.Models;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Prism.Commands;
using Repository;
using Repository.Entities;
using Serilog;

namespace HospitalUi.ViewModels
{
    public class ClientsViewModel : BasePageViewModel
    {
        private IServiceProvider _services;
        private ILogger _logger;
        private IRepository<Client> _clientRepository;
        private IRepository<Sex> _sexRepository;
        private IMapper _mapper;

        public override string Title { get; } = "Клиенты";
        public ObservableCollection<ClientDTO> Clients { get; private set; }
        public List<SexDTO> SexColl { get; }
        public CollectionView ClientsView { get; private set; }
        public DelegateCommand RemoveClientCommand { get; private set; }
        public DelegateCommand AddClientCommand { get; private set; }
        public DelegateCommand EditClientCommand { get; private set; }

        private ClientDTO _selectedClient;
        public ClientDTO SelectedClient
        {
            get => _selectedClient;
            set => SetProperty(ref _selectedClient, value);
        }

        public ClientsViewModel(IServiceProvider services)
        {
            _services = services;
            _logger = _services.GetRequiredService<ILogger>();
            _clientRepository = _services.GetRequiredService<IRepository<Client>>();
            _sexRepository = _services.GetRequiredService<IRepository<Sex>>();
            _mapper = _services.GetRequiredService<IMapper>();
            SexColl = _mapper.Map<List<SexDTO>>(_sexRepository.Get());
            GetData();
            ClientsView.Filter = OnFilterClients;
            InitCommands();
        }

        public sealed override void GetData()
        {
            Clients = _mapper.Map<ObservableCollection<ClientDTO>>(_clientRepository.GetWithInclude(x => x.Sex));
            ClientsView = GetClientsCollectionView(Clients);
            SelectedClient = Clients.FirstOrDefault();
        }

        public override void Refresh()
        {
            ClientsView.Refresh();
        }

        private void InitCommands()
        {
            AddClientCommand = new DelegateCommand(async () => { await AddClient(); });
            RemoveClientCommand = new DelegateCommand(async () => { await RemoveClient(); });
            EditClientCommand = new DelegateCommand(async () => { await EditClient();});
        }

        private bool OnFilterClients(object obj)
        {
            return true;
        }

        private CollectionView GetClientsCollectionView(ObservableCollection<ClientDTO> clients)
        {
            return (CollectionView)CollectionViewSource.GetDefaultView(clients);
        }

        private async Task EditClient()
        {
            if (SelectedClient == null) return;
            var index = Clients.IndexOf(SelectedClient);
            var vm = _services.GetRequiredService<Func<ClientDTO, EditClientViewModel>>()(_mapper.Map<ClientDTO>(SelectedClient));
          
            var result = await DialogHost.Show(vm, "RootDialog");
            if (result is bool res)
            {
                if (res)
                {
                    var client = _clientRepository.FindById(vm.Client.Id);
                    _clientRepository.Update(_mapper.Map(vm.Client, client));
                    Clients[index] = vm.Client;
                    SelectedClient = vm.Client;
                    _logger.Debug($"Client changed (id-{vm.Client.Id})");
                }
            }
        }

        private async Task AddClient()
        {
            var vm = _services.GetRequiredService<Func<ClientDTO, EditClientViewModel>>()(new ClientDTO(){ SexId = SexColl?.FirstOrDefault()?.Id ?? 0});

            var result = await DialogHost.Show(vm, "RootDialog");
            if (result is bool res)
            {
                if (res)
                {
                    vm.Client.Id = Guid.NewGuid();
                    var newClient = _mapper.Map<Client>(vm.Client);
                    _clientRepository.Create(newClient);
                    Clients.Add(vm.Client);
                    SelectedClient = vm.Client;
                    _logger.Debug($"Client added (id-{vm.Client.Id})");
                }
            }
        }

        private async Task RemoveClient()
        {
            var vm = _services.GetRequiredService<MessageBoxModel>();
            vm.Title = "Внимание";
            vm.Text = "Удалить запись?";
            var result = await DialogHost.Show(vm, "RootDialog");
            if (result is bool res)
            {
                if (res)
                {
                    var id = SelectedClient.Id;
                    _clientRepository.Remove(_mapper.Map<Client>(SelectedClient));
                    Clients.Remove(SelectedClient);
                    _logger.Debug($"Client deleted (id-{id})");
                }
            }
        }

    }
}
