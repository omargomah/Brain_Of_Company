using System.ComponentModel.DataAnnotations.Schema;

namespace Brain_API.DTO
{
    public class ShowShortDataAboutDepartmentDTO
    {
        public int Id { get; set; }
        public string ManagerSSN { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int MinimumDaysToAttendancePerMonth { get; set; }

    }
}
