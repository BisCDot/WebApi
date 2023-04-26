﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity
{
    public class CategoryEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual CourceEntity CourceEntity { get; set; }
    }
}
