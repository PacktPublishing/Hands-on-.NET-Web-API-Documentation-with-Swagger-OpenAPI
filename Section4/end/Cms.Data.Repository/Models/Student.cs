namespace Cms.Data.Repository.Models
{
    public class Student
    {
        public int StudentId { get; set; }  
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public Course Course { get; set; }
    }
}