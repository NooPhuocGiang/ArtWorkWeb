﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessTier.Payload
{
    public class PagingModel
    {
        public int page { get; set; } = 1;
        public int size { get; set; } = 10;
    }
}
