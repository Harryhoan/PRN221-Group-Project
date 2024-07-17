using PetSpaBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService.MailService
{
    public interface ISendMailService
    {
        bool SendMail(MailData mailData);


    }
}
