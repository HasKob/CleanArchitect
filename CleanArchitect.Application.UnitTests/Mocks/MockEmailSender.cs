using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitect.Application.Contracts.Infrastructure;
using CleanArchitect.Application.Models;
using Moq;

namespace CleanArchitect.Application.UnitTests.Mocks
{
    public static class MockEmailSender
    {
        public static Mock<IEmailSender> GetEmailSender()
        {
            var mockEmailSender = new Mock<IEmailSender>();
            mockEmailSender.Setup(s => s.SendEmail(It.IsAny<Email>())).ReturnsAsync(true);
            return mockEmailSender;
        }
    }
}
