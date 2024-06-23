using PetSpaBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo.BookingRepository
{
    public interface IBookingRepo
    {
        public void AddBooking(Booking booking);

        public void SoftRemoveBooking(int bookingId);

        public List<Booking> GetBookingList();

        public void UpdateBooking(Booking booking);

        public Booking GetBooking(int bookingId);
    }
}
