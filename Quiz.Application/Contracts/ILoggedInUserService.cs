﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Application.Contracts
{
    public interface ILoggedInUserService
    {
        public int UserId { get;  }
    }
}
