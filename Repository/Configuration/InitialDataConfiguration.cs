using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RandomNameGenerator;
using Repository.Entities;

namespace Repository.Configuration
{
    public class InitialDataConfiguration
    {
        private static string[] speciality = new[] {"терапевт", "лор", "хирург"};
        const string LoremIpsum = @"Lorem, ipsum dolor";
        static Random random = new Random();
       
        public static void CreateData(ModelBuilder modelBuilder)
        {
            var clients = CreateClients();
            var doctors = CreateDoctors();
            var visits = CreateVisits(doctors, clients);
            modelBuilder.Entity<VisitType>().HasData(CreateVisitTypes());
            modelBuilder.Entity<Sex>().HasData(CreateSex());
            modelBuilder.Entity<Client>().HasData(clients);
            modelBuilder.Entity<Doctor>().HasData(doctors);
            modelBuilder.Entity<Visit>().HasData(visits);
        }

        private static List<Sex> CreateSex()
        {
            List<Sex> sex = new List<Sex>();
            sex.Add(new Sex() {Id = 1, Name = "мужской"});
            sex.Add(new Sex() { Id = 2, Name = "женский" });
            return sex;
        }

        private static List<VisitType> CreateVisitTypes()
        {
            List<VisitType> visitType = new List<VisitType>();
            visitType.Add(new VisitType() { Id = 1, VisitName = "первичный" });
            visitType.Add(new VisitType() { Id = 2, VisitName = "вторичный" });
            return visitType;
        }

        private static List<Client> CreateClients()
        {
            List<Client> clients = new List<Client>();
            for (int i = 0; i < 40; i++)
            {
                var sex = random.Next(1, 3);
                clients.Add(new Client()
                {
                    Id = Guid.NewGuid(),
                    Name = NameGenerator.GenerateFirstName(sex == 1 ? Gender.Male : Gender.Female),
                    Surname = NameGenerator.GenerateFirstName(sex == 1 ? Gender.Male : Gender.Female),
                    MiddleName = NameGenerator.GenerateFirstName(sex == 1 ? Gender.Male : Gender.Female),
                    SexId = sex,
                    PhoneNumber = RandomPhone(10),
                    Address = String.Join(' ', Enumerable.Repeat(LoremIpsum, 2)),
                    BurthDate = RandomBurthDate()
                });
            }
            return clients;
        }

        private static List<Doctor> CreateDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            for (int i = 0; i < 5; i++)
            {
                var sex = random.Next(1, 3);
                doctors.Add(new Doctor()
                {
                    Id = Guid.NewGuid(),
                    Name = NameGenerator.GenerateFirstName(sex == 1 ? Gender.Male : Gender.Female),
                    Surname = NameGenerator.GenerateFirstName(sex == 1 ? Gender.Male : Gender.Female),
                    MiddleName = NameGenerator.GenerateFirstName(sex == 1 ? Gender.Male : Gender.Female),
                    SexId = sex,
                    Speciality = speciality[random.Next(0, speciality.Length)]
                });
            }
            return doctors;
        }

        private static List<Visit> CreateVisits(List<Doctor> doctors, List<Client> clients)
        {
            List<Visit> visits = new List<Visit>();
            for (int i = 0; i < 20; i++)
            {
                var visitType = random.Next(1, 3);
                visits.Add(new Visit()
                {
                    Id = Guid.NewGuid(),
                    DoctorId = doctors[random.Next(0, doctors.Count)].Id,
                    ClientId = clients[random.Next(0, clients.Count)].Id,
                    Diagnosis = visitType == 2 ? String.Join(' ', Enumerable.Repeat(LoremIpsum, 4)) : "",
                    VisitId = visitType,
                    VisitDate = RandomVisitDate()
                });
            }
            return visits;
        }

        private static DateTime RandomBurthDate()
        {
            DateTime start = new DateTime(1950, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }

        private static DateTime RandomVisitDate()
        {
            DateTime start = new DateTime(2020, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }

        private static string RandomPhone(int length)
        {
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
    }
}
