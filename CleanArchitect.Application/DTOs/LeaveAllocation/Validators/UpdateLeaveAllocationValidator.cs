using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitect.Application.Contracts.Persistence;
using FluentValidation;

namespace CleanArchitect.Application.DTOs.LeaveAllocation.Validators
{
    public class UpdateLeaveAllocationValidator : AbstractValidator<UpdateLeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public UpdateLeaveAllocationValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            Include(new ILeaveAllocationDtoValidator(_leaveTypeRepository));
            RuleFor(p => p.Id)
                .NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
