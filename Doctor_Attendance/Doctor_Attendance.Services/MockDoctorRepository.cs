using Doctor_Attendance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctor_Attendance.Services
{
    public class MockDoctorRepository : IDoctorRepository
    {

        private List<Doctor> _doctorList;

        public MockDoctorRepository()
        {
            _doctorList = new List<Doctor>()
            {
                new Doctor(){DoctorId = 1, Firstname = "doctor1", Lastname = "doc1", Age = 20},
                new Doctor(){DoctorId = 1, Firstname = "doctor2", Lastname = "doc2", Age = 30},
                new Doctor(){DoctorId = 1, Firstname = "doctor3", Lastname = "doc3", Age = 32}
            };
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _doctorList;
        }
    }
}
