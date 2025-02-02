using Brain_Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    public class ShowAllDataDependentDTO:ShowShortDataDependentDTO
    {
        public List<ShowShortDataAboutEmployeeDTO> Employee { get; set; }
    }



}
