using BusinessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly ApplicationContext _context;
    private readonly GenericRepositories<Student> _studentRepository;
    public StudentsController(ApplicationContext context, GenericRepositories<Student> studentRepository)
    {
        _context = context;
        _studentRepository = studentRepository;
    }

    [HttpPost("add-user")]
    public IActionResult AddUser(Student user)
    {
        MyStudentBusinessCode userBusinessCode = new MyStudentBusinessCode(_context,_studentRepository);

        bool isAddded = userBusinessCode.AddStudent(user);

        if (isAddded) return Ok(user);
        else return BadRequest();
    }

    [HttpDelete("delete-user/{userName}")]
    public IActionResult DeleteUser(string userName)
    {
        MyStudentBusinessCode userBusinessCode = new MyStudentBusinessCode(_context, _studentRepository);

        bool isDeleted = userBusinessCode.DeleteStudent(userName);

        if (isDeleted) return Ok($"Kullanıcı Adı {userName} Olan Kullanıcı Silindi");
        else return NotFound($"Kullanıcı Adı {userName} Olan Bir Kullanıcı Bulunamadı.");
    }

    [HttpPut("update-user/{userName}")]
    public IActionResult UpdateUser(Student updatedUser, string userName)
    {
        MyStudentBusinessCode userBusinessCode = new MyStudentBusinessCode(_context, _studentRepository);

        bool isUpdated = userBusinessCode.UpdateStudent(updatedUser, userName);

        if (isUpdated) return Ok(updatedUser);
        else return NotFound($"Kullanıcı Adı {updatedUser.StudentName} Olan Bir Kullanıcı Bulunamadı.");
    }

    [HttpGet("get-user/{userName}")]
    public IActionResult GetUser(string userName)
    {
        MyStudentBusinessCode userBusinessCode = new MyStudentBusinessCode(_context, _studentRepository);

        Student user = userBusinessCode.GetStudent(userName);

        if (user != null) return Ok(user);
        else return NotFound($"Kullanıcı Adı {userName} Olan Bir Kullanıcı Bulunamadı.");

    }
}
