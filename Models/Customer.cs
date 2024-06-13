﻿namespace JSON_Market.Models;

public class Customer
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public List<Order> OrderHistory { get; set; } = [];
}