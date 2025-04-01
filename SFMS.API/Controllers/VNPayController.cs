//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using SFMSSolution.Application.ExternalService.VNPay;
//using SFMSSolution.Domain.Entities;

//namespace SFMSSolution.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class VNPayController : ControllerBase
//    {
//        private readonly IVNPayService _vnPayService;

//        public VNPayController(IVNPayService vnPayService)
//        {
//            _vnPayService = vnPayService;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult CreatePaymentUrl(PaymentInfo model)
//        {
//            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

//            return Redirect(url);
//        }

//        public IActionResult PaymentCallback()
//        {
//            var response = _vnPayService.PaymentExecute(Request.Query);

//            return Json(response);
//        }
//    }
//}
