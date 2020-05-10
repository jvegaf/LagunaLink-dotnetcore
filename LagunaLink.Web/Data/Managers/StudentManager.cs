namespace LagunaLink.Web.Data.Managers
{
    using Entities;
    using System.Linq;
    using System.Threading.Tasks;

    public class StudentManager : IStudentManager
    {
        private readonly DataContext context;

        public StudentManager(DataContext context)
        {
            this.context = context;
        }

        public void AddStudent(Student student)
        {
            this.context.Students.Add(student);
        }

        public Student GetStudentById(int id)
        {
            return this.context.Students.Find(id);
        }

        public Student GetStudentByUserId(string userId)
        {
            return this.context.Students.Where(s => s.UserId == userId).FirstOrDefault();
        }

        public void UpdateStudent(Student student)
        {
            this.context.Students.Update(student);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }
    }
}
