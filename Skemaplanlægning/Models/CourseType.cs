using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// CourseTypes, ex: Programmering 4
    /// </summary>
    public class CourseType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
