using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinal.Context;
using ProyectoFinal.Interfaces;
using ProyectoFinal.Models;
using ProyectoFinal.Services;
using System;
using System.Threading.Tasks;
using static ProyectoFinal.DTOs.TeacherDTO;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MooDleTest
{
    [TestClass]
    public class TeacherServiceTests
    {
        private DbContextOptions<EduAsyncHubContext> _options;
        private EduAsyncHubContext _context;

        [TestInitialize]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<EduAsyncHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new EduAsyncHubContext(_options);
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            // Seeding logic here, if necessary
        }

        [TestMethod]
        public async Task CreateSubject_ValidSubject_ShouldCreateSuccessfully()
        {
            // Arrange
            ITeacherService teacherService = new TeacherService(_context);
            var subjectDto = new CreateSubjectRequestDto { Nombre = "Mathematics" };

            // Act
            await teacherService.CreateSubject(subjectDto);

            // Assert
            var createdSubject = await _context.Materias.FirstOrDefaultAsync(s => s.NombreMateria == "Mathematics");
            Assert.IsNotNull(createdSubject);
            Assert.AreEqual("Mathematics", createdSubject.NombreMateria);
        }

        // Add more test methods for other operations...

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }
    }
}
