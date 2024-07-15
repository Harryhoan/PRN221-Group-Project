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

		public List<Booking> GetActiveBookingList();

		public List<Booking> GetAccountBookingList(int accountId);

		public List<Booking> GetActiveBookingListBySpot(int spotId);

		public void AddBooking(Booking booking);

		public void DeleteBooking(int BookingId);

		public void UpdateBooking(Booking booking);

		public List<Booking> GetWeeklyBooking(DateTime week, List<Booking> bookings);

		public bool IsActiveBookingConflictBySpot(DateTime started, DateTime ended, int spotId);
		public int NumberOfBooking();

    }
}
