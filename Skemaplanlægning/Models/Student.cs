namespace Models
{
    /// <summary>
    /// a basic POCO of a student
    /// ex: Jesper baunsgaard
    /// </summary>
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}