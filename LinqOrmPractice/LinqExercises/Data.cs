using System.Collections.Generic;

namespace LinqExercises
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int CourseId { get; set; }
    }

    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public static class Data
    {
        public static List<Student> GetStudents()
        {
            return new List<Student>
            {
                new Student { Id = 1, Name = "Alice", Age = 20, CourseId = 1 },
                new Student { Id = 2, Name = "Bob", Age = 22, CourseId = 2 },
                new Student { Id = 3, Name = "Charlie", Age = 21, CourseId = 1 },
                new Student { Id = 4, Name = "David", Age = 23, CourseId = 3 },
                new Student { Id = 5, Name = "Eve", Age = 20, CourseId = 2 },
                new Student { Id = 6, Name = "Frank", Age = 24, CourseId = 1 },
                new Student { Id = 7, Name = "Grace", Age = 22, CourseId = 3 }
            };
        }

        public static List<Course> GetCourses()
        {
            return new List<Course>
            {
                new Course { Id = 1, Title = "Computer Science" },
                new Course { Id = 2, Title = "Mathematics" },
                new Course { Id = 3, Title = "Physics" }
            };
        }
    }
}

