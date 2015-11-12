using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// a basic POCO of a student
    /// ex: Jesper baunsgaard
    /// </summary>
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}