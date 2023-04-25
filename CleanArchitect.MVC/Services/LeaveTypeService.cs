using AutoMapper;
using CleanArchitect.MVC.Contracts;
using CleanArchitect.MVC.Models;
using CleanArchitect.MVC.Services.Base;

namespace CleanArchitect.MVC.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;
        private readonly IClient _httpClient;
        public LeaveTypeService(IMapper mapper, IClient httpClient, ILocalStorageService localStorageService) : base(localStorageService, httpClient)
        {
            _mapper = mapper;
            _httpClient = httpClient;
            _localStorageService = localStorageService;

        }
        public async Task<Response<int>> CreateLeaveType(CreateLeaveTypeVM createLeaveType)
        {
            try
            {
                var response = new Response<int>();
                CreateLeaveTypeDto createLeaveTypeDto = _mapper.Map<CreateLeaveTypeDto>(createLeaveType);
                var result = await _client.LeaveTypesPOSTAsync(createLeaveTypeDto);
                if(result.Success == true)
                {
                    response.Data = result.Id;
                    response.Success = true;
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        response.ValidationErrors += error + Environment.NewLine;
                    }
                }
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<Response<int>> DeleteLeaveType(int id)
        {
            try
            {
                await _client.LeaveTypesDELETEAsync(id);
                return new Response<int> { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<LeaveTypeVM> GetLeaveTypeDetails(int leaveTypeId)
        {
            var leaveType = await _client.LeaveTypesGETAsync(leaveTypeId);
            return _mapper.Map<LeaveTypeVM>(leaveType);
        }

        public async Task<List<LeaveTypeVM>> GetLeaveTypes()
        {
            var leaveTypes = await _client.LeaveTypesAllAsync();
            return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
        }

        public async Task<Response<int>> UpdateLeaveType(int id, LeaveTypeVM leaveType)
        {
            try
            {
                LeaveTypeDto leaveTypeDto = _mapper.Map<LeaveTypeDto>(leaveType);
                await _client.LeaveTypesPUTAsync(leaveTypeDto);
                return new Response<int> { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }
    }
}
