using Microsoft.Extensions.Configuration;
using PetSpaBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService.VnPayService
{
    public class VnPayService : IVnPayService
    {   private readonly IConfiguration configuration;
        public VnPayService(IConfiguration config)
        {
            configuration = config;
        }
        public string CreatePaymentURL(HttpContent content)
        {
            return null;
          /*  var tick = DateTime.Now.Ticks.ToString();  
            var pay = new VnPay*/
        }

        public VnPayResponseModel PaymentExecute(IQueryable<VnPayResponseModel> query)
        {
            throw new NotImplementedException();
        }
    }
}
