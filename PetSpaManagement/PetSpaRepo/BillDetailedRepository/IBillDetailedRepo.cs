using PetSpaBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo.BillDetailedRepository
{
    public interface IBillDetailedRepo
    {
        public void AddBillDetailed(BillDetailed billDetailed);

        public void DeleteBillDetailed(int billDetailedId);

        public List<BillDetailed> GetBillDetailedList();

        public BillDetailed GetBillDetailed(int billDetailedId);
        public List<BillDetailed> GetBillDetailsByBillId(int id);

    }
}
