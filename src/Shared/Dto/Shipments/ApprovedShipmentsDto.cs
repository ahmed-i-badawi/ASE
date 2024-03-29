﻿using Shipping.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shipping.Shared.Dto
{
    public class ApprovedShipmentsDto
    {
        public int Id { get; set; }
        public string ShipmentName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }
        public int ReceiverStateId { get; set; }
        public string ReceiverStateName { get; set; }
        public int ReceiverCityId { get; set; }
        public string ReceiverCityName { get; set; }
        public string Notes { get; set; }
        public string Address { get; set; }
        public int CashToBeCollected { get; set; }
        public ShipmentStatus Status { get; set; }
        public int DeliveryManId { get; set; }
        public string DeliveryManName { get; set; }
    }
}
