using Shipping.Domain.Entities;
using Shipping.Application.Common.Interfaces;
using Shipping.Application.Lookups;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Shipping.Domain.Enums;
using Shipping.Shared.Commands.Shipments;
using Microsoft.EntityFrameworkCore;

namespace Shipping.Application.CommandHandler.Shipments
{
    public class CreateApprovedShipmentCommandHandler : IRequestHandler<CreateApprovedShipmentCommand, int>
    {

        private readonly IApplicationDbContext _context;
        private readonly ILookupService _lookupService;

        public CreateApprovedShipmentCommandHandler
        (
             IApplicationDbContext context
            , ILookupService lookupService
        )
        {
            _lookupService = lookupService;
            _context = context;
        }
        public async Task<int> Handle(CreateApprovedShipmentCommand request, CancellationToken cancellationToken)
        {

            int res = 0;
            try
            {

                foreach (var approvedShipment in request.ApprovedShipments.ShipmentsPerDeliveryMan)
                {

                    var s = await _context.Shipments.Include(e => e.ApprovedShipment).FirstAsync(e=>e.Id == approvedShipment.Id);
                    s.Status = ShipmentStatus.Approved;

                    var tt = new ApprovedShipment()
                    {
                        ShipmentRef = approvedShipment.Id,
                        ApprovedNotes = request.Notes,
                        DeliveryManId = approvedShipment.DeliveryManId,
                        DeliveryManName = approvedShipment.DeliveryManName,
                };
                    _context.ApprovedShipments.Add(tt);

                }

                    res = await _context.SaveChangesAsync(cancellationToken);
                    return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }
    }
}
