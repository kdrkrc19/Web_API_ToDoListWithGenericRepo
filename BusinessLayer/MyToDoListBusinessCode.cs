using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class MyToDoListBusinessCode
    {
        private readonly ApplicationContext _context;
        private readonly GenericRepositories<ToDoList> _toDoListRepository;

        public MyToDoListBusinessCode(ApplicationContext context, GenericRepositories<ToDoList> toDoListRepository)
        {
            _context = context;
            _toDoListRepository = toDoListRepository;
        }

        public bool AddList(ToDoList toDoList)
        {
            _context.ToDoLists.Add(toDoList);
            _context.SaveChanges();

            return true;
        }

        public bool DeleteList(int listId, string userName)
        {
            var toDoList = GetToDoListByUserNameAndId(userName, listId);

            if (toDoList != null)
            {
                _context.ToDoLists.Remove(toDoList);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool UpdateList(ToDoList updatedList, int listId, string userName)
        {
            var toDoList = GetToDoListByUserNameAndId(userName, listId);

            if (toDoList != null)
            {
                toDoList.List = updatedList.List;
                toDoList.HasDone = updatedList.HasDone;
                toDoList.Description = updatedList.Description;

                _context.SaveChanges();
                return true;
            }

            return false;

        }

        public bool MarKDone(int listId, string userName)
        {
            var toDoList = GetToDoListByUserNameAndId(userName, listId);

            if (toDoList != null)
            {
                toDoList.HasDone = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public ToDoList GetList(int listId, string userName)
        {
            var toDoList = GetToDoListByUserNameAndId(userName, listId);
            return toDoList;
        }

        public ToDoList GetToDoListByUserNameAndId(string userName, int listId)
        {
            var toDoList = _context.ToDoLists
                .Where(t => t.Id == listId)
                .Join(
                    _context.Students,
                    toDo => toDo.UserId,
                    user => user.Id,
                    (toDo, user) => new { ToDo = toDo, User = user }
                )
                .FirstOrDefault(result => result.User.StudentName == userName)
                ?.ToDo;

            return toDoList;
        }
    }
}
