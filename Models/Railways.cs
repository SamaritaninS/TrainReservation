using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainReservation.Models;
public class Railways
{
    public int RailwaysID { get; set; }

    [Display(Name = "Railways Number")]
    public int RailwaysNumber { get; set; }

    [Display(Name = "Railways Name")]
    public string? RailwaysName { get; set; }

    [Display(Name = "Source")]
    public string? Source { get; set; }

    [Display(Name = "Destination")]
    public string? Destination { get; set; }

    [Display(Name ="Daparture Date")]
    public DateTime DepartureDate { get; set; }

    [Display(Name = "Depature Time")]
    public string? DepartsOn { get; set; }

    [Display(Name = "Arrival Time")]
    public string? ArrivesOn { get; set; }

    [Display(Name = "Economy Seats")]
    public int EconomyNos { get; set; }

    [Display(Name = "Сompartment Seats")]
    public int FirstNos { get; set; }

    [Display(Name = "Price Economy")]
    public int PriceEconomy { get; set; }

    [Display(Name = "Price Compartment")]
    public int PriceFirst { get; set; }

}

