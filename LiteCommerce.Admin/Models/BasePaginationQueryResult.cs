﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public abstract class BasePaginationQueryResult
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public string SearchValue { get; set; }

        public int RowCount { get; set; }

        public int PageCount
        {
            get
            {
                int v = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                    v = +1;
                return v;
            }
        }
    }
}