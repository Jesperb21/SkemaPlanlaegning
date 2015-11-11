namespace Models
{
    public class Course
    {
        /// <summary>
        /// Course Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the course
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Duration of the course in weeks
        /// </summary>
        public int Duration { get; set; }
    }
}