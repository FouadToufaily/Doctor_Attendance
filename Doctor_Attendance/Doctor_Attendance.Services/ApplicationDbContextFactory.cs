using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctor_Attendance.Services
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<AppDBContext>
{
        public AppDBContext CreateDbContext(string[] args)
        {
            string serverName = Environment.MachineName;
            var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
            optionsBuilder.UseSqlServer($"Data Source={serverName}\\SQLEXPRESS;Initial Catalog=Doctor_Attendance;Integrated Security=True; MultipleActiveResultSets=true");

            return new AppDBContext(optionsBuilder.Options);
        }
}
}
