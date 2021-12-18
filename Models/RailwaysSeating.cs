using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainReservation.Models;

    public class RailwaysSeating
    {
        public int RailwaysSeatingID { get; set; }
        public int RailwaysNumber { get; set; }
        public string? FirstClassSeatNumbers { get; set; }
        public string? FirstClassSeatStatus { get; set; }
        public string? EconomyClassSeatNumbers { get; set; }
        public string? EconomyClassSeatStatus { get; set; }
    }

    public class AvailableSeats
    {
        public RailwaysSeating? railwaysSeating { get; set; }
        public int NumberOfTickets { get; set; }
    }

