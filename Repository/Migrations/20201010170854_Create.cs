using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sex",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sex", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VisitTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitName = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Surname = table.Column<string>(maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: false),
                    BurthDate = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    SexId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Sex_SexId",
                        column: x => x.SexId,
                        principalTable: "Sex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Surname = table.Column<string>(maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: false),
                    Speciality = table.Column<string>(maxLength: 30, nullable: false),
                    SexId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Sex_SexId",
                        column: x => x.SexId,
                        principalTable: "Sex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    VisitDate = table.Column<DateTime>(nullable: false),
                    Diagnosis = table.Column<string>(maxLength: 1000, nullable: false),
                    VisitId = table.Column<int>(nullable: false),
                    DoctorId = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visits_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Visits_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Visits_VisitTypes_VisitId",
                        column: x => x.VisitId,
                        principalTable: "VisitTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Sex",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "мужской" },
                    { 2, "женский" }
                });

            migrationBuilder.InsertData(
                table: "VisitTypes",
                columns: new[] { "Id", "VisitName" },
                values: new object[,]
                {
                    { 1, "первичный" },
                    { 2, "вторичный" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "BurthDate", "MiddleName", "Name", "PhoneNumber", "SexId", "Surname" },
                values: new object[,]
                {
                    { new Guid("fab43ad7-fe6c-4fd2-8267-1d8032600a8f"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1996, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "JOHNATHON", "ALEXIS", "7270877602", 1, "JARED" },
                    { new Guid("fbae2f1d-91f3-4fea-b78d-96738e5d1f49"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(2011, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "KATHERINE", "SALLY", "6951344133", 2, "CHASITY" },
                    { new Guid("a5357326-8d97-44e4-8a98-10bb34a61977"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1972, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "EFFIE", "BEULAH", "5628459085", 2, "CANDICE" },
                    { new Guid("f54a06f5-5bfd-4594-8921-4a2405c882df"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1958, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "NATALIA", "EUGENIA", "5571215100", 2, "GLENNA" },
                    { new Guid("841bf23e-caa1-4377-8ea8-f2ba8dd144ca"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1993, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "ANGELA", "SIERRA", "6775290056", 2, "NORA" },
                    { new Guid("a30439f1-4f75-435a-8b48-c251647a1fe3"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1989, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "ROSLYN", "TARA", "2668375921", 2, "JOANNE" },
                    { new Guid("b151e1d1-dc9e-4783-b3b2-ab07c1c1602b"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1961, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "LULA", "OFELIA", "2324027444", 2, "MARCELLA" },
                    { new Guid("0e0e995e-9be3-47b5-b96d-f926c296129b"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(2017, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "BETTY", "LAVERNE", "7489433890", 2, "TAMERA" },
                    { new Guid("ccf0889a-c33f-4b10-aadb-9e72549d456e"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1970, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "BROOKE", "ROSLYN", "4772514520", 2, "JACLYN" },
                    { new Guid("7e70b103-68cc-48ff-8a88-262d98eab2cb"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1955, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORA", "JUSTINA", "6692812740", 2, "ESTELLE" },
                    { new Guid("8e10422d-440c-47a1-ab88-a54656a84179"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(2017, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "JUANITA", "ELIZABETH", "9659868146", 2, "PAULA" },
                    { new Guid("ea8806e1-f32a-4dd7-ac89-185d15c5b455"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1971, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "STEPHANIE", "CHRISTI", "3007811468", 2, "MELISA" },
                    { new Guid("9e8f8eb4-68c7-477c-b53c-98928a4ef418"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(2007, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "FLORENCE", "PAIGE", "6565214965", 2, "RUTHIE" },
                    { new Guid("4d6a5dbe-3443-4356-a509-2787ba157c7d"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1999, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "JULIET", "ALICE", "6032456620", 2, "FLOSSIE" },
                    { new Guid("5e9c99b6-b7fe-4981-974a-b32dfc334afd"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(2009, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "TIFFANY", "LUELLA", "3382925989", 2, "AIMEE" },
                    { new Guid("ff0ab253-a06c-4ac8-b7c2-90ce559cd6ad"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(2010, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "EUNICE", "INGRID", "9859584043", 2, "YOUNG" },
                    { new Guid("ea8b9b05-2a9a-4250-9e33-f068f262c908"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1965, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "PENELOPE", "TERRA", "9349575505", 2, "WINIFRED" },
                    { new Guid("b6d1122d-7dad-4526-b155-ad9d07edd3d2"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1997, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "LESLIE", "ERIN", "2552478454", 2, "BROOKE" },
                    { new Guid("7218b9c7-b7a6-4a38-bcb2-42de26d42e56"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1982, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "CLARICE", "PATRICE", "5955388947", 2, "LOIS" },
                    { new Guid("d6f162ff-0d8e-40dc-a5c7-801bac7a1123"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1984, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "CHASITY", "LESSIE", "8586913414", 2, "BEATRIZ" },
                    { new Guid("ba375c81-d7e5-4815-bb6e-be4bf73e4770"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(2008, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "NETTIE", "LAURA", "5786826319", 2, "LYNNETTE" },
                    { new Guid("562f47bc-fe9d-4d04-ab72-ad4ebc0ae3da"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1997, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "EDWINA", "LORRIE", "5067441910", 2, "BERYL" },
                    { new Guid("523160c9-fc0b-4b9e-9a0d-fb165c1567ba"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1998, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "JEREMY", "MARCEL", "2086671887", 1, "GARRY" },
                    { new Guid("166efc29-f7c3-4dc4-961c-5533e541626c"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(2017, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "RAFAEL", "JOHNNY", "7786672658", 1, "GERARDO" },
                    { new Guid("541cd224-f4fb-4459-9dfc-83c8297eaff7"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1976, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "MICKEY", "ROBIN", "5434980428", 1, "VICENTE" },
                    { new Guid("eebb2e8e-8500-47e7-b1f2-3f6880eaee54"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1954, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "NEAL", "VIRGIL", "8597329325", 1, "IRVING" },
                    { new Guid("978c32a9-7c5d-4269-8027-7bcd36fc8c3f"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1953, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "RAYMOND", "MARVIN", "3877856019", 1, "ELIJAH" },
                    { new Guid("7c1e872e-1d17-4103-b1e5-76369a75e5aa"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1986, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "ALEXANDER", "DARREL", "2759711662", 1, "DEWEY" },
                    { new Guid("f0f8dbb5-e7e6-4ec0-aaaa-788aac5ad631"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1966, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "JAIME", "DARREN", "8309906067", 1, "LINWOOD" },
                    { new Guid("ceef74c6-a2d1-46d9-87dd-87d0706bfa60"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1980, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "WILEY", "SALVADOR", "4611295556", 1, "ODELL" },
                    { new Guid("2292c6b7-5aca-4ad0-bcb6-5eabf3c9a8ff"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(2013, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "LUCINDA", "LESSIE", "1991743531", 2, "JACQUELINE" },
                    { new Guid("98b0a338-a7ef-4fad-9017-aa60b67df726"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1972, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "REX", "WILFREDO", "4593964619", 1, "TYSON" },
                    { new Guid("06ecb6b1-28d1-47bb-b8f1-169ec6965946"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1992, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "CEDRIC", "WAYNE", "9940431726", 1, "ED" },
                    { new Guid("a5afceff-ef61-488d-86a2-e50c999a3673"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(2007, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "DELMAR", "JUNIOR", "8251811013", 1, "EDGAR" },
                    { new Guid("24e45e4f-6d77-4ab8-8ef8-de8cce85dc13"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1963, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "ROMEO", "SAM", "7469475002", 1, "LINWOOD" },
                    { new Guid("50248e8c-91de-487d-9746-949f758d2bfa"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1973, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "KENNETH", "JAN", "8081558051", 1, "ALPHONSO" },
                    { new Guid("1e690e96-7880-4bf5-a8da-5f11b44d7f88"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1979, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "RON", "DONALD", "7640487444", 1, "THEODORE" },
                    { new Guid("6e156be6-1041-4035-b760-c80e1f2ed6f5"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1960, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "PETE", "DION", "4154775763", 1, "PIERRE" },
                    { new Guid("7fb5f305-47c3-4d6c-a097-0425d64c5976"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1958, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "BENNIE", "CESAR", "9337665349", 1, "RONALD" },
                    { new Guid("90f17a90-5b40-43c5-82f0-cd9685c57ab5"), "Lorem, ipsum dolor Lorem, ipsum dolor", new DateTime(1993, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "VAN", "HEATH", "1877538036", 1, "ERROL" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "MiddleName", "Name", "SexId", "Speciality", "Surname" },
                values: new object[,]
                {
                    { new Guid("9ec77a66-5395-4528-872b-6b93d92b6af9"), "ALBERTO", "MARC", 1, "лор", "JIMMIE" },
                    { new Guid("eaa6ebfc-083f-404c-98a9-660d840963e0"), "MELVA", "LACEY", 2, "лор", "OFELIA" },
                    { new Guid("a8f4ee8f-88de-4b78-8aa9-0242ae9b0397"), "OLLIE", "LUISA", 2, "хирург", "MANDY" },
                    { new Guid("bf57ce4e-378f-42ed-974c-5561f41fa0e3"), "MAGDALENA", "KIMBERLEY", 2, "хирург", "JANELL" },
                    { new Guid("f4fc19bf-7c61-47b6-8744-dab753ea9add"), "DIANE", "LILLIAN", 2, "хирург", "TANISHA" }
                });

            migrationBuilder.InsertData(
                table: "Visits",
                columns: new[] { "Id", "ClientId", "Diagnosis", "DoctorId", "VisitDate", "VisitId" },
                values: new object[,]
                {
                    { new Guid("70c75ce3-5088-47c3-83ab-95aac52918f1"), new Guid("eebb2e8e-8500-47e7-b1f2-3f6880eaee54"), "Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor", new Guid("9ec77a66-5395-4528-872b-6b93d92b6af9"), new DateTime(2020, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("27ae0445-e961-4364-8eec-973697d85498"), new Guid("166efc29-f7c3-4dc4-961c-5533e541626c"), "Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor", new Guid("f4fc19bf-7c61-47b6-8744-dab753ea9add"), new DateTime(2020, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("3b6dac21-e189-416d-a31e-4fab14883598"), new Guid("06ecb6b1-28d1-47bb-b8f1-169ec6965946"), "Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor", new Guid("f4fc19bf-7c61-47b6-8744-dab753ea9add"), new DateTime(2020, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("ea4deae0-5d70-4d8e-a143-cab5811393f7"), new Guid("ea8806e1-f32a-4dd7-ac89-185d15c5b455"), "", new Guid("f4fc19bf-7c61-47b6-8744-dab753ea9add"), new DateTime(2020, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("fad55e74-b907-4a33-9fde-7993afea98de"), new Guid("a30439f1-4f75-435a-8b48-c251647a1fe3"), "Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor", new Guid("f4fc19bf-7c61-47b6-8744-dab753ea9add"), new DateTime(2020, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("03d181c9-19ef-4550-a28c-9dad67700f45"), new Guid("0e0e995e-9be3-47b5-b96d-f926c296129b"), "Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor", new Guid("f4fc19bf-7c61-47b6-8744-dab753ea9add"), new DateTime(2020, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("0908b0d2-1698-42b3-a20f-77c8ba40e2f6"), new Guid("6e156be6-1041-4035-b760-c80e1f2ed6f5"), "", new Guid("bf57ce4e-378f-42ed-974c-5561f41fa0e3"), new DateTime(2020, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("8d3b39cd-6edd-4c07-874f-4145a7b2411e"), new Guid("fbae2f1d-91f3-4fea-b78d-96738e5d1f49"), "Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor", new Guid("bf57ce4e-378f-42ed-974c-5561f41fa0e3"), new DateTime(2020, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("05f57372-f6b7-4295-8ff0-1cb3660fbebc"), new Guid("ceef74c6-a2d1-46d9-87dd-87d0706bfa60"), "", new Guid("bf57ce4e-378f-42ed-974c-5561f41fa0e3"), new DateTime(2020, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("c59ef386-8260-41f9-a297-8484d2f4d63a"), new Guid("eebb2e8e-8500-47e7-b1f2-3f6880eaee54"), "Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor", new Guid("bf57ce4e-378f-42ed-974c-5561f41fa0e3"), new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("a66fb87d-e3b3-4ebf-a57c-ceb287230337"), new Guid("90f17a90-5b40-43c5-82f0-cd9685c57ab5"), "", new Guid("bf57ce4e-378f-42ed-974c-5561f41fa0e3"), new DateTime(2020, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("3f01dc92-e9ad-4468-9e3c-2c53fad46c53"), new Guid("ceef74c6-a2d1-46d9-87dd-87d0706bfa60"), "Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor", new Guid("bf57ce4e-378f-42ed-974c-5561f41fa0e3"), new DateTime(2020, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("3949a1f3-8ca9-4407-a558-91e373d7ad36"), new Guid("a5afceff-ef61-488d-86a2-e50c999a3673"), "Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor", new Guid("a8f4ee8f-88de-4b78-8aa9-0242ae9b0397"), new DateTime(2020, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("06771809-4981-499c-aaf6-5627ab19d98d"), new Guid("6e156be6-1041-4035-b760-c80e1f2ed6f5"), "", new Guid("a8f4ee8f-88de-4b78-8aa9-0242ae9b0397"), new DateTime(2020, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("01f47bdc-a323-4d4d-bc1a-776bb152a5ba"), new Guid("841bf23e-caa1-4377-8ea8-f2ba8dd144ca"), "Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor", new Guid("9ec77a66-5395-4528-872b-6b93d92b6af9"), new DateTime(2020, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("29267c3d-33f1-4bfc-86fd-cf3c6e125bc6"), new Guid("fbae2f1d-91f3-4fea-b78d-96738e5d1f49"), "Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor", new Guid("9ec77a66-5395-4528-872b-6b93d92b6af9"), new DateTime(2020, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("506143cf-01af-4b00-8615-ea750f1a0201"), new Guid("eebb2e8e-8500-47e7-b1f2-3f6880eaee54"), "Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor", new Guid("9ec77a66-5395-4528-872b-6b93d92b6af9"), new DateTime(2020, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("587167cc-1762-427a-8ab7-72433bc4977c"), new Guid("541cd224-f4fb-4459-9dfc-83c8297eaff7"), "", new Guid("9ec77a66-5395-4528-872b-6b93d92b6af9"), new DateTime(2020, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("9708bc3a-c7ab-4f87-a95d-30d45960118f"), new Guid("1e690e96-7880-4bf5-a8da-5f11b44d7f88"), "Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor", new Guid("f4fc19bf-7c61-47b6-8744-dab753ea9add"), new DateTime(2020, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("cc6c3cec-8c08-4a70-9df6-2802f63fe76f"), new Guid("a5357326-8d97-44e4-8a98-10bb34a61977"), "Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor Lorem, ipsum dolor", new Guid("f4fc19bf-7c61-47b6-8744-dab753ea9add"), new DateTime(2020, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_SexId",
                table: "Clients",
                column: "SexId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SexId",
                table: "Doctors",
                column: "SexId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_ClientId",
                table: "Visits",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_DoctorId",
                table: "Visits",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_VisitId",
                table: "Visits",
                column: "VisitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "VisitTypes");

            migrationBuilder.DropTable(
                name: "Sex");
        }
    }
}
