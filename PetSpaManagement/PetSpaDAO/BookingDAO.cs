using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaDAO
{
	public class BookingDAO
	{
		private readonly PetSpaManagementContext context = null;
		private static BookingDAO instance = null;

		public BookingDAO()
		{
			context = new PetSpaManagementContext();
		}

		public static BookingDAO Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new BookingDAO();
				}
				return instance;
			}
		}

		public List<Booking> GetAllBooking()
		{
			var bookings = context.Bookings.OrderByDescending(b => b.Started).ToList();
			if (bookings == null)
				throw new Exception("All bookings cannot be retrieved");
			return bookings;

		}

		public List<Booking> GetActiveBooking()
		{
			var bookings = context.Bookings.Where(b => b.Status == true).OrderByDescending(b => b.Started).ToList();
			if (bookings == null)
				throw new Exception("All bookings cannot be retrieved");
			return bookings;
		}
		public List<Booking> GetAccountBooking(int accountId)
		{
			var bookings = context.Bookings.Where(b => b.Status == true && b.AccountId == accountId).OrderByDescending(b => b.Started).ToList();
			if (bookings == null)
				throw new Exception("All bookings cannot be retrieved");
			return bookings;
		}
		public Booking GetBooking(int bookingId)
		{
			var booking = context.Bookings.FirstOrDefault(b => b.Id.Equals(bookingId));
			if (booking == null)
				throw new Exception("Booking cannot be retrieved");
			return booking;
		}
		public void AddBooking(Booking booking)
		{
			try
			{
				if (booking != null)
				{
					Booking existingBooking = GetBooking(booking.Id);
					if (existingBooking == null)
					{
						if (booking.Created == default || booking.Started == default || booking.Ended == default)
							throw new Exception("Booking has not been scheduled");

						if (booking.Created > DateTime.Now || booking.Started.Date <= DateTime.Today || booking.Ended.Date <= DateTime.Today || booking.Started >= booking.Ended)
							throw new Exception("Invalid booking date or time");

						context.Bookings.Add(booking);
						context.SaveChanges();
					}
				}
			}
			catch
			{
				Console.WriteLine("Booking cannot be added");
			}
		}

		public void UpdateBooking(Booking newBooking)
		{
			try
			{
				if (newBooking == null)
				{
					throw new ArgumentNullException(nameof(newBooking), "Booking cannot be null");
				}

				var existingBooking = context.Bookings.FirstOrDefault(s => s.Id == newBooking.Id);
				if (existingBooking == null)
				{
					throw new Exception("Booking does not exist");
				}
				context.Entry(existingBooking).CurrentValues.SetValues(newBooking);
				context.SaveChanges();
			}
			catch
			{
				Console.WriteLine("Booking cannot be updated");
			}
		}

		public void SoftRemoveBooking(int bookingId)
		{
			try
			{
				var booking = GetBooking(bookingId);
				if (booking == null)
					throw new Exception("Booking cannot be found");
				booking.Status = false;
				context.Update(booking);
				context.SaveChanges();
			}
			catch
			{
				Console.WriteLine("Booking cannot be removed");
			}
		}
	}
}
