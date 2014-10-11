﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kheper.Core.Store
{
    using Kheper.Core.Model;

    public interface IVoteRepository : IRepository<Vote, VoteId>
    {
    }
}
