using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainReservation.Models;

namespace TrainReservation.Data;

    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if(context.Railways.Any())
            {
                return;
            }

            //----< Adding 4 flights for Testing the workinng of application.
            //      More flights can be added from the Admin Application >-----

            var railways = new Railways[]
            {
                new Railways{RailwaysNumber=12345,RailwaysName="United",Source="Syracuse",Destination="New Jersey",DepartsOn=new DateTime().AddHours(9).AddMinutes(10).ToString("HH:mm"),
                               DepartureDate=new DateTime().AddDays(04).AddMonths(08).AddYears(2018) ,ArrivesOn=new DateTime().AddHours(14).AddMinutes(25).ToString("HH:mm"), EconomyNos=36,FirstNos=6, PriceEconomy=100,PriceFirst=400},

                new Railways{RailwaysNumber=89101,RailwaysName="Jet Blue",Source="Syracuse",Destination="New Jersey",DepartsOn=new DateTime().AddHours(11).AddMinutes(10).ToString("HH:mm"),
                               DepartureDate=new DateTime().AddDays(04).AddMonths(08).AddYears(2018) ,ArrivesOn=new DateTime().AddHours(14).AddMinutes(25).ToString("HH:mm"), EconomyNos=36,FirstNos=6, PriceEconomy=150,PriceFirst=600},

                new Railways{RailwaysNumber=45678,RailwaysName="American Airline",Source="New Jersey",Destination="Syracuse",DepartsOn=new DateTime().AddHours(15).AddMinutes(00).ToString("HH:mm"),
                                DepartureDate=new DateTime().AddDays(04).AddMonths(08).AddYears(2018),ArrivesOn=new DateTime().AddHours(19).AddMinutes(00).ToString("HH:mm"), EconomyNos=48,FirstNos=12,PriceEconomy=200,PriceFirst=500},

                new Railways{RailwaysNumber=76543,RailwaysName="United",Source="New Jersey",Destination="Syracuse",DepartsOn=new DateTime().AddHours(16).AddMinutes(00).ToString("HH:mm"),
                                DepartureDate=new DateTime().AddDays(04).AddMonths(08).AddYears(2018),ArrivesOn=new DateTime().AddHours(20).AddMinutes(00).ToString("HH:mm"), EconomyNos=48,FirstNos=12,PriceEconomy=250,PriceFirst=600}
            };

            foreach (Railways f in railways)
            {
                context.Railways.Add(f);
            }
            context.SaveChanges();

            //----< Adding the seat numbers and the initial seating arrangement of above flights 
            //      When a flight is added it's corressponding seating arrangement is saved in the 
            //      table automaticall, without making the adminstrator to input anything >----
            var railwayseating = new RailwaysSeating[]
            {
                new RailwaysSeating{RailwaysNumber=12345, FirstClassSeatNumbers="1A,1B,1C,1D,1E,1F",FirstClassSeatStatus="O,O,O,O,O,O",
                                    EconomyClassSeatNumbers="2A,2B,2C,2D,2E,2F,3A,3B,3C,3D,3E,3F,4A,4B,4C,4D,4E,4F,5A,5B,5C,5D,5E,5F,6A,6B,6C,6D,6E,6F,7A,7B,7C,7D,7E,7F"
                                    ,EconomyClassSeatStatus="O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O"},

                 new RailwaysSeating{RailwaysNumber=89101, FirstClassSeatNumbers="1A,1B,1C,1D,1E,1F",FirstClassSeatStatus="O,O,O,O,O,O",
                                    EconomyClassSeatNumbers="2A,2B,2C,2D,2E,2F,3A,3B,3C,3D,3E,3F,4A,4B,4C,4D,4E,4F,5A,5B,5C,5D,5E,5F,6A,6B,6C,6D,6E,6F,7A,7B,7C,7D,7E,7F"
                                    ,EconomyClassSeatStatus="O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O"},

                new RailwaysSeating{RailwaysNumber=45678, FirstClassSeatNumbers="1A,1B,1C,1D,1E,1F,2A,2B,2C,2D,2E,2F",FirstClassSeatStatus="O,O,O,O,O,O,O,O,O,O,O,O",
                                    EconomyClassSeatNumbers="2A,2B,2C,2D,2E,2F,3A,3B,3C,3D,3E,3F,4A,4B,4C,4D,4E,4F,5A,5B,5C,5D,5E,5F,6A,6B,6C,6D,6E,6F,7A,7B,7C,7D,7E,7F,8A,8B,8C,8D,8E,8F,9A,9B,9C,9D,9E,9F",
                                    EconomyClassSeatStatus ="O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O"},

                new RailwaysSeating{RailwaysNumber=76543, FirstClassSeatNumbers="1A,1B,1C,1D,1E,1F,2A,2B,2C,2D,2E,2F",FirstClassSeatStatus="O,O,O,O,O,O,O,O,O,O,O,O",
                                    EconomyClassSeatNumbers="2A,2B,2C,2D,2E,2F,3A,3B,3C,3D,3E,3F,4A,4B,4C,4D,4E,4F,5A,5B,5C,5D,5E,5F,6A,6B,6C,6D,6E,6F,7A,7B,7C,7D,7E,7F,8A,8B,8C,8D,8E,8F,9A,9B,9C,9D,9E,9F",
                                    EconomyClassSeatStatus ="O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O"}

            };

            foreach(RailwaysSeating f in railwayseating)
            {
                context.RailwaysSeatings.Add(f);
            }
            context.SaveChanges();
        }
    }

