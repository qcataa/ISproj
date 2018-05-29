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
    public class StudentControllerTests
    {
        [TestMethod]
        public void TestIndexAndFullName()
        {
            var studentTestData = new List<Student>() {
                new Student { id = 1, Name = "Andrei", Surname = "Andrei",    CNP="0000000000001",Email="a@google.com" },
                new Student { id = 2, Name = "Ion1",   Surname = "Gheorghe1", CNP="0000000000002",Email="a@google.com" },
            };
            var students = MockDbSet(studentTestData);
            //Set up mocks for db sets
            var dbContext = new Mock<IDbContext>();
            dbContext.Setup(m => m.StudentViewModel).Returns(students.Object);

            var studentsController = new StudentController(dbContext.Object, null, null, null, null);

            //Act
            var results = studentsController.DoIndex();

            //Assert
            Assert.IsTrue(results.Count == 2);
            Assert.IsTrue(results.ToArray()[0].FullName == "Andrei Andrei");
        }

        [TestMethod]
        public void TestDetails()
        {
            var teacherTestData = new List<Student>() {
                new Student { id = 1, Name = "Andrei", Surname = "Andrei",    CNP="0000000000001",Email="a@google.com" },
                new Student { id = 2, Name = "Ion1",   Surname = "Gheorghe1", CNP="0000000000002",Email="a@google.com" },
            };
            var students = MockDbSet(teacherTestData);
            //Set up mocks for db sets
            var dbContext = new Mock<IDbContext>();
            dbContext.Setup(m => m.StudentViewModel).Returns(students.Object);

            var studentsController = new StudentController(dbContext.Object, null, null, null, null);

            //Act
            var student = studentsController.DoDetails(1);

            //Assert
            Assert.IsTrue(student != null);
            Assert.IsTrue(student.id == 1);
            Assert.IsTrue(student.Name.CompareTo("Andrei") == 0);
            Assert.IsTrue(student.Surname.CompareTo("Andrei") == 0);
            Assert.IsTrue(student.Email.CompareTo("a@google.com") == 0);
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
