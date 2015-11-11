using System;
using System.Collections.ObjectModel;

namespace Models
{
    public class Sprint
    {
        public int Id { get; set; }
        public SprintType SprintType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}