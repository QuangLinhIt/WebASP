﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebASP.Data.Enums;

namespace WebASP.Data.Entities
{
    public class Promotion
    {
        public int Id { set; get; }
        public DateTime FromDate { set; get; }
        public DateTime ToDate { set; get; }
        public bool ApplyForAll { set; get; }
        public int? DiscountPercent { set; get; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? DiscountAmount { set; get; }
        public string ProductIds { set; get; }
        public string ProductCategoryIds { set; get; }
        public Status Status { set; get; }
        public string Name { set; get; }

    }
}
