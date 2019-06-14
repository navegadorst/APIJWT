using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLogin.Model
{
    public class LoginContext:DbContext
    {
        public DbSet<Login> Access { get; set; }
        public DbSet<Control> AccessControl { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlite($"Data Source={_hostingEnvironment.ContentRootPath}/APIAccess.db");
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBAPI;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"); 
        }
    }
    public class Login
    {
        public int Id { get; set; }
        public string Usser { get; set; }
        public int Pass { get; set; }
        public bool Stok { get; set; }
    }
    public class Control
    {
        public string USSER { get; set; }
        public int COUNTACCESS { get; set; }

    }
}
