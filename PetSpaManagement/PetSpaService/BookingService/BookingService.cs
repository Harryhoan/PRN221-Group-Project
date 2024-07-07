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
            if (booking.Id != default)
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

        public List<Booking> GetActiveBookingListBySpot(int spotId)
        {
            return BookingRepo.GetActiveBookingListBySpot(spotId);
        }

        public void UpdateBooking(Booking booking)
        {
            if (booking == null || !(booking.Id > 0))
                throw new Exception("Invalid new Booking");
            BookingRepo.UpdateBooking(booking);
        }

        public List<Booking> GetWeeklyBooking(DateTime week, List<Booking> bookings)
        {
            if (bookings == null)
                throw new Exception("Invalid weekly Booking");
            if (bookings.Count == 0)
                return bookings;
            return  bookings.Where(b => IsInSameWeek(b.Started.Date, week.Date)).ToList();
        }

		public bool IsActiveBookingConflictBySpot(DateTime started, DateTime ended, int spotId)
		{
            if (started >= ended)
                throw new Exception("Invalid booking datetime");
            return BookingRepo.IsActiveBookingConflictBySpot(started, ended, spotId);
        }

        public static bool IsInSameWeek(DateTime date1, DateTime date2)
        {
            // First, find the first day of the week for both dates
            DateTime startOfWeek1 = date1.Date.AddDays(-(int)date1.DayOfWeek);
            DateTime startOfWeek2 = date2.Date.AddDays(-(int)date2.DayOfWeek);

            // Dates are in the same week if the starting days of their weeks are the same
            return startOfWeek1 == startOfWeek2;
        }

    }
}
