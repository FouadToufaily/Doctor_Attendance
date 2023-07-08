using Doctor_Attendance.Services;
using Microsoft.AspNetCore.Mvc;

namespace Doctor_Attendance.ViewComponents
{
    public class HeadCountViewComponent : ViewComponent
    {
        private readonly IDoctorRepository doctorRepository;

        public HeadCountViewComponent(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }

        public IViewComponentResult Invoke()
        {
            var result = doctorRepository.GetDeptHeadCount();
            return View(result);
        }
    }
}
