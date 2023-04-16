using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitect.Application.Contracts.Infrastructure;
using CleanArchitect.Application.Contracts.Persistence;
using CleanArchitect.Application.Profiles;
using CleanArchitect.Application.UnitTests.Mocks;
using Moq;

namespace CleanArchitect.Application.UnitTests.LeaveRequests
{
    public abstract class LeaveRequestTestBase
    {
        protected readonly IMapper _mapper;
        protected readonly Mock<ILeaveRequestRepository> _mockLeaveRequestRepo;
        protected readonly Mock<ILeaveTypeRepository> _mockLeaveTypeRepo;
        protected readonly Mock<IEmailSender> _mockEmailSender;
        public LeaveRequestTestBase()
        {
            _mockLeaveRequestRepo = MockLeaveRequestRepository.GetLeaveRequestRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _mockLeaveTypeRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();
            _mockEmailSender = MockEmailSender.GetEmailSender();

        }
    }
}
