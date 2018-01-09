﻿using Billbee.Api.Client.Enums;
using Billbee.Api.Client.Model;
using BillBee.API.Client;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Endpoint
{
    /// <summary>
    /// Endpoint to access order functions
    /// </summary>
    public class OrderEndPoint : RestClientBaseClass
    {
        internal OrderEndPoint(ApiConfiguration config, ILogger logger = null) : base(logger, config)
        {

        }

        /// <summary>
        /// Selects an order by it's id
        /// </summary>
        /// <param name="id">id to search for</param>
        /// <returns>Details of the order</returns>
        public ApiResult<Order> GetOrder(string id)
        {
            return requestResource<ApiResult<Order>>($"/orders/{id}");
        }

        /// <summary>
        /// Selects an order by it's external id
        /// </summary>
        /// <param name="id">The external id, that should be used for selection</param>
        /// <returns>Details of the order</returns>
        public ApiResult<Order> GetOrderByExternalReference(string id)
        {
            return requestResource<ApiResult<Order>>($"/orders/findbyextref/{id}");
        }

        /// <summary>
        /// Selects an order by it's external id and its partner
        /// </summary>
        /// <param name="partner">If set, this is the internal partner name, this order should be searched at.</param>
        /// <param name="id">The external id, that should be used for selection</param>
        /// <returns>Details of the order</returns>
        public ApiResult<Order> GetOrderByExternalReferenceAndPartner(string partner, string id)
        {
            return requestResource<ApiResult<Order>>($"/orders/find/{id}/{partner}");
        }

        /// <summary>
        /// Delivers a list of orders
        /// </summary>
        /// <param name="minOrderDate">Minimum date of the order</param>
        /// <param name="maxOrderDate">Maximum date of the order</param>
        /// <param name="page">Selected page</param>
        /// <param name="pageSize">Selected number of entries per page</param>
        /// <param name="shopId">Id of the shop, the order belongs to</param>
        /// <param name="orderStateId">State of the order.<<see cref="OrderStateEnum"/></param>
        /// <param name="tag">If given, only orders with this tag are returned</param>
        /// <param name="minimumBillBeeOrderId">Minimum internal id of the order. As ids are in squence, this can be used to get only orders, not already imported.</param>
        /// <param name="modifiedAtMin">Minimum date of last modification</param>
        /// <param name="modifiedAtMax">Maximum date of last modification</param>
        /// <returns></returns>
        public ApiResult<List<Order>> GetOrderList(DateTime? minOrderDate = null, DateTime? maxOrderDate = null,
            int page = 1, int pageSize = 50, List<int> shopId = null,
            List<int> orderStateId = null, List<string> tag = null, int? minimumBillBeeOrderId = null,
            DateTime? modifiedAtMin = null, DateTime? modifiedAtMax = null)
        {
            NameValueCollection parameters = new NameValueCollection();

            if (minOrderDate != null)
                parameters.Add("minOrderDate", minOrderDate.Value.ToString("yyyy-MM-dd HH:mm"));
            if (maxOrderDate != null)
                parameters.Add("maxOrderDate", maxOrderDate.Value.ToString("yyyy-MM-dd HH:mm"));
            if (modifiedAtMin != null)
                parameters.Add("modifiedAtMin", modifiedAtMax.Value.ToString("yyyy-MM-dd HH:mm"));
            if (modifiedAtMax != null)
                parameters.Add("modifiedAtMax", modifiedAtMax.Value.ToString("yyyy-MM-dd HH:mm"));
            if (minimumBillBeeOrderId != null)
                parameters.Add("minimumBillBeeOrderId", minimumBillBeeOrderId.ToString());

            if (shopId != null)
            {
                int i = 0;
                foreach (var id in shopId)
                {
                    parameters.Add($"shopId[{i++}]", id.ToString());
                }
            }

            if (tag != null)
            {
                int i = 0;
                foreach (var id in tag)
                {
                    parameters.Add($"tag[{i++}]", id.ToString());
                }
            }

            if (orderStateId != null)
            {
                int i = 0;
                foreach (var id in orderStateId)
                {
                    parameters.Add($"orderStateId[{i++}]", id.ToString());
                }
            }

            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());

            return requestResource<ApiResult<List<Order>>>("/orders", parameters);
        }

        /// <summary>
        /// Delivers a list of invoices
        /// </summary>
        /// <param name="minInvoiceDate">Minimum date of the invoice</param>
        /// <param name="maxInvoiceDate">Maximum date of the invoice</param>
        /// <param name="page">Selected page</param>
        /// <param name="pageSize">Selected number of entries per page</param>
        /// <param name="shopId">Id of the shop, the order belongs to</param>
        /// <param name="orderStateId">State of the order. <<see cref="OrderStateEnum"/></param>
        /// <param name="tag">If given, only orders with this tag are returned</param>
        /// <param name="minPayDate">Minimum date, where the payment occured</param>
        /// <param name="maxPayDate">Maximum date, where the payment occured</param>
        /// <param name="includePositions">Should the invoice data contain all invoice positions?</param>
        /// <returns></returns>
        public ApiResult<List<InvoiceDetail>> GetInvoiceList(DateTime? minInvoiceDate = null, DateTime? maxInvoiceDate = null,
            int page = 1, int pageSize = 50, List<int> shopId = null,
            List<int> orderStateId = null,
            List<string> tag = null,
            DateTime? minPayDate = null, DateTime? maxPayDate = null,
            bool includePositions = false)
        {
            NameValueCollection parameters = new NameValueCollection();

            if (minInvoiceDate != null)
                parameters.Add("minInvoiceDate", minInvoiceDate.Value.ToString("yyyy-MM-dd HH:mm"));
            if (maxInvoiceDate != null)
                parameters.Add("maxInvoiceDate", maxInvoiceDate.Value.ToString("yyyy-MM-dd HH:mm"));
            if (minPayDate != null)
                parameters.Add("minPayDate", minPayDate.Value.ToString("yyyy-MM-dd HH:mm"));
            if (maxPayDate != null)
                parameters.Add("maxPayDate", maxPayDate.Value.ToString("yyyy-MM-dd HH:mm"));


            if (shopId != null)
            {
                int i = 0;
                foreach (var id in shopId)
                {
                    parameters.Add($"shopId[{i++}]", id.ToString());
                }
            }

            if (tag != null)
            {
                int i = 0;
                foreach (var id in tag)
                {
                    parameters.Add($"tag[{i++}]", id.ToString());
                }
            }

            if (orderStateId != null)
            {
                int i = 0;
                foreach (var id in orderStateId)
                {
                    parameters.Add($"orderStateId[{i++}]", id.ToString());
                }
            }


            parameters.Add("includePositions", includePositions.ToString());
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            return requestResource<ApiResult<List<InvoiceDetail>>>("/orders/invoices", parameters);

        }

        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="order">An order object, to create in billbee</param>
        /// <param name="shopId">The id of the shop. Neccessary, to attach an order directly to a shop connection</param>
        /// <returns></returns>
        public ApiResult<OrderResult> PostNewOrder(Order order, int? shopId = null)
        {
            NameValueCollection parameters = new NameValueCollection();
            if (shopId.HasValue)
            {
                parameters.Add("shopId", shopId.ToString());
            }

            return post<ApiResult<OrderResult>>("/orders", order, parameters.Count > 0 ? parameters: null);
        }

        /// <summary>
        /// Reset the tags on an order and add the given ones.
        /// All previuosly added tags will be deleted.
        /// </summary>
        /// <param name="tags">Tasg to add</param>
        /// <param name="orderId">Id of the order, where the tags should be edited.</param>
        /// <returns>ApiResult with the result of the update operation</returns>
        public ApiResult<dynamic> AddTags(List<string> tags, int orderId)
        {
            return post<ApiResult<dynamic>>($"/orders/{orderId}/tags", new { Tags = tags });
        }

        /// <summary>
        /// Reset the tags on an order and add the given ones.
        /// All previuosly added tags will be deleted.
        /// </summary>
        /// <param name="tags">Tasg to add</param>
        /// <param name="orderId">Id of the order, where the tags should be edited.</param>
        /// <returns>ApiResult with the result of the update operation</returns>
        public ApiResult<dynamic> UpdateTags(List<string> tags, int orderId)
        {
            return put<ApiResult<dynamic>>($"/orders/{orderId}/tags", new { Tags = tags });
        }

        /// <summary>
        /// Add a shipment, that was created in an external system to an order.
        /// </summary>
        /// <param name="shipment"></param>
        public void AddShipment(OrderShipment shipment)
        {
            post($"/orders/{shipment.OrderId}/shipment", shipment);
        }

        /// <summary>
        /// Requests the delivery note information for the given order.
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="includePdf">Should the pdf file be included in the response?</param>
        /// <returns>The delivery note information, if the the order was found and an delivery note was already created.</returns>
        public ApiResult<DeliveryNote> CreateDeliveryNote(int orderId, bool includePdf = false)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("includePdf", includePdf.ToString());
            return post<ApiResult<DeliveryNote>>($"/orders/CreateDeliveryNote/{orderId}", parameters);

        }

        /// <summary>
        /// Requests the invoice document for the given order.
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="includePdf">Should the pdf file be included in the response?</param>
        /// <returns>The invoice data, if the the order was found and an invoice was already created.</returns>
        public ApiResult<Invoice> CreateInvoice(int orderId, bool includePdf = false)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("includeInvoicePdf", includePdf.ToString());
            return post<ApiResult<Invoice>>($"/orders/CreateInvoice/{orderId}", parameters);

        }
    }
}