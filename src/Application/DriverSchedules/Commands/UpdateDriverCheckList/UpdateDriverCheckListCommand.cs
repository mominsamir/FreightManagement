﻿using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.DriverSchedules.Commands.UpdateDriverCheckList
{
    public class UpdateDriverCheckListCommand :IRequest 
    {
        public long Id { get; }
        public IEnumerable<CheckListItemsCommand> CheckListItems { get; }

        public UpdateDriverCheckListCommand(long id, IEnumerable<CheckListItemsCommand> checkListItems)
        {
            Id = id;
            CheckListItems = checkListItems;
        }
    }

    public class CheckListItemsCommand
    {
        public long Id { get; }
        public bool IsChecked { get; }

        public CheckListItemsCommand(long id, bool isChecked)
        {
            Id = id;
            IsChecked = isChecked;
        }
    }

    public class UpdateDriverCheckListCommandHandler : IRequestHandler<UpdateDriverCheckListCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateDriverCheckListCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateDriverCheckListCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _context.DriverScheduleLists
                .Include(b=> b.CheckList)
                .Where(l=> l.Id ==  request.Id ).SingleOrDefaultAsync(cancellationToken);

            if (schedule == null)
                throw new NotFoundException(string.Format("Schedule not found with id {0}", request.Id));

            foreach(var item in request.CheckListItems)
                schedule.ToggleCheckListItem(item.Id, item.IsChecked);

//            schedule.TryCompleteCheckList();

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
