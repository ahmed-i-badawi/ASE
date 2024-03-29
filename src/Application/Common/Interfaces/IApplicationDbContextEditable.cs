﻿using Shipping.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Shipping.Application.Common.Interfaces
{
    public partial interface IApplicationDbContext
    {
    DbSet<TodoList> TodoLists { get; set; }

        DbSet<TodoItem> TodoItems { get; set; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
