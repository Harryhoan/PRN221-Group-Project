using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PetSpaDAO
{
    public class BillDAO
    {
        private readonly PetSpaManagementContext context = null;
        private static BillDAO instance = null;
         
        public BillDAO()
        {
            context = new PetSpaManagementContext();
        }

        public static BillDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillDAO();
                }
                return instance;
            }
        }

        public List<Bill> GetAllBill()
        {
            var Bills = context.Bills.Include(b => b.Acc).Include(b => b.Voucher).ToList();
            if (Bills == null)
                throw new Exception("All Bills cannot be retrieved");
            return Bills;
        }

        public List<Bill> GetAllBillCreatedThisYear()
        {
            var Bills = context.Bills.AsNoTracking().Where(b => b.Created.Year == DateTime.Now.Year).Include(b => b.Acc).Include(b => b.Voucher).OrderByDescending(b => b.Created).ToList();
            if (Bills == null)
                throw new Exception("All Bills cannot be retrieved");
            return Bills;

        }

        public List<Bill> GetAccountBill(int accId)
        {
            var Bills = context.Bills.Where(b => b.AccId == accId).Include(b => b.Voucher).ToList();
            if (Bills == null)
                throw new Exception("All Bills cannot be retrieved");
            return Bills;
        }

        public Bill GetBill(int billId)
        {
            var bill = context.Bills.FirstOrDefault(b => b.Id.Equals(billId));
            if (bill == null)
                throw new Exception("Bill cannot be retrieved");
            return bill;
        }
        public void AddBill(Bill bill)
        {
            try
            {
                if (bill != null)
                {

                    if (bill.Started == default || bill.Started <= DateTime.Now || bill.Created > DateTime.Now)
                        throw new Exception("Invalid bill date or time");

                    if (bill.Total <= 0)
                        throw new Exception("Invalid bill total");
                    context.Entry(bill).State = EntityState.Added;
                    context.Bills.Add(bill);
                    context.SaveChanges();

                }
            }
            catch
            {
                Console.WriteLine("Bill cannot be added");
            }
        }
        //public void UpdateBill(Bill newBill)
        //{
        //	try
        //	{
        //		if (newBill == null)
        //		{
        //			throw new ArgumentNullException(nameof(newBill), "Bill cannot be null");
        //		}

        //		var existingBill = context.Bills.FirstOrDefault(b => b.Id == newBill.Id);
        //		if (existingBill == null)
        //		{
        //			throw new Exception("Bill does not exist");
        //		}
        //		context.Entry(existingBill).CurrentValues.SetValues(newBill);
        //		context.SaveChanges();
        //	}
        //	catch
        //	{
        //		Console.WriteLine("Bill cannot be updated");
        //	}
        //}
        public List<Bill> GetFilterdAccountBill(DateTime fromDate, DateTime toDate, int accountId)
        {
            var filteredBills = context.Bills
                .Where(b => b.AccId == accountId && b.Created >= fromDate && b.Created <= toDate)
                .OrderBy(b => b.Created)
                .ToList();
            return filteredBills;
        }
        public List<Bill> GetFilteredBill(DateTime fromDate, DateTime toDate)
        {
            List<Bill> allBills = GetAllBill();
            return allBills.Where(o => o.Created >= fromDate && o.Created <= toDate).ToList();
        }

        public void DeleteBill(int billId)
        {
            try
            {
                var existingBill = context.Bills.Include(b => b.BillDetaileds).SingleOrDefault(b => b.Id == billId) ?? throw new Exception("Bill cannot be found");
                foreach (var bd in existingBill.BillDetaileds)
                {
                    context.BillDetaileds.Remove(bd);
                }
                context.Remove(billId);
                context.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Bill cannot be deleted");
            }
        }
    }
}
