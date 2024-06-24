using PetSpaBussinessObject;
using PetSpaDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo.BookingRepository
{
    public class BookingRepo : IBookingRepo
    {
        public void AddBooking(Booking Booking) => BookingDAO.Instance.AddBooking(Booking);

        public Booking GetBooking(int bookingId) => BookingDAO.Instance.GetBooking(bookingId);

        public List<Booking> GetBookingList() => BookingDAO.Instance.GetAllBooking();

        public List<Booking> GetActiveBookingList() => BookingDAO.Instance.GetActiveBooking();


		public void SoftRemoveBooking(int BookingId) => BookingDAO.Instance.SoftRemoveBooking(BookingId);

        public void UpdateBooking(Booking Booking) => BookingDAO.Instance.UpdateBooking(Booking);

    }
}
