using PetSpaBussinessObject;
using PetSpaRepo.BillDetailedRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService.BillDetailedService
{
    public class BillDetailedService : IBillDetailedService
    {
        private IBillDetailedRepo billDetailedRepo;
        public BillDetailedService()
        {
            billDetailedRepo = new BillDetailedRepo();
        }

        public BillDetailed GetBillDetailed(int billDetailedId)
        {
            return billDetailedRepo.GetBillDetailed(billDetailedId);
        }

        public List<BillDetailed> GetBillDetailedList()
        {
            return billDetailedRepo.GetBillDetailedList();
        }
        public List<BillDetailed> GetBillDetailsByBillId(int id)
        {
          return   billDetailedRepo.GetBillDetailsByBillId(id);

        }
        public void AddBillDetailed(BillDetailed billDetailed)
        {
            if (billDetailed == null || billDetailed.Id != default)
                throw new Exception("Invalid billDetailed");
            billDetailedRepo.AddBillDetailed(billDetailed);
        }

        public void DeleteBillDetailed(int billDetailedId)
        {
            if (!(billDetailedId > 0))
                throw new Exception("Invalid billDetailed id");
            billDetailedRepo.DeleteBillDetailed(billDetailedId);
        }
    }
}
