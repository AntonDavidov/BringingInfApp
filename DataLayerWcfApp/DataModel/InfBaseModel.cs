namespace DataLayerWcfApp.DataModel
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using System.IO;

    public partial class InfBaseModel : IdentityDbContext<IdentityUser>
    {
        static InfBaseModel()
        {
            Database.SetInitializer(new InfBaseModelInitializer());
        }
        public InfBaseModel()
            : base("name=InfBaseModel")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.LeadProjects)
                .WithOptional(p => p.Leader)
                .HasForeignKey(p => p.LeaderId);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

    }
    
    public class InfBaseModelInitializer: CreateDatabaseIfNotExists<InfBaseModel>
    {
        protected override void Seed(InfBaseModel db)
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee() {Name="Алексей" ,Surname="Бакиров", MiddleName="Артёмович", EMail="BakAlAr@mailru"},
                new Employee() {Name="Ростислав" ,Surname="Магер", MiddleName="Фёдорович", EMail="RosMaF@yandex.ru"},
                new Employee() {Name="Владимир" ,Surname="Фёдоров", MiddleName="Станиславович", EMail="VlaFeS@mail.ru"},
                new Employee() { Name="Руслан" ,Surname="Журавченко", MiddleName="Олегович", EMail="RuZhuO@gmail.com"}
            };
            List<Project> projects = new List<Project>()
            {
                new Project()
                {
                    ProjName = "Программа для синхронизации данных",
                    OrgOrderName = "Мария-Ра",
                    OrgExecuteName = "IT-Сервис",
                    DateProjExecuteBegin = new DateTime(2014, 06, 09),
                    DateProjExecuteEnd = new DateTime(2014, 09, 15),
                    Priority=4, Comment="Выполнено."
                },
                new Project()
                {
                    ProjName = "Приложение для работы с гос. бюро",
                    OrgOrderName = "Гос. Бюро",
                    OrgExecuteName = "IT-Сервис",
                    DateProjExecuteBegin = new DateTime(2012, 11, 06),
                    DateProjExecuteEnd = new DateTime(2013, 03, 19),
                    Priority=3, Comment="Выполнено."
                },
                new Project()
                {
                    ProjName = "Адаптер к крипто-бирже",
                    OrgOrderName = "OKEX",
                    OrgExecuteName = "IT-Developers",
                    DateProjExecuteBegin = new DateTime(2016, 04, 02),
                    DateProjExecuteEnd = new DateTime(2017, 08, 07),
                    Priority=1, Comment="Выполнено."
                },

                new Project()
                {
                    ProjName = "Парсинг видео потока",
                    OrgOrderName = "ИП Андреев",
                    OrgExecuteName = "IT-Developers",
                    DateProjExecuteBegin = new DateTime(2018, 07, 20),
                    Priority=21 , Comment="В работе."
                }
            };


            db.Projects.AddRange(projects);
            db.Employees.AddRange(employees);

            db.Projects.Local[0].Employees = employees.Take(3).ToList();
            db.Projects.Local[0].Leader = employees.First();

            db.Projects.Local[1].Employees = employees.Take(2).ToList();
            db.Projects.Local[1].Leader = employees.First();

            db.Projects.Local[2].Employees = employees.Skip(2).ToList();
            db.Projects.Local[2].Leader = employees.Last();

            db.Projects.Local[3].Employees = employees.Skip(1).ToList();
            db.Projects.Local[3].Leader = employees.Last();

            db.SaveChanges();

            
			if (CreateRole(db, "Admin").Succeeded)
				if (CreateUser(db, "Admin", "123").Succeeded)
					AddUserToRole(db, db.Users.First(u => u.UserName == "Admin").Id, "Admin");

			CreateRole(db, "User");
        }

        IdentityResult CreateUser(InfBaseModel db, string login, string password)
        {
			IdentityUser user = new IdentityUser()
			{
				UserName = login,
				PasswordHash = password
            };
            UserStore<IdentityUser> userStore = new UserStore<IdentityUser>(db);
            UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(userStore);
			return userManager.Create(user);
        }
		IdentityResult CreateRole(InfBaseModel db, string roleName)
		{
			IdentityRole role = new IdentityRole()
			{
				Name = roleName
			};
			RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(db);
			RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);
			return roleManager.Create(role);
		}
		IdentityResult AddUserToRole(InfBaseModel db, string userId, string roleName)
		{
			UserStore<IdentityUser> userStore = new UserStore<IdentityUser>(db);
			UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(userStore);
			return userManager.AddToRole(userId, roleName);
		}
	}
}
