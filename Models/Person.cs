using System.ComponentModel.DataAnnotations;


namespace mvcApp.Models
{
    public class Person
    {
        public int? Id { get; set; }
        [Required]

        public string Name { get; set; }  
        public string? Title { get; set; }
        public string? Email { get; set; }
        public  string? Adress {  get; set; }
        // public int Age {  get; set; }   
    }
}
