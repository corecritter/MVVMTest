﻿using ASP_TDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_TDD.DAL
{
    public interface IStudentRepository : IDisposable
    {
        IEnumerable<Student> GetStudents();
        Student GetStudentByID(int studentId);
        void InsertStudent(Student student);
        void DeleteStudent(int studentID);
        void UpdateStudent(Student student);
        void Save();

    }
}