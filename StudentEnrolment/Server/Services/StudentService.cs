using Microsoft.EntityFrameworkCore;
using StudentEnrolment.Server.Interfaces;
using StudentEnrolment.Shared;
using StudentEnrolment.Shared.Data;
using StudentEnrolment.Shared.ViewModels;

namespace StudentEnrolment.Server.Services
{
    public class StudentService : IStudentService
    {
        EnrolmentContext _context;
        public StudentService(EnrolmentContext context)
        {
            _context = context;
        }

        public List<Student> GetStudents()
        {
            return _context.Students.ToList();
        }

        public List<Student> SearchStudentsByName(string id)
        {
            return _context.Students.Where(S => S.StudentName.Contains(id)).ToList();
        }

        public List<Student> SearchStudentsById(int id)
        {
            return _context.Students.Where(S => S.StudentId == (id)).ToList();
        }

        public AddStudentViewModel GetStudentById(int id)
        {
            var targetStudent = _context.Students.SingleOrDefault(S => S.StudentId == id);
            return new AddStudentViewModel()
            {
                StudentName = targetStudent.StudentName,
                DateOfBirth = targetStudent.DateOfBirth,
                CourseName = targetStudent.CourseName,
                EndDate = targetStudent.EndDate,
                StartDate = targetStudent.StartDate,
                WelshLanguageProficiency = targetStudent.WelshLanguageProficiency
            };
        }

        public UpdateStudentViewModel GetStudentForUpdate(int id)
        {
            var targetStudent = _context.Students.SingleOrDefault(S => S.StudentId == id);
            return new UpdateStudentViewModel()
            {
                StudentName = targetStudent.StudentName,
                WelshLanguageProficiency = targetStudent.WelshLanguageProficiency
            };
        }

        public void AddStudent(AddStudentViewModel student)
        {
            _context.Students.Add(new Student
            {
                StudentName = student.StudentName,
                DateOfBirth = student.DateOfBirth,
                CourseName = student.CourseName,
                EndDate = student.EndDate,
                StartDate = student.StartDate,
                WelshLanguageProficiency = student.WelshLanguageProficiency
            });
            _context.SaveChanges();
        }

        public void UpdateStudent(UpdateStudentViewModel student, int id)
        {
            var targetStudent = _context.Students.FirstOrDefault(S => S.StudentId == id);
            if (targetStudent != null)
            {
                targetStudent.StudentName = student.StudentName;
                targetStudent.WelshLanguageProficiency = student.WelshLanguageProficiency;
                _context.Students.Update(targetStudent);
                _context.SaveChanges();
            }
        }

        public void DeleteStudent(int id)
        {
            var student = _context.Students.FirstOrDefault(S => S.StudentId == id);
            _context.Students.Remove(student);
            _context.SaveChanges();
        }
    }
}
