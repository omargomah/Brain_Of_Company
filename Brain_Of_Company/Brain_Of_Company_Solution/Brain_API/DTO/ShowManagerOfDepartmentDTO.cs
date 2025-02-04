namespace Brain_API.DTO
{
    public class ShowManagerOfDepartmentDTO: ShowShortDataAboutDepartmentDTO
    {
        public ShowShortDataAboutEmployeeDTO ManagedBy { get; set; } 
    }
    

}
