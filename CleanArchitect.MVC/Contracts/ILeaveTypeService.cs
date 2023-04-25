using CleanArchitect.MVC.Models;
using CleanArchitect.MVC.Services.Base;

namespace CleanArchitect.MVC.Contracts
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeVM>> GetLeaveTypes();
        Task<LeaveTypeVM> GetLeaveTypeDetails(int  leaveTypeId);
        Task<Response<int>> CreateLeaveType(CreateLeaveTypeVM createLeaveType);
        Task<Response<int>> UpdateLeaveType(int id, LeaveTypeVM leaveType);
        Task<Response<int>> DeleteLeaveType(int id);
    }
}
