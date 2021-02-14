using FreightManagement.Application.Common.Interfaces;
using System;

namespace FreightManagement.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
