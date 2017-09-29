﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab6.Models
{
    public class StoreSettingsOptions
    {
        public string StoreName { get; set; }
        public int StoreID { get; set; }
        public Dictionary<string, StoreSetting> Settings { get; set; }
    }

    public class StoreSetting
    {
        public string Value { get; set; }
        public bool Enabled { get; set; }
    }
}
