using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using TrainReservation.Data;
using TrainReservation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace TrainReseravtion.Controllers;


    public class RailwaysController : Controller
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ApplicationDbContext _context;
      


    public RailwaysController(ApplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

     
    
        [AllowAnonymous]
        public IActionResult ViewRailways()
        {
            try
            {
                return View(_context.Railways.ToList<Railways>());
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }


        public IActionResult ViewMyRailways()
        {
            try
            {
                return View(_context.ReservationInfos.ToList<ReservationInfo>());
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }



        [AllowAnonymous]
        public ActionResult RailwaysDetails(int? id)
        {
            if(id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            Railways railway = _context.Railways.Find(id);

            if(railway == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return View(railway);
        }

        [Authorize]
        public ActionResult MyRailwaysDetails(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            ReservationInfo reservationInfo = _context.ReservationInfos.Find(id);

            if (reservationInfo == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return View(reservationInfo);
        }

        public IActionResult DeleteMyRailway(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            try
            {
                var railway = _context.ReservationInfos.Find(id);
                if (railway != null)
                {
                    _context.Remove(railway);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("ViewMyRailways");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditRailwayDetails(int? id)
        {
            if(id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            Railways railway = _context.Railways.Find(id);

            if(railway == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(railway);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditRailwayDetails(int? id, Railways flt)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var railway = _context.Railways.Find(id);
            if(railway != null)
            {
                railway.RailwaysNumber = flt.RailwaysNumber;
                railway.RailwaysName = flt.RailwaysName;
                railway.Source = flt.Source;
                railway.Destination = flt.Destination;
                railway.DepartsOn = flt.DepartsOn;
                railway.ArrivesOn = flt.ArrivesOn;
                railway.EconomyNos = flt.EconomyNos;
                railway.FirstNos= flt.FirstNos;
                railway.PriceEconomy= flt.PriceEconomy;
                railway.PriceFirst= flt.PriceFirst;
                try
                {
                    _context.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            return RedirectToAction("ViewRailways");
        }


        [Authorize(Roles = "Admin")]
        public IActionResult DeleteRailway(int? id)
        {
            if(id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            try
            {
                var railway = _context.Railways.Find(id);
                if(railway != null)
                {
                    _context.Remove(railway);
                    _context.SaveChanges();
                }
            }
            catch(Exception)
            {
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("ViewRailways");
        }


        [AllowAnonymous]
        public IActionResult AvailableRailways(RailwaysSearch railwaysSearch)
        {
            if (_context.Railways.Any(fl => fl.RailwaysID == 0))
            {
                return RedirectToAction("Error", "Home");
            }
            try
            {
                TempData["DOJ"] = railwaysSearch.DateSearch;
              
                int totalTicketCount = railwaysSearch.AdultSearch + railwaysSearch.ChildrenSearch + railwaysSearch.InfantSearch; 
                IQueryable<ReturningValue> railwayOrder = Enumerable.Empty<ReturningValue>().AsQueryable();

                if (railwaysSearch.ClassSearch.Equals("economy"))
                {
                    var search = _context.Railways.Where(s => s.Source.Equals(railwaysSearch.FromSearch)
                                                     && s.Destination.Equals(railwaysSearch.ToSearch)
                                                     && s.DepartureDate.Equals(railwaysSearch.DateSearch)
                                                     && s.EconomyNos >= (railwaysSearch.AdultSearch + railwaysSearch.ChildrenSearch + railwaysSearch.InfantSearch));
                         railwayOrder = search.OrderBy(s => s.DepartsOn)
                        .Select(s => new ReturningValue
                        {
                            RailwaysNumber = s.RailwaysNumber,
                            Name = s.RailwaysName,
                            Departure = s.DepartsOn,
                            Arrival = s.ArrivesOn,
                            PFirst = 0,                     
                            PEconomy = s.PriceEconomy * totalTicketCount,
                            TicketSelected = totalTicketCount
                        });
                }
                if (railwaysSearch.ClassSearch.Equals("first"))
                {
                    var search = _context.Railways.Where(s => s.Source.Equals(railwaysSearch.FromSearch)
                                                     && s.Destination.Equals(railwaysSearch.ToSearch)
                                                     && s.DepartureDate.Equals(railwaysSearch.DateSearch)
                                                     && s.FirstNos >= (railwaysSearch.AdultSearch + railwaysSearch.ChildrenSearch + railwaysSearch.InfantSearch));
                         railwayOrder = search.OrderBy(s => s.DepartsOn)
                        .Select(s => new ReturningValue
                        {
                            RailwaysNumber = s.RailwaysNumber,
                            Name = s.RailwaysName,
                            Departure = s.DepartsOn,
                            Arrival = s.ArrivesOn,
                            PFirst = s.PriceFirst * totalTicketCount,
                            PEconomy = 0,               //Selected class is Economy, so make Economy 0
                            TicketSelected=totalTicketCount
                        });

                   
                }
                return View(railwayOrder);
            }
            catch (Exception)
            {
               
                return RedirectToAction("Error", "Home");
            }
        }


        [Authorize]
        public IActionResult BookRailway(int? id, int ticketNum, string ticketClass)
        {
            if (id == null || ticketNum == 0 || ticketClass == null )
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            try
            {
                TempData["RailwayId"] = id;

                IQueryable<AvailableSeats> railwaySeatings = Enumerable.Empty<AvailableSeats>().AsQueryable();
                var seats = _context.RailwaysSeatings.Where(s => s.RailwaysNumber.Equals(id));

                
                if (ticketClass.Equals("Economy"))
                {

                    railwaySeatings = seats.Select(s => new AvailableSeats
                    {
                        railwaysSeating = new RailwaysSeating
                        {
                            RailwaysNumber = s.RailwaysNumber,
                            EconomyClassSeatNumbers = s.EconomyClassSeatNumbers,
                            EconomyClassSeatStatus = s.EconomyClassSeatStatus
                        },
                        NumberOfTickets = ticketNum
                    });
                                                                
                }
                else
                {
                    railwaySeatings = seats.Select(s => new AvailableSeats
                    {
                        railwaysSeating = new RailwaysSeating
                        {
                            RailwaysNumber = s.RailwaysNumber,
                            FirstClassSeatNumbers = s.FirstClassSeatNumbers,
                            FirstClassSeatStatus = s.FirstClassSeatStatus
                        },
                        NumberOfTickets = ticketNum
                    });
                }
                return View(railwaySeatings);
            }
            catch(Exception)
            {
                return RedirectToAction("Error", "Home");
            }
            
        }

    
    public IActionResult BookTicket(TicketInfo bookTicket, ClaimsPrincipal claimsPrincipal)
        {

            string firstName = string.Join(",", bookTicket.FirstName.ToArray());
            string lastName = string.Join(",", bookTicket.LastName.ToArray());
            string DOB = string.Join(",", bookTicket.DOB.ToArray());
            string DOJ = TempData["DOJ"].ToString();
            string currentUser = User.Identity.Name;
            int railwayNumber = (int) TempData["RailwayId"];
            string seatNumbers = string.Join(",", bookTicket.seatSelection.ToArray());

            string[] seatNumber = { };
            string[] seatStatus = { };
            IDictionary<string, string> seatsDictionary = new Dictionary<string, string>();

            var seats = _context.RailwaysSeatings.Where(s => s.RailwaysNumber.Equals(railwayNumber));

            if(bookTicket.TicketClass.Equals("Economy"))
            {
                var economySeat = seats.Select(s => s.EconomyClassSeatNumbers).Single();
                
                var economyStatus = seats.Select(s => s.EconomyClassSeatStatus).Single();
                seatNumber = economySeat.Split(",");
                seatStatus = economyStatus.Split(",");

                for (int i = 0; i < seatNumber.Length; i++)
                {
                    seatsDictionary.Add(seatNumber[i], seatStatus[i]);
                }
            }
            else
            {
                var firstSeat = seats.Select(s => s.FirstClassSeatNumbers).Single();
                var firstStatus = seats.Select(s => s.FirstClassSeatStatus).Single();
                seatNumber = firstSeat.Split(",");
                seatStatus = firstStatus.Split(",");

                for (int i = 0; i < seatNumber.Length; i++)
                {
                    seatsDictionary.Add(seatNumber[i], seatStatus[i]);
                }
            }

            foreach(var s in bookTicket.seatSelection.ToArray())
            {
                Console.WriteLine(s);
                seatsDictionary[s] = "X";
            }
            string[] updatedStatusArray = seatsDictionary.Values.ToArray();
            string updatedSeatStaus = string.Join(",", updatedStatusArray.ToArray());

            var railwaySeatings = _context.RailwaysSeatings;
            var result = railwaySeatings.SingleOrDefault(s => s.RailwaysNumber == railwayNumber);
            if (bookTicket.TicketClass.Equals("Economy"))
            {               
                if(result != null)
                {
                    result.EconomyClassSeatStatus = updatedSeatStaus;
                    _context.SaveChanges();
                }
            }
            else
            {
                if (result != null)
                {
                    result.FirstClassSeatStatus = updatedSeatStaus;
                    _context.SaveChanges();
                }
            }

            var reservarion = _context.ReservationInfos;
            if(reservarion != null)
            {
                var reservationInfo = new ReservationInfo
                {
                    RailwaysNumber = railwayNumber,
                    JourneyDate = DOJ,
                    BookingDate = DateTime.Now,
                    UserID = currentUser,
                    FirstNames = firstName,
                    LastNames = lastName,
                    DOBs = DOB,
                    SeatNumbers = seatNumbers
                };
                _context.ReservationInfos.Add(reservationInfo);
            }

            try
            {
                _context.SaveChanges();
            }
            catch(Exception)
            {
                return RedirectToAction("Error","Home");
            }


            return RedirectToAction("Index","Home");
        }


    }
