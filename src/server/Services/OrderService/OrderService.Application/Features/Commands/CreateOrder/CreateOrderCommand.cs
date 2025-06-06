﻿using MediatR;
using OrderService.Domain.Models;

namespace OrderService.Application.Features.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<bool>
    {
        private readonly List<OrderItemDTO> _orderItems;

        public Guid UserId { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
        public string CardNumber { get; private set; }
        public string CardHolderName { get; private set; }
        public DateTime CardExpiration { get; private set; }
        public string CardSecurityNumber { get; private set; }
        public int CardTypeId { get; private set; }
        public string Description { get; private set; }
        public IEnumerable<OrderItemDTO> OrderItems => _orderItems;

        public CreateOrderCommand()
        {
            _orderItems = [];
        }


        public CreateOrderCommand(List<BasketItem> basketItems, Guid userId, string userName,
            string email, string city, string street, string state, string country, string zipcode,
            string cardNumber, string cardHolderName, DateTime cardExpiration,
            string cardSecurityNumber, int cardTypeId, string description) : this()
        {
            var dtoList = basketItems.Select(item => new OrderItemDTO()
            {
                ItemId = item.ItemId,
                ItemName = item.ItemName,
                PictureUrl = item.PictureUrl,
                UnitPrice = item.UnitPrice,
                Units = item.Quantity
            });

            _orderItems = dtoList.ToList();

            UserId = userId;
            UserName = userName;
            Email = email;
            City = city;
            Street = street;
            State = state;
            Country = country;
            ZipCode = zipcode;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            CardExpiration = cardExpiration;
            CardSecurityNumber = cardSecurityNumber;
            CardTypeId = cardTypeId;
            CardExpiration = cardExpiration;
            Description = description;
        }
    }

    public class OrderItemDTO
    {
        public int ItemId { get; init; }
        public string ItemName { get; init; }
        public decimal UnitPrice { get; init; }
        public int Units { get; init; }
        public string PictureUrl { get; init; }
    }
}
