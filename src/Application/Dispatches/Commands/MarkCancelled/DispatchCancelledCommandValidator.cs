using FluentValidation;
using FreightManagement.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreightManagement.Application.Dispatches.Commands.MarkCancelled
{
    public class DispatchCancelledCommandValidator : AbstractValidator<DispatchCancelledCommand>
    {
        private readonly IApplicationDbContext _context;

        public DispatchCancelledCommandValidator(IApplicationDbContext context)
        {
            _context = context;
        }

    }
}
