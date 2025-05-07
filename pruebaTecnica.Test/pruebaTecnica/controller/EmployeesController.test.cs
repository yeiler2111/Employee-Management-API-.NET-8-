using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using pruebaTecnica.controllers;
using pruebaTecnica.services;
using pruebaTecnica.models;

namespace pruebaTecnica.Test
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly EmployeesController _controller;

        public EmployeeControllerTests()
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
            _controller = new EmployeesController(_mockEmployeeService.Object);
        }

        //test crate employee
        [Fact]
        public async Task CreateEmployee_ReturnsCreatedAtActionResult()
        {
            var employee = new Employee
            {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "123-456-7890",
                HireDate = DateTime.Now
            };

            _mockEmployeeService
                .Setup(service => service.CreateEmployeeAsync(It.IsAny<Employee>()))
                .ReturnsAsync(employee);


            var result = await _controller.CreateEmployee(employee);

            var actionResult = Assert.IsType<ActionResult<Employee>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);  
            var returnValue = Assert.IsType<Employee>(createdAtActionResult.Value);
            Assert.Equal(employee.EmployeeId, returnValue.EmployeeId);
            Assert.Equal(employee.FirstName, returnValue.FirstName);
            Assert.Equal(employee.LastName, returnValue.LastName);
            Assert.Equal(employee.Email, returnValue.Email);
            Assert.Equal(employee.Phone, returnValue.Phone);
            Assert.Equal(employee.HireDate, returnValue.HireDate);
        }
        [Fact]
        public async Task CreateEmployee_ReturnsBadRequest_WhenModelIsInvalid()
        {
            var employee = new Employee
            {
                EmployeeId = 0,
                FirstName = "asd",
                LastName = "dfgd",
                Email = "a@gmail.com",
                Phone = "1231231",
                HireDate = DateTime.Now
            };

            _controller.ModelState.AddModelError("FirstName", "El primer nombre es obligatorio");

            var result = await _controller.CreateEmployee(employee);
            var actionResult = Assert.IsType<ActionResult<Employee>>(result); 
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);  
            Assert.IsType<SerializableError>(badRequestResult.Value);  
        }


        //test getAllEmployees
        [Fact]
        public async Task GetAllEmployees_ReturnsOkResult_WithListOfEmployees()
        {
            var employees = new List<Employee>
            {
            new Employee { EmployeeId = 1, FirstName = "John", LastName = "Doe" },
            new Employee { EmployeeId = 2, FirstName = "Jane", LastName = "Smith" }
            };

            _mockEmployeeService.Setup(service => service.GetAllEmployeesAsync())
                                .ReturnsAsync(employees);

            var result = await _controller.GetAllEmployees();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Employee>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }


    }
}
