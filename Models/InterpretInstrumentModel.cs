﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ebis.Models
{
    public class InterpretInstrumentModel
    {
        public IEnumerable<int> SelectedItemIds { get; set; }
        public IEnumerable<nastroje> AvailableItems { get; set; }
        public osoby Interpret { get; set; }
    }
}