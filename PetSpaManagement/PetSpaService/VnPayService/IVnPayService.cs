using PetSpaBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService.VnPayService
{
    public interface IVnPayService
    {
        string CreatePaymentURL(HttpContent content);
        VnPayResponseModel PaymentExecute(IQueryable<VnPayResponseModel> query);
    }
}
