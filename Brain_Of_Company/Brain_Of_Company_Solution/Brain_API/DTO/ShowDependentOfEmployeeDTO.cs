using Brain_Entities.Models;

namespace Brain_API.DTO
{
    public class ShowDependentOfEmployeeDTO 
    {
        public string EmployeeSSN { get; set; }
        public string EmployeeName { get; set; }

        public List<ShowShortDataDependentDTO> dataDependentDTOs{ get; set; }
    }
    

}
