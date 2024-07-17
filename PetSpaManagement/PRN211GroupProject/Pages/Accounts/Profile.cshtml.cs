using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using PetSpaBussinessObject;
using PetSpaRepo.VoucherRepository;
using PetSpaService.AccountService;
using PetSpaService.VoucherService.VoucherService;
using PRN211GroupProject.Utilities;
using PRN211GroupProject.ViewModel;
using System.Security.Claims;

namespace PRN211GroupProject.Pages.Accounts
{
	public class ProfileModel : PageModel
	{
		private readonly IAccountService accountService;
		private readonly IVoucherService voucherService;
		public ProfileModel(IAccountService account, IVoucherService voucher)
		{
			accountService = account;
			voucherService = voucher;
			ProfileViewModel = new();
			ChangePasswordViewModel = new();
			errorMessage = String.Empty;
			successMessage = String.Empty;
		}
		[BindProperty]
		public ProfileViewModel ProfileViewModel { get; set; }
		[BindProperty]
		public ChangePasswordViewModel ChangePasswordViewModel { get; set; }
		public Account? Account { get; set; }
		[TempData]
		public string errorMessage { get; set; }
		[TempData]
		public string successMessage { get; set; }
		public List<Voucher>? VoucherList { get; set; }

		public IActionResult OnGet()
		{
			try
			{
				Account = AccountUtilities.Instance.GetAccount(HttpContext, accountService);
				if (Account == null)
				{
					errorMessage = "You must login first";
					return RedirectToPage("/Accounts/Login");
				}
				if (voucherService == null || accountService == null)
				{
					return BadRequest();
				}
				if (Account.VoucherId != null && Account.VoucherId > 0)
				{
					var voucher = voucherService.GetVoucher((int)Account.VoucherId);
					if (voucher != null)
					{
						if (voucher.Expired <= DateTime.Now)
						{
							Account.VoucherId = null;
							accountService.UpdateAccount(Account);
						}
						else
						{
							Account.Voucher = voucher;
						}
					}
				}
				VoucherList = voucherService.GetActiveVoucherList();
				return Page();
			}
			catch
			{
				return BadRequest();
			}
		}
		public IActionResult OnPostSaveProfile()
		{
			try
			{
				Account = AccountUtilities.Instance.GetAccount(HttpContext, accountService);
				if (Account == null)
				{
					errorMessage = "You must login first";
					return RedirectToPage("/Accounts/Login");
				}

				if (ProfileViewModel == null)
				{
					return BadRequest();
				}
				ProfileViewModel.Email = ProfileViewModel.Email.Trim();
				if (Account.Email.Trim() == ProfileViewModel.Email)
				{
					if (accountService.GetAccountByEmail(ProfileViewModel.Email) != null)
					{
						ModelState.AddModelError("ProfileViewModel.Email", "Email already exists.");
						return Page();
					}
				}
				Account.Email = ProfileViewModel.Email.Trim();
				Account.Name = FormatUtilities.TrimSpacesPreserveSingle(ProfileViewModel.Name);
				Account.Phone = ProfileViewModel.Phone;
				Account.Status = true;
				accountService.UpdateAccount(Account);
				successMessage = "The profile is successfully updated.";
				return Page();
			}
			catch
			{
				return BadRequest();
			}
		}
		public IActionResult OnPostChangePassword()
		{
			try
			{
				Account = AccountUtilities.Instance.GetAccount(HttpContext, accountService);
				if (Account == null || String.IsNullOrEmpty(Account.Pass))
				{
					errorMessage = "You must login first";
					return RedirectToPage("/Accounts/Login");
				}
				if (ChangePasswordViewModel == null)
				{
					return BadRequest();
				}
				if (!accountService.VerifyPassword(ChangePasswordViewModel.OldPass, Account.Pass))
				{
					errorMessage = "Your password is incorrect";
					return Page();
				}
				else if (accountService.VerifyPassword(ChangePasswordViewModel.NewPass, Account.Pass))
				{
					errorMessage = "New password cannot be the same as the old password.";
					return Page();
				}
				Account.Pass = ChangePasswordViewModel.NewPass;
				accountService.UpdateAccount(Account);
				Account.Status = true;
				successMessage = "The password has successfully been changed.";
				return Page();
			}
			catch
			{
				return BadRequest();
			}

		}

		public IActionResult OnPostApplyVoucher(int? voucherId)
		{
			try
			{
				Account = AccountUtilities.Instance.GetAccount(HttpContext, accountService);
				if (Account == null)
				{
					errorMessage = "You must login first";
					return RedirectToPage("/Accounts/Login");
				}
				if (Account.RoleId == 1 || Account.RoleId == 3)
				{
					errorMessage = "You are not authorized for a voucher.";
					return OnGet();
				}
				if (Account.CountVoucher <= 0)
				{
					errorMessage = "You are ineligible for a voucher.";
					return OnGet();
				}
				if (voucherId == null || voucherId <= 0)
				{
					errorMessage = "Finding the voucher to be applied is impossible.";
					return OnGet();
				}
				var voucher = voucherService.GetVoucher((int)voucherId);
				if (voucher == null)
				{
					errorMessage = "The voucher to be applied cannot be found.";
					return OnGet();
				}
				if (voucher.Expired <= DateTime.Now)
				{
					Account.VoucherId = null;
					accountService.UpdateAccount(Account);
					errorMessage = "The voucher to be applied has already expired.";
					return OnGet();
				}
				if (Account.CountVoucher < voucher.Reach)
				{
					errorMessage = "You are not currently eligible for this voucher.";
					return OnGet();
				}
				Account.CountVoucher -= voucher.Reach;
				Account.Voucher = null;
				Account.VoucherId = voucher.Id;
				accountService.UpdateAccount(Account);
				successMessage = "You got the voucher!";
				return OnGet();
			}
			catch
			{
				return BadRequest();
			}
		}

	}
}
