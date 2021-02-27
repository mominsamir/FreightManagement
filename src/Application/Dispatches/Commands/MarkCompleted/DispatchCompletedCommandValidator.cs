using FluentValidation;
using FreightManagement.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreightManagement.Application.Dispatches.Commands.MarkCompleted
{
    public class DispatchCompletedCommandValidator: AbstractValidator<DispatchCompletedCommand>
    {
        private readonly IApplicationDbContext _context;

        public DispatchCompletedCommandValidator(IApplicationDbContext context)
        {
            _context = context;
        }
    }

}
