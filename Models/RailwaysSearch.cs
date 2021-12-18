﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainReservation.Models;

    public class RailwaysSearch
    {
        public string? FromSearch { get; set; }
        public string? ToSearch { get; set; }
        public string? ClassSearch { get; set; }
        public DateTime DateSearch { get; set; }
        public int AdultSearch { get; set; }
        public int ChildrenSearch { get; set; }
        public int InfantSearch { get; set; }
    }

