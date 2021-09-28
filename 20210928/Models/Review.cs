﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20210928.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public Item Item { get; set; }
    }
}