using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISproj.Controllers;
using ISproj.Models;
using ISproj.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;


namespace ISproj.UnitTests
{
    [TestClass]
    public class TeacherControllerTests
    {
        [TestMethod]
        public void TestIndexAndFullName()
        {
            var teacherTestData = new List<Teacher>() {
                new Teacher { Id = "1", FirstName = "Ion", LastName = "Gheorghe", Email="a@google.com", Courses = new List<CourseModel>() },
                new Teacher { Id = "2", FirstName = "Ion1", LastName = "Gheorghe1", Email="a@google.com", Courses = new List<CourseModel>() },
                new Teacher { Id = "3", FirstName = "Ion2", LastName = "Gheorghe2", Email="a@google.com", Courses = new List<CourseModel>() },
                new Teacher { Id = "4", FirstName = "Ion3", LastName = "Gheorghe3", Email="a@google.com", Courses = new List<CourseModel>() },
            };
            var teachers = MockDbSet(teacherTestData);
            //Set up mocks for db sets
            var dbContext = new Mock<IDbContext>();
            dbContext.Setup(m => m.TeacherViewModel).Returns(teachers.Object);

            var teacherController = new TeacherController(dbContext.Object, null, null, null);

            //Act
            var results = teacherController.DoIndex();

            //Assert
            Assert.IsTrue(results.Count == 4);
            Assert.IsTrue(results.ToArray()[0].FullName == "Gheorghe Ion");
        }

        [TestMethod]
        public void TestDetails()
        {
            var teacherTestData = new List<Teacher>() {
                new Teacher { Id = "1", FirstName = "Ion", LastName = "Gheorghe", Email="a@google.com", Courses = new List<CourseModel>() },
                new Teacher { Id = "2", FirstName = "Ion1", LastName = "Gheorghe1", Email="a@google.com", Courses = new List<CourseModel>() },
                new Teacher { Id = "3", FirstName = "Ion2", LastName = "Gheorghe2", Email="a@google.com", Courses = new List<CourseModel>() },
                new Teacher { Id = "4", FirstName = "Ion3", LastName = "Gheorghe3", Email="a@google.com", Courses = new List<CourseModel>() },
            };
            var teachers = MockDbSet(teacherTestData);
            //Set up mocks for db sets
            var dbContext = new Mock<IDbContext>();
            dbContext.Setup(m => m.TeacherViewModel).Returns(teachers.Object);

            var teacherController = new TeacherController(dbContext.Object, null, null, null);

            //Act
            var teacher = teacherController.DoDetails("1");

            //Assert
            Assert.IsTrue(teacher != null);
            Assert.IsTrue(teacher.Id.CompareTo("1") == 0);
            Assert.IsTrue(teacher.FirstName.CompareTo("Ion") == 0);
            Assert.IsTrue(teacher.LastName.CompareTo("Gheorghe") == 0);
            Assert.IsTrue(teacher.Email.CompareTo("a@google.com") == 0);
        }

        Mock<DbSet<T>> MockDbSet<T>(IEnumerable<T> list) where T : class, new()
        {
            IQueryable<T> queryableList = list.AsQueryable();
            Mock<DbSet<T>> dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Provider).Returns(queryableList.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(queryableList.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(queryableList.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(() => queryableList.GetEnumerator());

            return dbSetMock;
        }
    }
}
