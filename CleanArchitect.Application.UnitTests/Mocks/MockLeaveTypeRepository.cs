using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitect.Application.Contracts.Persistence;
using CleanArchitect.Domain;
using Moq;

namespace CleanArchitect.Application.UnitTests.Mocks
{
    public static class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetLeaveTypeRepository()
        {
            var leaveTypes = new List<LeaveType>
            {
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 10,
                    Name = "Test Vacation"
                },
                new LeaveType
                {
                    Id = 2,
                    DefaultDays = 15,
                    Name = "Test Sick"
                },
                new LeaveType
                {
                    Id = 3,
                    DefaultDays = 15,
                    Name = "Test Maternity"
                }
            };

            var mockRepo = new Mock<ILeaveTypeRepository>();
            //Get All
            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(leaveTypes);

            //Create
            mockRepo.Setup(r => r.Add(It.IsAny<LeaveType>())).ReturnsAsync((LeaveType leaveType) =>
            {
                leaveTypes.Add(leaveType);
                return leaveType;
            });

            //Get by Id
            mockRepo.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                var leaveType = leaveTypes.FirstOrDefault(t => t.Id == id);
                return leaveType;
            });

            //Delete
            mockRepo.Setup(r => r.Delete(It.IsAny<LeaveType>())).Callback((LeaveType lt) =>
            {
                var leaveType = leaveTypes.FirstOrDefault(t => t.Id == lt.Id);
                leaveTypes.Remove(leaveType);
            });

            //Update
            mockRepo.Setup(r => r.Update(It.IsAny<LeaveType>())).Callback((LeaveType lt) =>
            {
                var oldLeaveType = leaveTypes.FirstOrDefault(p => p.Id == lt.Id);
                if(oldLeaveType != null)
                {
                    oldLeaveType.Name = lt.Name;
                    oldLeaveType.DefaultDays = lt.DefaultDays;
                }
            });
            return mockRepo;
        }
    }
}
