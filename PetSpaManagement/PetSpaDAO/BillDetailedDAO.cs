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
	public class BillDetailedDAO
	{
		private readonly PetSpaManagementContext context = null;
		private static BillDetailedDAO instance = null;

		public BillDetailedDAO()
		{
			context = new PetSpaManagementContext();
		}

		public static BillDetailedDAO Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new BillDetailedDAO();
				}
				return instance;
			}
		}

		public List<BillDetailed> GetAllBillDetailed()
		{
			var BillDetaileds = context.BillDetaileds.ToList();
			if (BillDetaileds == null)
				throw new Exception("All BillDetaileds cannot be retrieved");
			return BillDetaileds;

		}

		public BillDetailed GetBillDetailed(int billDetailedId)
		{
			var billDetailed = context.BillDetaileds.FirstOrDefault(b => b.Id.Equals(billDetailedId));
			if (billDetailed == null)
				throw new Exception("BillDetailed cannot be retrieved");
			return billDetailed;
		}
		public void AddBillDetailed(BillDetailed billDetailed)
		{
			try
			{
				BillDetailed existingBillDetailed = GetBillDetailed(billDetailed.Id);
				if (existingBillDetailed == null && billDetailed != null)
				{
					if (billDetailed.Cost <= 0)
						throw new Exception("Invalid billDetailed cost");
					context.BillDetaileds.Add(billDetailed);
					context.SaveChanges();
				}
			}
			catch
			{
				Console.WriteLine("BillDetailed cannot be added");
			}
		}

		public void DeleteBillDetailed(int billDetailedId)
		{
			try
			{
				var existingBillDetailed = GetBillDetailed(billDetailedId) ?? throw new Exception("BillDetailed cannot be found");
				context.Remove(billDetailedId);
				context.SaveChanges();
			}
			catch
			{
				Console.WriteLine("BillDetailed cannot be deleted");
			}
		}
	}
}
