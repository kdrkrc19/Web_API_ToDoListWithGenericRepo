using BusinessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetEFCoreWithSwagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly GenericRepositories<Student> _studentRepository;
        private readonly GenericRepositories<ToDoList> _toDoListRepository;
        public ToDoListController(ApplicationContext context, GenericRepositories<Student> studentRepository, GenericRepositories<ToDoList> toDoListRepository)
        {
            _context = context;
            _studentRepository = studentRepository;
            _toDoListRepository = toDoListRepository;
        }

        [HttpPost("add-list/{userName}")]
        public IActionResult AddList(ToDoList toDoList, string userName)
        {
            MyToDoListBusinessCode toDoListBusinessCode = new MyToDoListBusinessCode(_context,_toDoListRepository);
            MyStudentBusinessCode userBusinessCode = new MyStudentBusinessCode(_context, _studentRepository);
            Student user = userBusinessCode.GetStudent(userName);

            if (user == null) return NotFound($"Kullanıcı Adı {userName} ile eşleşen kullanıcı bulunamadı.");

            toDoList.UserId = user.Id;

            toDoList.User = user;

            bool isAdded = toDoListBusinessCode.AddList(toDoList);

            if (isAdded) return Ok(toDoList);
            else return BadRequest("Ekleme İşlemi Başarısız Oldu.");
        }


        [HttpDelete("delete-list/{listId}")]
        public IActionResult DeleteList(int listId, string userName)
        {
            MyToDoListBusinessCode toDoListBusinessCode = new MyToDoListBusinessCode(_context, _toDoListRepository);

            bool isDeleted = toDoListBusinessCode.DeleteList(listId, userName);

            if (isDeleted) return Ok($"Liste ID {listId} Olan Liste Silindi");
            else return NotFound($"Liste ID {listId} Olan Bir Liste Bulunamadı.");
        }

        [HttpPut("update-list/{listId}")]
        public IActionResult UpdateList(ToDoList updatedList, int listId, string userName)
        {
            MyToDoListBusinessCode toDoListBusinessCode = new MyToDoListBusinessCode(_context, _toDoListRepository);

            bool isUpdated = toDoListBusinessCode.UpdateList(updatedList, listId, userName);

            if (isUpdated) return Ok(updatedList);
            else return NotFound($"Liste ID {listId} Olan Bir Liste Bulunamadı.");
        }

        [HttpGet("get-list/{listId}")]
        public IActionResult GetList(int listId, string userName)
        {
            MyToDoListBusinessCode toDoListBusinessCode = new MyToDoListBusinessCode(_context, _toDoListRepository);

            ToDoList toDoList = toDoListBusinessCode.GetList(listId, userName);

            if (toDoList != null) return Ok(toDoList);
            else return NotFound($"Liste ID {listId} Olan Bir Liste Bulunamadı.");
        }

        [HttpGet("mark-done/{listId}")]
        public IActionResult MarkDone(int listId, string userName)
        {
            MyToDoListBusinessCode toDoListBusinessCode = new MyToDoListBusinessCode(_context, _toDoListRepository);

            bool isMarked = toDoListBusinessCode.MarKDone(listId, userName);

            if (isMarked != null) return Ok(isMarked);
            else return NotFound($"Liste ID {listId} Olan Bir Liste Bulunamadı.");
        }
    }
}
