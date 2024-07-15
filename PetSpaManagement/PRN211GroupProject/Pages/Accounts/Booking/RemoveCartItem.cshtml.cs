using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Linq.Expressions;

namespace PRN211GroupProject.Pages.Accounts.Booking
{
	public class RemoveCartItemModel : PageModel
	{
		public IActionResult OnPost(int bookingIndex)
		{
			try
			{
				if (bookingIndex >= 0)
				{
					var bookingCartBytes = HttpContext.Session.Get("BookingCart");
					if (bookingCartBytes != null && bookingCartBytes.Length > 0)
					{
						JsonSerializerOptions options = new JsonSerializerOptions
						{
							ReferenceHandler = ReferenceHandler.Preserve,
							WriteIndented = true
						};
						var bookingCart = JsonSerializer.Deserialize<List<PetSpaBussinessObject.Booking>>(bookingCartBytes, options)?.ToList();
						if (bookingCart != null && bookingCart.Count > 0)
						{
							bookingCart.RemoveAt(bookingIndex);
							HttpContext.Session.Set("BookingCart",JsonSerializer.SerializeToUtf8Bytes(bookingCart, options));
							HttpContext.Session.SetInt32("BookingCount", bookingCart.Count);
						}

					}
				}
				return RedirectToPage("/Index");
			}
			catch
			{
				return RedirectToPage("/Index");
			}
		}
	}
}
