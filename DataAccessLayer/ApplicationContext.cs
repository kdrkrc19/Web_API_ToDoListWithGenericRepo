using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoList>()
                .HasOne(toDoList => toDoList.User)
                .WithMany()
                .HasForeignKey(toDoList => toDoList.UserId);
        }
    }
}
