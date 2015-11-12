using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// a sprint type,
    /// ex: H5 for Datatechnicians with Specialty in Programming
    /// </summary>
    public class SprintType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } 
    }
}