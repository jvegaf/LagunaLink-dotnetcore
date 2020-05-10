namespace LagunaLink.Web.Data.Managers
{
    using System.Threading.Tasks;
    using Entities;

    public interface IStudentManager
    {
        Student GetStudentById(int id);
        
        Student GetStudentByUserId(string userId);
        
        void AddStudent(Student student);
        
        void UpdateStudent(Student student);
        
        Task<bool> SaveAllAsync();
        
    }
}