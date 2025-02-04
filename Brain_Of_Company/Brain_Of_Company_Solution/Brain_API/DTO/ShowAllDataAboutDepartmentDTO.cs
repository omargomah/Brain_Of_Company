namespace Brain_API.DTO
{
    public class ShowAllDataAboutDepartmentDTO: ShowManagerOfDepartmentDTO
    {
        public List<ShowShortDataAboutEmployeeDTO> WorkingEmployees { get; set; } 
    }
    

}
