﻿using homeassignment.server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homeassignment.server.GeaphQL
{
    public class GraphQLContinentsResponse
    {
        public ICollection<Continent> continents { get; set; }

    }
}
