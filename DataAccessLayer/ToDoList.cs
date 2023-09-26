using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ToDoList
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string List { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        //[JsonIgnore]
        public Student User { get; set; }
        public bool HasDone { get; set; }
        public string? Description { get; set; }
    }
}
