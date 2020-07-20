﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AutoLot.Dal.Models.Entities.Base;

namespace AutoLot.Dal.Models.Entities
{
    [Table("Makes", Schema = "dbo")]
    public partial class Make : BaseEntity
    {
        [StringLength(50), Required] public string Name { get; set; } = "Ford";

        [JsonIgnore]
        [InverseProperty(nameof(Car.MakeNavigation))]
        public IEnumerable<Car> Cars { get; set; } = new List<Car>();

    }
}