using AutoMapper;
using EventBus.Base.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Report.API.Controllers.v1;
using TelephoneBook.Report.API.MessageBrokerIntegrationEvents.Events;
using TelephoneBook.Report.Business.Interfaces;
using TelephoneBook.Report.Business.Mapping;
using TelephoneBook.Report.Entities.Models;
using TelephoneBook.Report.Entities.ViewModels;
using TelephoneBook.Report.UnitTest.MockDatas;
using TelephoneBook.Shared.Models;
using Xunit;
using static TelephoneBook.Shared.Models.Response;

namespace TelephoneBook.Report.UnitTest.UnitTest
{
    public class ReportsControllerTest
    {
        private readonly Mock<IReportRepository> _reportRepositoryMock;
        private readonly Mock<IReportDetailsRepository> _reportDetailsRepositoryMock;
        private readonly IMapper _mapper;
        private readonly Mock<IEventBus> _serviceBusMock;
        private readonly ReportsController _reportController;

        public ReportsControllerTest()
        {
            _reportRepositoryMock = new Mock<IReportRepository>();
            _reportDetailsRepositoryMock = new Mock<IReportDetailsRepository>();
            _serviceBusMock = new Mock<IEventBus>();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<ReportMapping>()).CreateMapper();
            _reportController = new ReportsController(_serviceBusMock.Object,_reportRepositoryMock.Object, _mapper, _reportDetailsRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllReports_TEST_200()
        {
            //Arrange
            _reportRepositoryMock.Setup(x => x.GetAllReportsAsync())
              .Returns(Task.FromResult(FakeDatas.GetReportsFake()));

            //Act
            var actionResult = await _reportController.GetAllReports();

            //Assert
            var objectResult = (ObjectResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.IsType<ResponseDatas<IList<ReportIndexViewModel>>>(objectResult.Value);
        }


        [Fact]
        public async Task ReportCreated_TEST_201()
        {
            //Arrange
            var fakeReport = FakeDatas.GetReportById(FakeDatas.fakeReportId);

            _reportRepositoryMock.Setup(x => x.CreateReportAsync())
              .Returns(Task.FromResult(fakeReport));

            _serviceBusMock.Setup(x => x.Publish(It.IsAny<ReportStartingEvent>()));

            //Act
            var actionResult = await _reportController.CreateReport();

            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (ResponseDatas<ResultId<string>>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.Created);
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.Id, FakeDatas.fakeReportId);
        }

        [Fact]
        public async Task CreateReport_TEST_400()
        {
            //Arrange
            _reportRepositoryMock.Setup(x => x.CreateReportAsync())
              .Returns(Task.FromResult((Reports?)null));

            //Act
            var actionResult = await _reportController.CreateReport();

            //Assert
            var objectResult = (BadRequestResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetReportById_TEST_200()
        {
            //Arrange

            _reportRepositoryMock.Setup(x => x.GetReportByIdAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(FakeDatas.GetReportById(FakeDatas.fakeReportId)));

            //Act
            var actionResult = await _reportController.GetReportById(FakeDatas.fakeReportId);

            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (ResponseDatas<ReportViewModel>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.Id, FakeDatas.fakeReportId);
        }

        [Fact]
        public async Task GetReportById_TEST_404()
        {
            //Arrange

            _reportRepositoryMock.Setup(x => x.GetReportByIdAsync(It.IsAny<string>()))
              .Returns(Task.FromResult((Reports?)null));

            //Act
            var actionResult = await _reportController.GetReportById(FakeDatas.fakeReportId);

            //Assert
            var objectResult = (NotFoundResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.NotFound);
        }
    }
}
