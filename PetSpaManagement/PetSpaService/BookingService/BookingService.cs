using PetSpaBussinessObject;
using PetSpaRepo.BookingRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService.BookingService
{
    public class BookingService : IBookingService
    {
        private IBookingRepo BookingRepo;

        public BookingService()
        {
            BookingRepo = new BookingRepo();
        }

        public void AddBooking(Booking booking)
        {
            if (booking.Id != default(int))
                throw new Exception("Invalid booking cannot be added");
            BookingRepo.AddBooking(booking);
        }

        public void DeleteBooking(int bookingId)
        {
            if (!(bookingId > 0))
                throw new Exception("Invalid Booking id");
            BookingRepo.SoftRemoveBooking(bookingId);
        }

        public Booking GetBooking(int bookingId)
        {
            return BookingRepo.GetBooking(bookingId);
        }

        public List<Booking> GetBookingList()
        {
            return BookingRepo.GetBookingList();
        }

		public List<Booking> GetActiveBookingList()
        {
            return BookingRepo.GetActiveBookingList();
        }

		public List<Booking> GetAccountBookingList()
		{
			return BookingRepo.GetActiveBookingList();
		}


		public void UpdateBooking(Booking booking)
        {
            if (booking == null || !(booking.Id > 0))
                throw new Exception("Invalid new Booking");
            BookingRepo.UpdateBooking(booking);
        }
    }
}
