using System.ComponentModel.DataAnnotations;

namespace LoclizationWebApi.DTO
{
    public class TestDTO
    {
       
        [Required(ErrorMessage ="required")]
        public string Name { get; set; }
    }
}
