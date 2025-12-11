using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Services
{
    public class DBManager : DbContext
    {
        public DbSet<Article> Articles { get; set; }

        // erzeuge die OnConfiguring-Methode für mysql mit dem connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=webapi",
                new MySqlServerVersion(new Version(8, 0, 26)));
        }


    }
}
