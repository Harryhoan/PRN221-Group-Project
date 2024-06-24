using PetSpaBussinessObject;
using PetSpaDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo.BillDetailedRepository
{
    public class BillDetailedRepo : IBillDetailedRepo
    {
        public void AddBillDetailed(BillDetailed billDetailed) => BillDetailedDAO.Instance.AddBillDetailed(billDetailed);

        public void DeleteBillDetailed(int billDetailedId) => BillDetailedDAO.Instance.DeleteBillDetailed(billDetailedId);

        public List<BillDetailed> GetBillDetailedList() => BillDetailedDAO.Instance.GetAllBillDetailed();

        public BillDetailed GetBillDetailed(int billDetailedId) => BillDetailedDAO.Instance.GetBillDetailed(billDetailedId);
    }
}
