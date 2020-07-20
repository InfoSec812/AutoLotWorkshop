﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AutoLot.Dal.Models.Entities.Base;
using AutoLot.Dal.Models.Entities.Owned;

namespace AutoLot.Dal.Models.Entities
{
    [Table("Customers",Schema = "Dbo")]
    public partial class Customer : BaseEntity
    {
        public Person PersonalInformation { get; set; } = new Person();

        [JsonIgnore]
        [InverseProperty(nameof(CreditRisk.CustomerNavigation))]
        public IEnumerable<CreditRisk> CreditRisks { get; set; } = new List<CreditRisk>();

        [JsonIgnore]
        [InverseProperty(nameof(Order.CustomerNavigation))]
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();

        [NotMapped]
        public string FullName => $"{PersonalInformation?.FirstName} {PersonalInformation?.LastName}";

    }
}
