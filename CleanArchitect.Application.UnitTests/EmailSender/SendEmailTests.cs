using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitect.Application.Contracts.Infrastructure;
using CleanArchitect.Application.Models;
using CleanArchitect.Application.UnitTests.Mocks;
using Microsoft.Extensions.Options;
using Moq;
using Shouldly;

namespace CleanArchitect.Application.UnitTests.EmailSender
{
    public class SendEmailTests
    {
        private readonly IOptions<EmailSettings> _options;
        private readonly Mock<IOptions<EmailSettings>> _mockOptions;
        private readonly Mock<IEmailSender> _mockEmailSender;
        public SendEmailTests()
        {
            _options = Options.Create(new EmailSettings
            {
                ApiKey = "YOUR_KEY",
                FromAddress = "noreply@cleanarchitect.com",
                FromName = "Leave Management System"
            });

            _mockOptions = new Mock<IOptions<EmailSettings>>();
            _mockOptions.Setup(x => x.Value).Returns(_options.Value);

            _mockEmailSender = MockEmailSender.GetEmailSender();
        }
        [Fact]
        public async Task SendEmail_ShouldReturnTrue()
        {
            // Arrange
            var email = new Email
            {
                To = "haskobcs@gmail.com",
                Subject = "test-subject",
                Body = "test-body"
            };

            // Act
            var result = await _mockEmailSender.Object.SendEmail(email);

            // Assert
            result.ShouldBeTrue();
            _mockEmailSender.Verify(x => x.SendEmail(email), Times.Once);
        }
    }
}
