using Business.Create;
using Business.Query;
using Entities.DTO;
using Entities.Models;
using MediatR;
using Moq;
using NUnit.Framework;
using Repository.Interface;

namespace Test.Business.Polcy;

[TestFixture]
public class PolicyTest
{
    [Test]
    public async Task HandleValidCommandReturnsPolicyCustomers()
    {
        // Arrange
        var policyRepositoryMock = new Mock<IPolicyRepository>();
        var cancellationToken = CancellationToken.None;
        var command = new GetAllCommand();
        var expectedCustomers = new List<PolicyCustomerDto>
        {
            new PolicyCustomerDto { Id = "", Name = "Customer 1" },
            new PolicyCustomerDto { Id = "", Name = "Customer 2" }
        };
        policyRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedCustomers);
        var handler = new GetAllCommandHanble(policyRepositoryMock.Object);
        // Act
        var result = await handler.Handle(command, cancellationToken);
        // Assert
        Assert.AreEqual(expectedCustomers, result);
    }
    
    [Test]
    public async Task HandleValidCommandReturnsEmptyErrorsList()
    {
        // Arrange
        var customerRepositoryMock = new Mock<ICustomerRepository>();
        var policyRepositoryMock = new Mock<IPolicyRepository>();
        var vehicleRepositoryMock = new Mock<IVehicleRepository>();
        var cancellationToken = CancellationToken.None;
        var command = new CreatePolicyCommand(
            policyNumber: "123",
            idPlan: "plan1",
            plaque: "ABC123",
            vehicleModel: "Model1",
            whitInspection: true,
            StartDate: "2023-01-01",
            EndDate: "2023-12-31",
            IdCustomer: null,
            City: "City1",
            Address: "Address1",
            customerName: "Customer1",
            identification: "1234567890"
        );

        customerRepositoryMock.Setup(repo => repo.AddCustomerAsync(It.IsAny<Customers>())).Returns(Task.CompletedTask);
        vehicleRepositoryMock.Setup(repo => repo.Create(It.IsAny<Vehicles>())).Returns(Task.CompletedTask);
        policyRepositoryMock.Setup(repo => repo.Create(It.IsAny<Entities.Models.Policy>())).Returns(Task.CompletedTask);

        var handler = new CreatePolicyCommandHabler(
            customerRepositoryMock.Object,
            policyRepositoryMock.Object,
            vehicleRepositoryMock.Object
        );

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.IsEmpty(result);
    }

    [Test]
    public async Task HandleValidCommandReturnsErrorWhenCustomerHasActivePolicy()
    {
        // Arrange
        var customerRepositoryMock = new Mock<ICustomerRepository>();
        var policyRepositoryMock = new Mock<IPolicyRepository>();
        var vehicleRepositoryMock = new Mock<IVehicleRepository>();
        var cancellationToken = CancellationToken.None;
        var command = new CreatePolicyCommand(
            policyNumber: "123",
            idPlan: "plan1",
            plaque: "ABC123",
            vehicleModel: "Model1",
            whitInspection: true,
            StartDate: "2023-01-01",
            EndDate: "2023-12-31",
            IdCustomer: "64a99cbcbe6b26292c59c43f",
            City: "City1",
            Address: "Address1",
            customerName: "Customer1",
            identification: "1234567890"
        );

        var activePolicy= new Policy() {  policyNumber = "123", StartDate = "2022-01-01", EndDate = "2023-01-02" };
        policyRepositoryMock.Setup(repo => repo.GetPolicyCustomerAsync(command.IdCustomer)).ReturnsAsync(activePolicy);

        var handler = new CreatePolicyCommandHabler(
            customerRepositoryMock.Object,
            policyRepositoryMock.Object,
            vehicleRepositoryMock.Object
        );

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.IsNotEmpty(result);
        Assert.AreEqual("El cliente cuenta con una poliza activa", result[0]);
    }
    
    
}