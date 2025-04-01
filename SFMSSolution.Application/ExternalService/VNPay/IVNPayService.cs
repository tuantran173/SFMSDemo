using Microsoft.AspNetCore.Http;
using SFMSSolution.Application.DataTransferObjects.Payment;
using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.ExternalService.VNPay
{
    public interface IVNPayService
    {
        string CreatePaymentUrl(PaymentInfo model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
