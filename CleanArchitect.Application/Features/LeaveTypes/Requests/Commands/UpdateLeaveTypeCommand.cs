using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitect.Application.DTOs.LeaveType;
using MediatR;

namespace CleanArchitect.Application.Features.LeaveTypes.Requests.Commands
{
    public class UpdateLeaveTypeCommand: IRequest<Unit>
    {
        public LeaveTypeDto LeaveTypeDto { get; set; }
    }
}
