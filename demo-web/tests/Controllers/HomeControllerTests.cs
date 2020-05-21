using System;
using Xunit;
using Moq;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using demo_web.Controllers;
using demo_web.Models;

namespace demo_web_tests
{
    public class TestsFixture : IDisposable
    {
        public Mock<ILogger<HomeController>> Logger { get; private set; }
        public TelemetryClient TelemetryClient { get ;private set; }

        public TestsFixture()
        {
            this.Logger = new Mock<ILogger<HomeController>>();
            #pragma warning disable CS0618 
            this.TelemetryClient = new TelemetryClient();
            #pragma warning restore CS0618
        }

        public void Dispose()
        {

        }
    }

    public class HomeControllerTests : IClassFixture<TestsFixture>
    {
        private readonly TestsFixture _fixture;

        public HomeControllerTests(TestsFixture fixture) {
            this._fixture = fixture;
        }

        [Fact]
        public void CalcForm_Val1NaN_ThrowsArgumentException()
        {
            // Arrange
            var homeController = new HomeController(_fixture.Logger.Object, _fixture.TelemetryClient);
            var calcModel = new CalcModel{ Num1 = "test", Num2 = "5" };

            // Act/Assert
            var result = Assert.Throws<ArgumentException>(() => homeController.CalcForm(calcModel));
            Assert.Equal("Value must be a number.", result.Message);
            Assert.Equal(calcModel, result.Data["CalcsData"]);
            Assert.Equal("Value must be a number.", result.Data["ExceptionMessage"]);
        }

        [Fact]
        public void CalcForm_Val2NaN_ThrowsArgumentException()
        {
            // Arrange
            var homeController = new HomeController(_fixture.Logger.Object, _fixture.TelemetryClient);
            var calcModel = new CalcModel{ Num1 = "5", Num2 = "test" };

            // Act/Assert
            var result = Assert.Throws<ArgumentException>(() => homeController.CalcForm(calcModel));
            Assert.Equal("Value must be a number.", result.Message);
            Assert.Equal(calcModel, result.Data["CalcsData"]);
            Assert.Equal("Value must be a number.", result.Data["ExceptionMessage"]);
        }

        [Fact]
        public void CalcForm_Val1OutofRange_ThrowsArithmeticException()
        {
            // Arrange
            var homeController = new HomeController(_fixture.Logger.Object, _fixture.TelemetryClient);
            var calcModelNegative = new CalcModel{ Num1 = "-5", Num2 = "10" };
            var calcModelTooHigh = new CalcModel{ Num1 = "105", Num2 = "10" };

            // Act/Assert
            var resultNegative = Assert.Throws<ArithmeticException>(() => homeController.CalcForm(calcModelNegative));
            Assert.Equal("Value out of range.", resultNegative.Message);
            Assert.Equal(calcModelNegative, resultNegative.Data["CalcsData"]);
            Assert.Equal("Value out of range.", resultNegative.Data["ExceptionMessage"]);

            var resultTooHigh = Assert.Throws<ArithmeticException>(() => homeController.CalcForm(calcModelTooHigh));
            Assert.Equal("Value out of range.", resultTooHigh.Message);
            Assert.Equal(calcModelTooHigh, resultTooHigh.Data["CalcsData"]);
            Assert.Equal("Value out of range.", resultTooHigh.Data["ExceptionMessage"]);
        }

        [Fact]
        public void CalcForm_Val2OutofRange_ThrowsArithmeticException()
        {
            // Arrange
            var homeController = new HomeController(_fixture.Logger.Object, _fixture.TelemetryClient);
            var calcModelNegative = new CalcModel{ Num1 = "10", Num2 = "-5" };
            var calcModelTooHigh = new CalcModel{ Num1 = "10", Num2 = "105" };

            // Act/Assert
            var resultNegative = Assert.Throws<ArithmeticException>(() => homeController.CalcForm(calcModelNegative));
            Assert.Equal("Value out of range.", resultNegative.Message);
            Assert.Equal(calcModelNegative, resultNegative.Data["CalcsData"]);
            Assert.Equal("Value out of range.", resultNegative.Data["ExceptionMessage"]);

            var resultTooHigh = Assert.Throws<ArithmeticException>(() => homeController.CalcForm(calcModelTooHigh));
            Assert.Equal("Value out of range.", resultTooHigh.Message);
            Assert.Equal(calcModelTooHigh, resultTooHigh.Data["CalcsData"]);
            Assert.Equal("Value out of range.", resultTooHigh.Data["ExceptionMessage"]);
        }

        [Fact]
        public void CalcForm_Val2IsZero_ThrowsDivideByZeroException()
        {
            // Arrange
            var homeController = new HomeController(_fixture.Logger.Object, _fixture.TelemetryClient);
            var calcModel = new CalcModel{ Num1 = "5", Num2 = "0" };

            // Act/Assert
            var result = Assert.Throws<DivideByZeroException>(() => homeController.CalcForm(calcModel));
            Assert.Equal("Denominator cannot be 0.", result.Message);
            Assert.Equal(calcModel, result.Data["CalcsData"]);
            Assert.Equal("Denominator cannot be 0.", result.Data["ExceptionMessage"]);
        }

        [Theory]
        [InlineData("15", "5", 3.0)]
        [InlineData("50", "10", 5.0)]
        [InlineData("0", "10", 0.0)]
        public void CalcForm_SuppliesNumbers_ReturnsCorrectResult(string num1, string num2, decimal result)
        {
            // Arrange
            var homeController = new HomeController(_fixture.Logger.Object, _fixture.TelemetryClient);
            var calcModel = new CalcModel{ Num1 = num1, Num2 = num2 };

            // Act
            var viewResult =  homeController.CalcForm(calcModel);

            // Assert
            Assert.IsType<ViewResult>(viewResult);
            Assert.IsType<CalcModel>(((ViewResult)viewResult).Model);
            Assert.Equal(result, ((CalcModel)((ViewResult)viewResult).Model).Result);
        }
    }
}
