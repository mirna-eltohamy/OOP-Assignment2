#region p1q1
//a
//public fields allow external modifications & no proper validation in withdraw method (to check balance availability)

//b
//make fields private & provide setters and getters instead
//validate whether balance is sufficient before allowing withdrawal

//c
//because public fields allow uncontrolled modifications which can lead to invalid state of object
#endregion

#region p1q2
//field member is variable that holds data
//property is member that provides access to private field to read or write or even validate
//yes, a property can contain logic
//public int salary { get { return initialSalary + bonus; } }
#endregion

#region p1q3
//a)Indexer, allows indexed access to object like an array - obj[i] 
//b) throws indexOutOfRangeException 
//to make safe:
//public string this[int index]
//{
//    get
//    {
//        if (index < 0 || index >= names.Length)
//            throw new ArgumentOutOfRangeException(nameof(index));
//        return names[index];
//    }
//    set
//    {
//        if (index < 0 || index >= names.Length)
//            throw new ArgumentOutOfRangeException(nameof(index));
//        names[index] = value;
//    }
//}
//c)yes, indexers can be overloaded with different parameter types and counts
//for example: public int this[string name] &  public string this[int index]
//indexer allows access using name & other using index
#endregion

#region p1q4
//a) static TotalOrders: class member that is shared among all its instances
//   ,while item: is instance member that is unique to each object of the class

//b) No, static methods cannot access instance members directly (Item) because they do not belong to any specific instance of the class.
//   static methods can only access static members of the class.
#endregion

#region p2
//Movie Ticket Booking System
using System;
namespace MovieTicketBookingSystem
{
    public enum TicketType
    {
        Standard,
        VIP,
        IMAX
    }

    
    public struct SeatLocation
    {
        public char Row { get; set; }
        public int Number { get; set; }

        public SeatLocation(char row, int number)
        {
            Row = row;
            Number = number;
        }

        public override string ToString()
        {
            return $"{Row}-{Number}";
        }
    }

    ///////////////////////////////////////


    public class Ticket
    {
        private string _movieName;
        private double _price;

        private static int ticketCounter = 0;

        public int TicketId { get; }

        public TicketType Type { get; set; }
        public SeatLocation Seat { get; set; }

        public string MovieName
        {
            get { return _movieName; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _movieName = value;
            }
        }

        public double Price
        {
            get { return _price; }
            set
            {
                if (value > 0)
                    _price = value;
            }
        }

        public double PriceAfterTax
        {
            get { return _price * 1.14; }
        }

        public Ticket(string movieName, TicketType type, SeatLocation seat, double price)
        {
            ticketCounter++;
            TicketId = ticketCounter;

            Type = type;
            Seat = seat;

            MovieName = movieName;
            Price = price;
        }

        public static int GetTotalTicketsSold()
        {
            return ticketCounter;
        }
    }

    ///////////////////////////////////////

    public class Cinema
    {
        private Ticket[] tickets = new Ticket[20];

        public Ticket this[int index]
        {
            get
            {
                if (index < 0 || index >= tickets.Length)
                    return null;

                return tickets[index];
            }
            set
            {
                if (index < 0 || index >= tickets.Length)
                    return;

                tickets[index] = value;
            }
        }

        public Ticket GetMovie(string movieName)
        {
            foreach (var ticket in tickets)
            {
                if (ticket != null && ticket.MovieName.Equals(movieName, StringComparison.OrdinalIgnoreCase))
                    return ticket;
            }
            return null;
        }

        public bool AddTicket(Ticket t)
        {
            for (int i = 0; i < tickets.Length; i++)
            {
                if (tickets[i] == null)
                {
                    tickets[i] = t;
                    return true;
                }
            }
            return false;
        }
    }

///////////////////////////////////////
    public static class BookingHelper
    {
        private static int bookingCounter = 0;

        public static double CalcGroupDiscount(int numberOfTickets, double pricePerTicket)
        {
            double total = numberOfTickets * pricePerTicket;

            if (numberOfTickets >= 5)
                return total * 0.9; // 10% discount

            return total;
        }

        public static string GenerateBookingReference()
        {
            bookingCounter++;
            return $"BK-{bookingCounter}";
        }
    }

/////////////////////////////////////////
    class Program
    {
        static void Main()
        {
            Console.WriteLine("========== Ticket Booking ==========\n");

            Cinema cinema = new Cinema();

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Enter data for Ticket {i + 1}:");

                Console.Write("Movie Name: ");
                string movieName = Console.ReadLine();

                Console.Write("Ticket Type (0=Standard, 1=VIP, 2=IMAX): ");
                TicketType type = (TicketType)int.Parse(Console.ReadLine());

                Console.Write("Seat Row (A-Z): ");
                char row = char.Parse(Console.ReadLine());

                Console.Write("Seat Number: ");
                int number = int.Parse(Console.ReadLine());

                Console.Write("Price: ");
                double price = double.Parse(Console.ReadLine());

                SeatLocation seat = new SeatLocation(row, number);
                Ticket ticket = new Ticket(movieName, type, seat, price);

                cinema.AddTicket(ticket);
                Console.WriteLine();
            }

            Console.WriteLine("========== All Tickets ==========\n");

            for (int i = 0; i < 3; i++)
            {
                var t = cinema[i];
                if (t != null)
                {
                    Console.WriteLine(
                        $"Ticket #{t.TicketId} | {t.MovieName} | {t.Type} | " +
                        $"Seat: {t.Seat} | Price: {t.Price} EGP | After Tax: {t.PriceAfterTax} EGP");
                }
            }

            Console.WriteLine("\n========== Search by Movie ==========");

            Console.Write("Enter movie name to search: ");
            string search = Console.ReadLine();

            Ticket found = cinema.GetMovie(search);

            if (found != null)
            {
                Console.WriteLine(
                    $"Found: Ticket #{found.TicketId} | {found.MovieName} | {found.Type} | " +
                    $"Seat: {found.Seat} | Price: {found.Price} EGP");
            }
            else
            {
                Console.WriteLine("Movie not found.");
            }

            Console.WriteLine($"\nTotal Tickets Sold: {Ticket.GetTotalTicketsSold()}");

            Console.WriteLine("\nBooking References:");
            Console.WriteLine(BookingHelper.GenerateBookingReference());
            Console.WriteLine(BookingHelper.GenerateBookingReference());

            Console.WriteLine("\nGroup Discount (5 tickets, 80 EGP each):");
            double discounted = BookingHelper.CalcGroupDiscount(5, 80);
            Console.WriteLine($"Total after discount: {discounted} EGP");
        }
    }
}

#endregion
