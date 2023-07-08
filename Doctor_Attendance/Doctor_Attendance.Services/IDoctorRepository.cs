using Doctor_Attendance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctor_Attendance.Services
{
    public interface IDoctorRepository
    {
        IEnumerable<Doctor> GetAllDoctors();

        Doctor getDoctor(int id);

        IEnumerable<DeptHeadCount> GetDeptHeadCount();
    }
}
