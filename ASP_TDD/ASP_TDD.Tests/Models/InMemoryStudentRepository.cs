using ASP_TDD.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP_TDD.Models;

namespace ASP_TDD.Tests.Models
{
    public class InMemoryStudentRepository : IStudentRepository
    {
        private List<Student> _db = new List<Student>();

        public Exception ExceptionToThrow { get; set; }
        public Student GetStudentByID(int studentId)
        {
            return _db.FirstOrDefault(s => s.PersonId == studentId);
        }
        public IEnumerable<Student> GetStudents()
        {
            return _db.ToList();
        }
        public void InsertStudent(Student student)
        {
            if (ExceptionToThrow != null)
            {
                throw ExceptionToThrow;
            }
            _db.Add(student);
        }
        public void UpdateStudent(Student student)
        {
            foreach (var person in _db)
            {
                if(person.PersonId == student.PersonId)
                {
                    _db.Remove(student);
                    _db.Add(student);
                    return;
                }
            }
        }
        public void DeleteStudent(int studentID)
        {
            _db.Remove(GetStudentByID(studentID));
        }
        public void Save()
        {
            return;
        }
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
