using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using AutoMapper;
using HospitalUi.Main;
using HospitalUi.Models;
using HospitalUi.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Entities;
using Serilog;

namespace HospitalUi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ILogger _logger;
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            DispatcherUnhandledException += OnDispatcherUnhandledException;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            _logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            var mainViewModel = ServiceProvider.GetRequiredService<MainViewModel>();
            mainWindow.DataContext = mainViewModel;
            mainWindow.Show();
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _logger.Error(e.Exception, e.Exception.Message);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MessageBoxModel>();
            services.AddTransient<EditVisitViewModel>();
            services.AddTransient<IRepository<VisitType>, Repository<VisitType>>();
            services.AddTransient<IRepository<Doctor>, Repository<Doctor>>();
            services.AddTransient<IRepository<Visit>, Repository<Visit>>();
            services.AddTransient<IRepository<Client>, Repository<Client>>();
            services.AddTransient<IRepository<Sex>, Repository<Sex>>();
            services.AddTransient<IRepository<Doctor>, Repository<Doctor>>();
            services.AddTransient<IRepository<Visit>, Repository<Visit>>();
            services.AddTransient<DbContext, DataContext>();
            services.AddTransient<ClientsViewModel>();
            services.AddTransient<VisitViewModel>();
            services.AddTransient<MainWindow>();
            services.AddScoped<MainViewModel>();
            services.AddTransient<Func<ClientDTO, EditClientViewModel>>((provider) => {
                return new Func<ClientDTO, EditClientViewModel>(
                    (client) => new EditClientViewModel(
                        provider.GetRequiredService<IRepository<Sex>>(),
                        provider.GetRequiredService<IMapper>(),
                        client)
                );
            });
            services.AddTransient<Func<VisitDTO, EditVisitViewModel>>((provider) => {
                return new Func<VisitDTO, EditVisitViewModel>(
                    (visit) => new EditVisitViewModel(
                        provider.GetRequiredService<IRepository<Client>>(),
                        provider.GetRequiredService<IRepository<Sex>>(),
                        provider.GetRequiredService<IRepository<Doctor>>(),
                        provider.GetRequiredService<IRepository<VisitType>>(),
                        provider.GetRequiredService<IMapper>(),
                        visit)
                );
            });
            services.AddSingleton(typeof(IMapper), new Mapper(CreateMapperConfiguration()));
            services.AddSingleton(_logger);
        }

        private MapperConfiguration CreateMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClientDTO, ClientDTO>();
                cfg.CreateMap<VisitType, VisitTypeDTO>();
                cfg.CreateMap<Client, ClientDTO>().ReverseMap();
                cfg.CreateMap<Sex, SexDTO>().ReverseMap();
                cfg.CreateMap<VisitDTO, VisitDTO>();
                cfg.CreateMap<Visit, VisitDTO>()
                    .ForMember(x => x.ClientName,
                        x => x.MapFrom(m => string.Join(" ", m.Client.Surname, m.Client.Name, m.Client.MiddleName)))
                    .ForMember(x => x.DoctorName,
                        x => x.MapFrom(m => string.Join(" ", m.Doctor.Surname, m.Doctor.Name, m.Doctor.MiddleName)))
                    .ForMember(x => x.VisitType, x => x.MapFrom(m => m.VisitType.VisitName));

                cfg.CreateMap<VisitDTO, Visit>()
                    .ForMember(x => x.Client, x => x.Ignore())
                    .ForMember(x => x.Doctor, x => x.Ignore())
                    .ForMember(x => x.VisitType, x => x.Ignore());
                    

                cfg.CreateMap<Doctor, DoctorDTO>()
                    .ForMember(x => x.Sex, x => x.MapFrom(m => m.Sex.Name));
            });

            return config;
        }
    }
}
