using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace Models
{
    /// <summary>
    /// a sprint type,
    /// ex: H5 for Datatechnicians with Specialty in Programming
    /// </summary>
    public class ClassTemplate
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Course> Courses { get; set; }
    }
}