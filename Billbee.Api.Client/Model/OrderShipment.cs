﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    /// <summary>
    /// Information to add a shipment information from an external system to billbee
    /// </summary>
    public class OrderShipment
    {
        /// <summary>
        /// The id, the shipment got from the provider
        /// </summary>
        public string ShippingId { get; set; }
        /// <summary>
        /// The id of the order, this shipment should be attached to
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// A note, that should be attached to the shipment
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// If applicable, that internal id of the provider, the shipment was send with.
        /// </summary>
        public int? ShippingProviderId { get; set; }
        /// <summary>
        /// If applicable, the internal id of the shipping product, the shipment uses
        /// </summary>
        public int? ShippingProviderProductId { get; set; }
    }
}