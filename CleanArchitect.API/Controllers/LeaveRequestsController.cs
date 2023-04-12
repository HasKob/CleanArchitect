using CleanArchitect.Application.DTOs.LeaveRequest;
using CleanArchitect.Application.Features.LeaveRequests.Requests.Commands;
using CleanArchitect.Application.Features.LeaveRequests.Requests.Queries;
using CleanArchitect.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanArchitect.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LeaveRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<LeaveRequestsController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestListDto>>> Get()
        {
            var leaveRequests = await _mediator.Send(new GetLeaveRequestListRequest());
            return Ok(leaveRequests);
        }

        // GET api/<LeaveRequestsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDto>> Get(int id)
        {
            var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailRequest { Id = id });
            return Ok(leaveRequest);
        }

        // POST api/<LeaveRequestsController>
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveRequestDto leaveRequestDto)
        {
            var response = await _mediator.Send(new CreateLeaveRequestCommand { LeaveRequestDto = leaveRequestDto });
            return Ok(response);
        }

        // PUT api/<LeaveRequestsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveRequestDto leaveRequestDto)
        {
            await _mediator.Send(new UpdateLeaveRequestCommand { Id = id, LeaveRequestDto = leaveRequestDto });
            return NoContent();
        }
        // PUT api/<LeaveRequestsController>/ChangeLeaveRequestApproval/5
        [HttpPut("ChangeLeaveRequestApproval/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ChangeLeaveRequestApprovalDto leaveRequestDto)
        {
            await _mediator.Send(new UpdateLeaveRequestCommand { Id = id, ChangeLeaveRequestApprovalDto = leaveRequestDto });
            return NoContent();
        }

        // DELETE api/<LeaveRequestsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteLeaveRequestCommand { Id = id });
            return NoContent();
        }
    }
}
