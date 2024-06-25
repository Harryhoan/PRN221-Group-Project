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

		public List<Booking> GetAccountBookingList(int accountId)
		{
			return BookingRepo.GetAccountBookingList(accountId);
		}


		public void UpdateBooking(Booking booking)
        {
            if (booking == null || !(booking.Id > 0))
                throw new Exception("Invalid new Booking");
            BookingRepo.UpdateBooking(booking);
        }

        public async Task<List<Booking>> GetWeeklyBooking(DateTime week, List<Booking> bookings)
        {
            if (bookings == null ||  !(bookings.Count > 0))
                throw new Exception("Invalid weekly Booking");
            return await Task.Run(() => bookings.Where(b => Math.Abs((b.Started.Date - week.Date).TotalDays) < 7).ToList());
        }
    }
}
