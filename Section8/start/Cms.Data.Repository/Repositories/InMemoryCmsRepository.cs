using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Data.Repository.Models;

namespace Cms.Data.Repository.Repositories
{
    public class InMemoryCmsRepository : ICmsRepository
    {
        List<Course> courses = null;
        List<Student> students = null;

        public InMemoryCmsRepository()
        {
            courses = new List<Course>();
            courses.Add(
                new Course()
                {
                    CourseId = 1,
                    CourseName = "Computer Science",
                    CourseDuration = 4,
                    CourseType = COURSE_TYPE.ENGINEERING
                }
            );
            courses.Add(
                new Course()
                {
                    CourseId = 2,
                    CourseName = "Information Technology",
                    CourseDuration = 4,
                    CourseType = COURSE_TYPE.ENGINEERING
                }
            );

            students = new List<Student>();
            students.Add(
                new Student()
                {
                    StudentId = 101,
                    FirstName = "James",
                    LastName = "Smith",
                    PhoneNumber = "555-555-1234",
                    Address = "US",
                    Course = courses.Where(c => c.CourseId == 1).SingleOrDefault()
                }
            );
            students.Add(
                new Student()
                {
                    StudentId = 102,
                    FirstName = "Robert",
                    LastName = "Smith",
                    PhoneNumber = "555-555-5678",
                    Address = "US",
                    Course = courses.Where(c => c.CourseId == 1).SingleOrDefault()
                }
            );

        }

        public IEnumerable<Course> GetAllCourses()
        {
            return courses;
        }

        public Course AddCourse(Course newCourse)
        {
            var maxCourseId = courses.Max(c => c.CourseId);
            newCourse.CourseId = maxCourseId + 1;
            courses.Add(newCourse);

            return newCourse;
        }

        public bool IsCourseExists(int courseId)
        {
            return courses.Any(c => c.CourseId == courseId);
        }

        public Course GetCourse(int courseId)
        {
            var result = courses.Where(c => c.CourseId == courseId)
                                .SingleOrDefault();
            return result;
        }

        public Course UpdateCourse(int courseId, Course updatedCourse)
        {
            var course = courses.Where(c => c.CourseId == courseId)
                                .SingleOrDefault();

            if (course != null)
            {
                course.CourseName = updatedCourse.CourseName;
                course.CourseDuration = updatedCourse.CourseDuration;
                course.CourseType = updatedCourse.CourseType;
            }

            return course;
        }

        public Course DeleteCourse(int courseId)
        {
            var course = courses.Where(c => c.CourseId == courseId)
                                .SingleOrDefault();

            if (course != null)
            {
                courses.Remove(course);
            }

            return course;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await Task.Run(() => courses.ToList());
        }

        public IEnumerable<Student> GetStudents(int courseId)
        {
            return students.Where(s => s.Course.CourseId == courseId);
        }

        public Student AddStudent(Student newStudent)
        {
            var maxStudentId = students.Max(c => c.StudentId);
            newStudent.StudentId = maxStudentId + 1;
            students.Add(newStudent);

            return newStudent;
        }
    }
}