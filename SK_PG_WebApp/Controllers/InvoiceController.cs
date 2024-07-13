using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SK_PG_WebApp.DAL;
using SK_PG_WebApp.Helper;
using SK_PG_WebApp.Models.BusinessModels;
using SK_PG_WebApp.Models.DynamicModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SK_PG_WebApp.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly ILogger<InvoiceController> _logger;
        private readonly IConfiguration config;
        private readonly DatabaseContext dbContext;

        public InvoiceController(ILogger<InvoiceController> logger, DatabaseContext databaseContext, IConfiguration _config)
        {
            _logger = logger;
            dbContext = databaseContext;
            config = _config;
        }

        public IActionResult AddPayment(string name, string ddlPG, string ddlPT)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));
            ViewBag.userId = userId;
            ViewBag.payingGuest = string.Empty;
            ViewBag.PayTo = string.Empty;
            ViewBag.PayToId = string.Empty;
            ViewBag.PayForRoom = string.Empty;
            ViewBag.Amount = string.Empty;
            ViewBag.Discount = string.Empty;
            ViewBag.payentTypeLabel = string.Empty;
            ViewBag.ddlPG = ddlPG;
            ViewBag.ddlPT = ddlPT;
            ViewBag.ItemList = new List<AddPaymentSuccessDC>();
            if (ddlPG.IsNullOrEmpty())
            {
                ViewBag.Second = "none";
                ViewBag.First = "flex";
                ViewBag.Third = "none";
            }
            else
            {
                ViewBag.Second = "flex";
                ViewBag.First = "none";
                ViewBag.Third = "none";
                Hashtable hash = new Hashtable();
                hash.Add("@userId", ddlPG);
                var pgData = new ManualDbContext(config).GetDataTable(StoredProcedure.USP_PG_Rooms, hash);

                if (pgData.IsNull())
                {
                    
                }
                if(pgData.Rows.Count == 0)
                {

                }
                else
                {
                    StringBuilder str = new StringBuilder("Bed No : 101, ");
                    str.Append(Convert.ToString(pgData.Rows[0]["BlockDetails"]));
                    str.Append(", ");
                    str.Append(Convert.ToString(pgData.Rows[0]["PGName"]));
                    str.Append(", ");
                    str.Append(Convert.ToString(pgData.Rows[0]["location"]));
                    str.Append(", ");
                    str.Append(Convert.ToString(pgData.Rows[0]["city"]));
                    str.Append(", ");
                    str.Append(Convert.ToString(pgData.Rows[0]["state"]));
                    str.Append(", ");
                    str.Append(Convert.ToString(pgData.Rows[0]["country"]));
                    str.Append(". ");

                    ViewBag.payingGuest = Convert.ToString(pgData.Rows[0]["userName"]);
                    ViewBag.PayTo = Convert.ToString(pgData.Rows[0]["payTo"]);
                    ViewBag.PayForRoom = str.ToString();
                    ViewBag.Amount = (ddlPT == Convert.ToString((int)PAYMENT_TYPE.Deposit) ? Convert.ToString(pgData.Rows[0]["depositAmount"])
                                        : (ddlPT == Convert.ToString((int)PAYMENT_TYPE.Rent) ? Convert.ToString(pgData.Rows[0]["rentAmount"]) : "0.00"));
                    ViewBag.Discount = (ddlPT == Convert.ToString((int)PAYMENT_TYPE.Deposit) ? Convert.ToString(pgData.Rows[0]["Depositdiscount"])
                                        : (ddlPT == Convert.ToString((int)PAYMENT_TYPE.Rent) ? Convert.ToString(pgData.Rows[0]["Rentdiscount"]) : "0.00"));
                    
                    ViewBag.payentTypeLabel = Convert.ToString(((PAYMENT_TYPE)Convert.ToInt32(ddlPT)));
                    ViewBag.PayToId = Convert.ToString(pgData.Rows[0]["payToId"]);
                }

                
            }



            ViewBag.PG = new MasterDropdowns(dbContext, config).PayingGuest(userId);
                ViewBag.PaymentGateway = new MasterDropdowns(dbContext, config).PaymentGateway();
                ViewBag.PaymentType = new MasterDropdowns(dbContext, config).PaymentTypeMaster();

            return View();
        }

        [HttpPost]
        public IActionResult AddPayment(string ddlPT, string ddlPG, string ddlPayGateway,string discount
            ,string amount, string dateRange, string payOn, string payingGuest, string PayTo
            , string payentType, string payGatewayDetails, string payToId)
        {
            string[] formats = { "MM-dd-yyyy" };
            var dateTime = DateTime.ParseExact(
                Convert.ToString(payOn.Substring(0, payOn.IndexOf(" ", StringComparison.Ordinal))).Replace("/", "-")
                , formats, new CultureInfo("en-US"), DateTimeStyles.None);
            //var t = Convert.ToDateTime(Convert.ToString("08-28-2022").ToString("MM-dd-yyyy"));
            ViewBag.Second = "none";
            ViewBag.First = "none";
            ViewBag.Third = "flex";
            var userId = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));
            ViewBag.userId = Convert.ToInt64(ddlPG);
           
            var pgMaster = dbContext.PayingGuestMasterDbSet.Where(x => x.userId == Convert.ToInt64(ddlPG)).FirstOrDefault();
            
            PayingGuestPaymentBO objPM = new PayingGuestPaymentBO()
            {
                amount = Convert.ToDecimal(amount),
                isActive = true,
                discount = Convert.ToDecimal(discount),
                isInvoiceGenerated = false,
                paymentDate = dateTime, // Convert.ToDateTime(payOn),
                paymentDateRange = dateRange,
                paymentDescription = payGatewayDetails,
                paymentTypeId = Convert.ToInt64(ddlPT),
                payToUserId = Convert.ToInt64(payToId),
                pgMasterId  = pgMaster.id,
                pgRoomMasterId = pgMaster.pgRoomMasterId,
                userId = Convert.ToInt64(ddlPG)
            };

            dbContext.PayingGuestPaymentDbSet.Add(objPM).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            dbContext.SaveChanges();

            //Hashtable hash = new Hashtable();
            //hash.Add("@ipuserId",ddlPG);
            //hash.Add("@ippayToUserId",payToId);
            //hash.Add("@ipamount",amount);
            //hash.Add("@ipdiscount",discount);
            //hash.Add("@ipdateRange",dateRange);
            //hash.Add("@ippaymentDate",payOn);
            //hash.Add("@ippaymnetTypeId",  ddlPT);
            //hash.Add("@ippaymentDetails", payGatewayDetails);
            //hash.Add("@ippaymentGatewayId", ddlPayGateway);
            //hash.Add("@ippaymentTypeDescription", payentType);

            //var data = new ManualDbContext(config).GetDataTable(StoredProcedure.USP_PG_PAYMENT, hash);

            //if (data.IsNull())
            //{

            //}
            //if(data.Rows.Count <= 0)
            //{

            //}


            var data = (from payingguest in dbContext.PayingGuestPaymentDbSet.Where(x=>x.isInvoiceGenerated == false && x.userId == Convert.ToInt64(ddlPG))
                        join users in dbContext.UsersDbSet on payingguest.userId equals users.id
                        join reporting in dbContext.UsersDbSet on payingguest.payToUserId equals reporting.id
                        select new AddPaymentSuccessDC
                        {
                            amount = Convert.ToString(payingguest.amount),
                            discount = Convert.ToString(payingguest.discount),
                            fromUserName = users.name,
                            dateRange = payingguest.paymentDateRange,
                            paymentDetails = payingguest.paymentDescription,
                            paymentGateway = Convert.ToString(((PAYMENT_GATEWAY)Convert.ToInt32(ddlPG))),
                            paymentType = Convert.ToString(((PAYMENT_TYPE)Convert.ToInt32(payingguest.paymentTypeId))) ,
                            toUsername = reporting.name
                        }).ToList();

            List<AddPaymentSuccessDC> obj = new List<AddPaymentSuccessDC>();
            obj = data;

            //foreach(DataRow row in data.Rows)
            //{
            //    obj.Add(new AddPaymentSuccessDC()
            //    {
            //        fromUserName = Convert.ToString(row["fromUserName"]),
            //        amount = Convert.ToString(row["amount"]),
            //        dateRange = Convert.ToString(row["paymentDateRange"]),
            //        discount = Convert.ToString(row["discount"]),
            //        paymentDetails = Convert.ToString(row["paymentDescription"]),
            //        paymentGateway = Convert.ToString(((PAYMENT_GATEWAY)Convert.ToInt32(row["paymentGatewayId"]))),
            //        paymentType = Convert.ToString(((PAYMENT_TYPE)Convert.ToInt32(row["paymentTypeId"]))),
            //        toUsername = Convert.ToString(row["toUserName"])
            //    });
            //}

            ViewBag.ItemList = obj;
            ViewBag.PayToId = string.Empty;
            ViewBag.payingGuest = payingGuest;
            ViewBag.PayTo = PayTo;
            ViewBag.PayForRoom = dateRange;
            ViewBag.Amount = amount;
            ViewBag.Discount = discount;
            ViewBag.payentTypeLabel = payentType;
            ViewBag.ddlPG = ddlPayGateway;
            ViewBag.ddlPT = payGatewayDetails;
            
            ViewBag.PG = new MasterDropdowns(dbContext, config).PayingGuest(userId);
            ViewBag.PaymentGateway = new MasterDropdowns(dbContext, config).PaymentGateway();
            ViewBag.PaymentType = new MasterDropdowns(dbContext, config).PaymentTypeMaster();

            return View();
        }

        [HttpPost]
        public JsonResult GetPGData(int pgId)
        {
            Hashtable hash = new Hashtable();
            hash.Add("@userId", pgId);
            var pgData = new ManualDbContext(config).GetDataTable(StoredProcedure.USP_PG_Rooms, hash);

            return Json(pgData);
        }

        public IActionResult GenerateInvoice(string userId = "4")
        {
            Hashtable hash = new Hashtable();
            hash.Add("ipuserId", userId);
            var data = new ManualDbContext(config).GetDataSet(StoredProcedure.USP_GENERATE_INVOICE, hash);

            if (data.IsNull())
            {

            }
            if (data.Tables[0].Rows.Count <= 0)
            {

            }


            List<PrintInvoiceDC> list = new List<PrintInvoiceDC>();
            PrintInvoiceDC obj = new PrintInvoiceDC();
            foreach (DataRow row in data.Tables[0].Rows)
            {
                obj.fromUser = Convert.ToString(row["userName"]);
                obj.fromEmail = Convert.ToString(row["userEmail"]);
                obj.fromAddress = Convert.ToString(row["userAddress"]);
                obj.fromPhone = Convert.ToString(row["userNumber"]);
                obj.toUser = Convert.ToString(HttpContext.User.FindFirstValue("UserName")); ;
                obj.toEmail = Convert.ToString(HttpContext.User.FindFirstValue("EmailAddress")); ;
                obj.toAddress = String.Empty;
                obj.toPhone = Convert.ToString(HttpContext.User.FindFirstValue("mobile")); ;
                obj.invoiceNumber = Convert.ToString(row["invoiceNumber"]);
            }
            foreach (DataRow row in data.Tables[1].Rows)
            {
                obj.totalAmount = Convert.ToString(row["totalAmount"]);
                obj.totalDiscount = Convert.ToString(row["totalDiscount"]);
                obj.tax = Convert.ToString(row["tax"]);
                obj.totalPayable = Convert.ToString(row["totalPayable"]);
               
            }

            foreach (DataRow row in data.Tables[2].Rows)
            {
                obj.data.Add(new PrintInvoiceDataDC()
                {
                    transactionId = Convert.ToString(row["transactionId"]),
                    amount = Convert.ToString(row["amount"]),
                    no = Convert.ToString(row["srno"]),
                    dateRange = Convert.ToString(row["dateRange"]),
                    discount = Convert.ToString(row["discount"]),
                    paymentGateway = Convert.ToString(row["paymentGateway"]),
                    paymentType = Convert.ToString(row["paymentType"])

                });
                

            }
            list.Add(obj);

            return View(list);
        }

        public IActionResult UserPaymentList(string id)
        {
            Hashtable hash = new Hashtable();
            hash.Add("ipUserId", id);
            var data = new ManualDbContext(config).GetDataTable(StoredProcedure.USP_USER_INVOICE_LIST, hash);
            List<UserInvoiceDC> obj = new List<UserInvoiceDC>();
            ViewBag.data = obj;
            if (data.IsNotNull())
            {
                foreach(DataRow items in data.Rows)
                {
                    obj.Add(new UserInvoiceDC()
                    {
                        amount = Convert.ToString(items["amount"]),
                        discount = Convert.ToString(items["discount"]),
                        paymentDate = Convert.ToString(items["paymentDate"]),
                        paymentDateRange = Convert.ToString(items["paymentDateRange"]),
                        paymentDescription = Convert.ToString(items["paymentDescription"]),
                        paymentId = Convert.ToString(items["id"])
                    });
                }
            }

            return View(obj);
        }

        [HttpPost]
        public IActionResult UserPaymentList(List<UserInvoiceDC> obj)
        {
            
            return RedirectToAction("PayingGuest", "User");
        }

        public IActionResult InvoiceDashboard(string propertyId = "")
        {
            var UserId = HttpContext.User.FindFirstValue("UserId");
            Hashtable hash = new Hashtable();
            hash.Add("@ipUserId", UserId);
            hash.Add("@ipPropertyId", propertyId);
            DataSet ds = new ManualDbContext(config).GetDataSet(StoredProcedure.USP_GET_USER_DASHBOARD, hash);

            List<InvoiceDashboardDisplayOC> obj = new List<InvoiceDashboardDisplayOC>();
            List<InvoiceDashboardDisplayDataOC> obj1 = new List<InvoiceDashboardDisplayDataOC>();

            if (ds.IsNotNull())
            {
                if(ds.Tables.Count >= 2)
                {
                    foreach(DataRow row in ds.Tables[0].Rows)
                    {
                        obj.Add(new InvoiceDashboardDisplayOC()
                        {
                            amount = Convert.ToString(row["amount"]),
                            invoiceType = Convert.ToString(row["invoiceType"])
                        });
                    }

                    foreach (DataRow row in ds.Tables[1].Rows)
                    {
                        obj1.Add(new InvoiceDashboardDisplayDataOC()
                        {
                            amount = Convert.ToString(row["amount"]),
                            property = Convert.ToString(row["property"]),
                            invoiceType = Convert.ToString(row["invoice_type"])
                        });
                    }
                }
            }

            ViewBag.List = obj1;
            ViewBag.Headers = obj;


            return View();
        }


    }
}
