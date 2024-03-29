﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IQueryDb<T, K>
    {
        public Task<bool> Exists(K key);
    }
}
