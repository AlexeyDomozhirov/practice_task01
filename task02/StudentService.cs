namespace task02;

using System.Collections.Generic;
using System.Linq;

public class Student
{
    public string Name { get; set; } = string.Empty;
    public string Faculty { get; set; } = string.Empty;
    public List<int> Grades { get; set; } = new List<int>();
}

public class StudentService
{
    private readonly List<Student> _students;

    public StudentService(List<Student> students) => _students = students;

    public IEnumerable<Student> GetStudentsByFaculty(string faculty)
        => _students.Where(s => s.Faculty == faculty);

    public IEnumerable<Student> GetStudentsWithMinAverageGrade(double minAverageGrade)
        => _students.Where(s =>
            (s.Grades?.Count > 0 ? s.Grades.Average() : 0.0) >= minAverageGrade);

    public IEnumerable<Student> GetStudentsOrderedByName()
        => _students.OrderBy(s => s.Name);

    public ILookup<string, Student> GroupStudentsByFaculty()
        => _students.ToLookup(s => s.Faculty);

    public string GetFacultyWithHighestAverageGrade()
        => _students
            .GroupBy(s => s.Faculty)
            .MaxBy(g => g
                .SelectMany(s => s.Grades ?? Enumerable.Empty<int>())
                .DefaultIfEmpty()
                .Average())
            ?.Key ?? string.Empty;
}
