﻿namespace FreecurrencyAPI.Models;

public class QuotaDetails
{
    public int Total { get; set; }

    public int Used { get; set; }

    public int Remaining { get; set; }
}