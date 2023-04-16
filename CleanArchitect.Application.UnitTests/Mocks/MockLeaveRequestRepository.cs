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
    public static class MockLeaveRequestRepository
    {
        public static Mock<ILeaveRequestRepository> GetLeaveRequestRepository()
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
            var leaveRequests = new List<LeaveRequest>
            {
                new LeaveRequest
                {
                    Id = 1,
                    StartDate = new DateTime(DateTime.Now.Year, 1, 31),
                    EndDate = new DateTime(DateTime.Now.Year, 1, 1),
                    LeaveType = leaveTypes.FirstOrDefault(x => x.Id == 1),
                    LeaveTypeId = leaveTypes.FirstOrDefault(x => x.Id == 1).Id,
                    DateRequested = new DateTime(DateTime.Now.Year, 1, 2),
                    RequestComments = "Test Request Comments",
                    DateActioned = new DateTime(DateTime.Now.Year, 1, 3),
                    Approved = null,
                    Canceled = false
                },
                new LeaveRequest
                {
                    Id = 2,
                    StartDate = new DateTime(DateTime.Now.Year, 2, 1),
                    EndDate = new DateTime(DateTime.Now.Year, 2, 28),
                    LeaveType = leaveTypes.FirstOrDefault(x => x.Id == 2),
                    LeaveTypeId = leaveTypes.FirstOrDefault(x => x.Id == 2).Id,
                    DateRequested = new DateTime(DateTime.Now.Year, 2, 2),
                    RequestComments = "Test Request Comments",
                    DateActioned = new DateTime(DateTime.Now.Year, 2, 3),
                    Approved = null,
                    Canceled = false
                },
                new LeaveRequest
                {
                    Id = 3,
                    StartDate = new DateTime(DateTime.Now.Year, 3, 1),
                    EndDate = new DateTime(DateTime.Now.Year, 3, 31),
                    LeaveType = leaveTypes.FirstOrDefault(x => x.Id == 3),
                    LeaveTypeId = leaveTypes.FirstOrDefault(x => x.Id == 3).Id,
                    DateRequested = new DateTime(DateTime.Now.Year, 3, 2),
                    RequestComments = "Test Request Comments",
                    DateActioned = new DateTime(DateTime.Now.Year, 3, 3),
                    Approved = null,
                    Canceled = false
                },
                new LeaveRequest
                {
                    Id = 4,
                    StartDate = new DateTime(DateTime.Now.Year, 4, 1),
                    EndDate = new DateTime(DateTime.Now.Year, 4, 30),
                    LeaveType = leaveTypes.FirstOrDefault(x => x.Id == 1),
                    LeaveTypeId = leaveTypes.FirstOrDefault(x => x.Id == 1).Id,
                    DateRequested = new DateTime(DateTime.Now.Year, 4, 2),
                    RequestComments = "Test Request Comments",
                    DateActioned = new DateTime(DateTime.Now.Year, 4, 3),
                    Approved = null,
                    Canceled = false
                }
            };
            var mockRepo = new Mock<ILeaveRequestRepository>();

            //Get All
            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(leaveRequests);
            mockRepo.Setup(r => r.GetLeaveRequestsWithDetails()).ReturnsAsync(leaveRequests);

            //Create
            mockRepo.Setup(r => r.Add(It.IsAny<LeaveRequest>())).ReturnsAsync((LeaveRequest leaveRequest) =>
            {
                leaveRequests.Add(leaveRequest);
                return leaveRequest;
            });

            //Get by Id
            mockRepo.Setup(r => r.GetLeaveRequestWithDetails(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                var leaveRequest = leaveRequests.FirstOrDefault(t => t.Id == id);
                return leaveRequest;
            });
            mockRepo.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                var leaveRequest = leaveRequests.FirstOrDefault(t => t.Id == id);
                return leaveRequest;
            });

            //Delete
            mockRepo.Setup(r => r.Delete(It.IsAny<LeaveRequest>())).Callback((LeaveRequest lr) =>
            {
                var leaveRequest = leaveRequests.FirstOrDefault(t => t.Id == lr.Id);
                leaveRequests.Remove(leaveRequest);
            });

            //Update
            mockRepo.Setup(r => r.Update(It.IsAny<LeaveRequest>())).Callback((LeaveRequest lr) =>
            {
                var oldLeaveRequest = leaveRequests.FirstOrDefault(p => p.Id == lr.Id);
                oldLeaveRequest.StartDate = lr.StartDate;
                oldLeaveRequest.EndDate = lr.EndDate;
                oldLeaveRequest.LeaveTypeId = lr.LeaveTypeId;
            });

            //Change Approval Status
            mockRepo.Setup(r => r.ChangeApprovalStatus(It.IsAny<LeaveRequest>(), true)).Callback((LeaveRequest lr, bool? approved) =>
            {
                var oldLeaveRequest = leaveRequests.FirstOrDefault(p => p.Id == lr.Id);
                oldLeaveRequest.Approved = approved;
            });

            return mockRepo;
        }
    }
}
