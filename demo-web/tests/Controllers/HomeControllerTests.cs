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
    }
}
