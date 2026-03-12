namespace CinemaBooking.Models
{
    public class CartItem
    {
        public long MovieID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Seat { get; set; } = string.Empty;
        public decimal TicketPrice { get; set; }
    }

    public class BookingCart
    {
        public List<CartItem> Items { get; set; } = new();
    }
}
