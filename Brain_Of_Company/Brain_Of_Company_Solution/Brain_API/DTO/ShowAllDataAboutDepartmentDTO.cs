namespace Brain_API.DTO
{
    public class ShowAllDataAboutDepartmentDTO: ShowManagerOfDepartmentDTO
    {
        public List<ShowShortDataAboutEmployeeDTO> WorkingEmployees { get; set; } 
    }
    public class ShowManagerOfDepartmentDTO: ShowShortDataAboutDepartmentDTO
    {
        public ShowShortDataAboutEmployeeDTO ManagedBy { get; set; } 
    }
    

}
