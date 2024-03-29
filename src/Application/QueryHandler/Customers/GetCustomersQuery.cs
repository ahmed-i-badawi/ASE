﻿using AutoMapper;
using Shipping.Application.Common.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Shipping.Shared.Dto;
using Microsoft.Extensions.Caching.Memory;

namespace Shipping.Application.QueryHandler.Customers
{
    public class GetCustomersQuery : IRequest<List<CustomersDto>>
    {
        public string Search { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<CustomersDto>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public GetCustomersQueryHandler(IMemoryCache cache, IMapper mapper, IApplicationDbContext context)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;

        }

        public async Task<List<CustomersDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var items = _context.Customers.
                Select(e => new CustomersDto()
                {
                    Id = e.Id,
                    SId = e.SId,
                    NameAr = e.NameAr,
                    NameEn = e.NameEn,
                    Active = e.Active,
                    Age = e.Age,
                    CityName = e.CityName,
                    GenderName = e.GenderName,
                    Phone = e.Phone,
                    StateName = e.StateName,
                    Address = e.Address
                }
                ).ToList();

            if (!string.IsNullOrEmpty(request.Search))
            {
                return items.Where(i => i.NameAr.ToLower().Contains(request.Search.ToLower()) || i.NameEn.ToLower().Contains(request.Search.ToLower())).ToList();
            }
            if (request.Take > 0)
            {
                return items.OrderBy(i => i.NameAr).Take(request.Take).ToList();
            }


            return items;
        }
    }

}
