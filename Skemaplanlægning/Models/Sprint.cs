using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// An Instance of a SprintType, 
    /// ex the DT-P-H5 that runs from now to tomorrow
    /// </summary>
    public class Sprint
    {
        [Key]
        public int Id { get; set; }
        public SprintType SprintType { get; set; }
        public List<Student> Students { get; set; }
    }
}