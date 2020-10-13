using System;
using System.Collections.ObjectModel;
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
    public class VisitViewModel : BasePageViewModel
    {
        #region private

        private IServiceProvider _services;
        private IRepository<Visit> _visitRepository;
        private ILogger _logger;
        private IMapper _mapper;

        #endregion

        private VisitDTO _selectedVisit;
        public VisitDTO SelectedVisit
        {
            get => _selectedVisit;
            set => SetProperty(ref _selectedVisit, value);
        }

        public override string Title { get; } = "Посещения";
        public ObservableCollection<VisitDTO> Visits { get; private set; }
        public CollectionView VisitsView { get; private set; }
        public DelegateCommand RemoveVisitCommand { get; private set; }
        public DelegateCommand AddVisitCommand { get; private set; }
        public DelegateCommand EditVisitCommand { get; private set; }

        public VisitViewModel(IServiceProvider services)
        {
            _services = services;
            _visitRepository = _services.GetRequiredService<IRepository<Visit>>();
            _logger = _services.GetRequiredService<ILogger>();
            _mapper = _services.GetRequiredService<IMapper>();
            GetData();
            VisitsView.Filter = OnFilterClients;
            InitCommands();
        }

        public sealed override void GetData()
        {
            Visits = _mapper.Map<ObservableCollection<VisitDTO>>(_visitRepository.GetWithInclude(x => x.Client, o => o.Doctor, p => p.VisitType));
            VisitsView = GetClientsCollectionView(Visits);
        }

        public override void Refresh()
        {
            VisitsView.Refresh();
        }

        private void InitCommands()
        {
            AddVisitCommand = new DelegateCommand(async () => { await AddVisit(); });
            RemoveVisitCommand = new DelegateCommand(async () => { await RemoveVisit(); });
            EditVisitCommand = new DelegateCommand(async () => { await EditVisit(); });
        }

        private async Task EditVisit()
        {
            if (SelectedVisit == null) return;
            var index = Visits.IndexOf(SelectedVisit);
            var vm = _services.GetRequiredService<Func<VisitDTO, EditVisitViewModel>>()(_mapper.Map<VisitDTO>(SelectedVisit));

            var result = await DialogHost.Show(vm, "RootDialog");
            if (result is bool res)
            {
                if (res)
                {
                    var visit = _visitRepository.FindById(vm.Visit.Id);
                    _visitRepository.Update(_mapper.Map(vm.Visit, visit));
                    Visits[index] = vm.Visit;
                    SelectedVisit = vm.Visit;
                    _logger.Debug($"Visit changed (id-{vm.Visit.Id})");
                }
            }
        }

        private async Task RemoveVisit()
        {
            var vm = _services.GetRequiredService<MessageBoxModel>();
            vm.Title = "Внимание";
            vm.Text = "Удалить запись?";
            var result = await DialogHost.Show(vm, "RootDialog");
            if (result is bool res)
            {
                if (res)
                {
                    var id = SelectedVisit.Id;
                    _visitRepository.Remove(_mapper.Map<Visit>(SelectedVisit));
                    Visits.Remove(SelectedVisit);
                    _logger.Debug($"Visit deleted (id-{id})");
                }
            }
        }

        private async Task AddVisit()
        {
            var vm = _services.GetRequiredService<Func<VisitDTO, EditVisitViewModel>>()(new VisitDTO());

            var result = await DialogHost.Show(vm, "RootDialog");
            if (result is bool res)
            {
                if (res)
                {
                    var newVisit = _mapper.Map<Visit>(vm.Visit);
                    _visitRepository.Create(newVisit);
                    Visits.Add(vm.Visit);
                    SelectedVisit = vm.Visit;
                    _logger.Debug($"Visit added (id-{vm.Visit.Id})");
                }
            }
        }

        private bool OnFilterClients(object obj)
        {
            return true;
        }

        private CollectionView GetClientsCollectionView(ObservableCollection<VisitDTO> visits)
        {
            return (CollectionView)CollectionViewSource.GetDefaultView(visits);
        }
    }
}
