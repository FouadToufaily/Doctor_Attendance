using Doctor_Attendance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctor_Attendance.Services
{
    public class SQLDoctorRepository : IDoctorRepository
    {

        private readonly AppDBContext _context;

        public SQLDoctorRepository(AppDBContext context)
        {
            this._context = context;
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _context.Doctors;
        }

        public Doctor GetDoctor(int id)
        {
            return _context.Doctors.Find(id);
        }

        public IEnumerable<Doctor> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return _context.Doctors;
            }
            return _context.Doctors.Where(e => e.Firstname.Contains(searchTerm) ||
                                            e.Lastname.Contains(searchTerm) ||
                                            e.City.Contains(searchTerm) ||
                                            e.Email.Contains(searchTerm) ||
                                            e.Category.Type.Contains(searchTerm)
                                            );
        }
    }
}
