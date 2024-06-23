using PetSpaBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService.BookingService
{
    public interface IBookingService
    {
        public Booking GetBooking(int bookingId);

        public List<Booking> GetBookingList();

        public void AddBooking(Booking booking);

        public void DeleteBooking(int BookingId);

        public void UpdateBooking(Booking booking);
    }
}
