using CleanArchitect.Application.DTOs.LeaveAllocation;
using CleanArchitect.Application.Features.LeaveAllocations.Requests.Commands;
using CleanArchitect.Application.Features.LeaveAllocations.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanArchitect.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LeaveAllocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<LeaveAllocationsController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> Get()
        {
            var leaveAllocations = await _mediator.Send(new GetLeaveAllocationListRequest());
            return Ok(leaveAllocations);
        }

        // GET api/<LeaveAllocationsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocationDto>> Get(int id)
        {
            var leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailRequest { Id = id });
            return Ok(leaveAllocation);
        }

        // POST api/<LeaveAllocationsController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateLeaveAllocationDto leaveAllocationDto)
        {
            var leaveAllocationId = await _mediator.Send(new CreateLeaveAllocationCommand { LeaveAllocationDto = leaveAllocationDto });
            return Ok(leaveAllocationId);
        }

        // PUT api/<LeaveAllocationsController>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveAllocationDto leaveAllocationDto)
        {
            await _mediator.Send(new UpdateLeaveAllocationCommand { LeaveAllocationDto = leaveAllocationDto });
            return NoContent();
        }

        // DELETE api/<LeaveAllocationsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteLeaveAllocationCommand { Id = id });
            return NoContent();
        }
    }
}
