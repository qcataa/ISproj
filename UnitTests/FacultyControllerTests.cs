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
    public class FacultyControllerTests
    {
        [TestMethod]
        public void TestIndex()
        {
            var facultyTestData = new List<Faculty>() {
                new Faculty { Id = 1, Name = "ACE",  Address = "Str. Doi, nr.2, jud. Dolj, Craiova" },
                new Faculty { Id = 2, Name = "Drept", Address = "Str. Unu, nr.1, jud. Dolj, Craiova"},
                new Faculty { Id = 3, Name = "Mecanica", Address = "Str. Trei, nr.3, jud. Dolj, Craiova"},
                new Faculty { Id = 4, Name = "ASE", Address = "Str. Patru, nr.4, jud. Dolj, Craiova"},
            };
            var faculties = MockDbSet(facultyTestData);
            //Set up mocks for db sets
            var dbContext = new Mock<IDbContext>();
            dbContext.Setup(m => m.Faculty).Returns(faculties.Object);

            var facultyController = new FacultyController(dbContext.Object);

            //Act
            var results = facultyController.DoIndex();

            //Assert
            Assert.IsTrue(results.Count == 4);
            Assert.IsTrue(results.ToArray()[0].Name == "ACE");
        }

        [TestMethod]
        public void TestDetails()
        {
            var facultyTestData = new List<Faculty>() {
                new Faculty { Id = 1, Name = "ACE",  Address = "Str. Doi, nr.2, jud. Dolj, Craiova" },
                new Faculty { Id = 2, Name = "Drept", Address = "Str. Unu, nr.1, jud. Dolj, Craiova"},
                new Faculty { Id = 3, Name = "Mecanica", Address = "Str. Trei, nr.3, jud. Dolj, Craiova"},
                new Faculty { Id = 4, Name = "ASE", Address = "Str. Patru, nr.4, jud. Dolj, Craiova"},
            };
            var faculties = MockDbSet(facultyTestData);
            //Set up mocks for db sets
            var dbContext = new Mock<IDbContext>();
            dbContext.Setup(m => m.Faculty).Returns(faculties.Object);

            var facultyController = new FacultyController(dbContext.Object);

            //Act
            var faculty = facultyController.DoDetails(1);

            //Assert
            Assert.IsTrue(faculty != null);
            Assert.IsTrue(faculty.Id == 1);
            Assert.IsTrue(faculty.Name.CompareTo("ACE") == 0);
            Assert.IsTrue(faculty.Address.CompareTo("Str. Doi, nr.2, jud. Dolj, Craiova") == 0);
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
