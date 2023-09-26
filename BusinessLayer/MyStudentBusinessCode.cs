using DataAccessLayer;

namespace BusinessLayer
{
    public class MyStudentBusinessCode
    {
        private readonly GenericRepositories<Student> _studentRepository;
        private readonly ApplicationContext _context;
        public MyStudentBusinessCode(ApplicationContext context, GenericRepositories<Student> studentRepository)
        {
            _context = context;
            _studentRepository = studentRepository;
        }
        public bool AddStudent(Student student)
        {
            var isStudentExist = _studentRepository.Find(u => u.StudentName == student.StudentName).FirstOrDefault();

            if (isStudentExist != null) return false;

            _studentRepository.Add(student);
            _studentRepository.Save();

            return true;
        }

        public bool DeleteStudent(string studentName)
        {
            var isStudent = _studentRepository.Find(u => u.StudentName == studentName).FirstOrDefault();

            if (isStudent != null)
            {
                _studentRepository.Delete(isStudent.Id);
                _studentRepository.Save();
                return true;
            }

            return false;
        }

        public bool UpdateStudent(Student updatedStudent, string studentName)
        {
            var student = _studentRepository.Find(u => u.StudentName == studentName).FirstOrDefault();

            if (student != null)
            {
                student.StudentName = updatedStudent.StudentName;
                student.StudentSurname = updatedStudent.StudentSurname;

                _studentRepository.Update(student);
                _studentRepository.Save();
                return true;
            }

            return false;
        }

        public Student GetStudent(string studentName)
        {
            return _studentRepository.Find(u => u.StudentName == studentName).FirstOrDefault();
        }
    }
}