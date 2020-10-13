using System;
using System.Collections.Generic;
using System.Linq;
using HospitalUi.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Entities;

namespace HospitalUi.Main
{
    public class MainViewModel:BaseViewModel
    {
        private IRepository<Client> _clientRepository;
        private IRepository<Doctor> _doctorRepository;
        private IRepository<Visit> _visitRepository;

        public MainViewModel(IServiceProvider services)
        {
            _clientRepository = services.GetRequiredService<IRepository<Client>>();
            _doctorRepository = services.GetRequiredService<IRepository<Doctor>>();
            _visitRepository = services.GetRequiredService<IRepository<Visit>>();

            Pages = new List<BasePageViewModel>();
            Pages.Add(services.GetRequiredService<ClientsViewModel>());
            Pages.Add(services.GetRequiredService<VisitViewModel>());
            _selectedPage = Pages.FirstOrDefault();
        }

        private BasePageViewModel _selectedPage;
        public BasePageViewModel SelectedPage
        {
            get => _selectedPage;
            set => SetProperty(ref _selectedPage, value, () =>
            {
                MenuIsOpen = false;
                value.GetData();
                value.Refresh();
            });
        }

        private bool _menuIsOpen;
        public bool MenuIsOpen
        {
            get => _menuIsOpen;
            set => SetProperty(ref _menuIsOpen, value);
        }

        public List<BasePageViewModel> Pages { get; }
    }
}
