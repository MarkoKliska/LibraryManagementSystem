using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem.Contracts;

public abstract record IntegrationEvent
{
    public Guid EventId { get; set; } = Guid.NewGuid();
    public DateTime OccuredAtUtc { get; set; } = DateTime.UtcNow;
}
