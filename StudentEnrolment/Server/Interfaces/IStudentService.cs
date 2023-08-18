using StudentEnrolment.Shared;
using StudentEnrolment.Shared.ViewModels;

namespace StudentEnrolment.Server.Interfaces
{
    public interface IStudentService
    {
        List<Student> GetStudents();

        List<Student> SearchStudentsByName(string id);

        List<Student> SearchStudentsById(int id);

        AddStudentViewModel GetStudentById(int id);

        UpdateStudentViewModel GetStudentForUpdate(int id);

        void AddStudent(AddStudentViewModel student);

        void UpdateStudent(UpdateStudentViewModel student, int id);

        void DeleteStudent(int id);
    }
}
