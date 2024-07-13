using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SK_PG_WebApp.DAL;
using SK_PG_WebApp.Helper;
using SK_PG_WebApp.Models.DynamicModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SK_PG_WebApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration config;
        private readonly DatabaseContext dbContext;
        private IHostingEnvironment environment;

        public UserController(ILogger<UserController> logger, DatabaseContext databaseContext, IConfiguration _config, IHostingEnvironment _environment)
        {
            _logger = logger;
            dbContext = databaseContext;
            config = _config;
            environment = _environment;
        }

        public IActionResult Users(int id = 0)
        {

            var roleId = Convert.ToInt32(HttpContext.User.FindFirstValue("roleId"));
            var userId = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));
            if (roleId == 2)
            {
                ViewBag.isEditOrCreate = "none";
                ViewBag.PropertyMapping = "none";
                var data = dbContext.UsersDbSet.Where(x => x.roleId == id && x.id == userId).ToList();
                ViewBag.userId = id;

                if (data.IsNotNull())
                {
                    foreach (var row in data)
                    {
                        row.password = "http://api.whatsapp.com/send?phone=91" + row.whatsAppNumber + "&text=Hii, " + row.name + "&source=&data=";
                    }
                }


                return View(data);
            }
            else
            {
                ViewBag.isEditOrCreate = "initial";
                ViewBag.PropertyMapping = id == 2 ? "initial" : "none";
                var data = dbContext.UsersDbSet.Where(x => x.roleId == id).ToList();
                ViewBag.userId = id;

                if (data.IsNotNull())
                {
                    foreach (var row in data)
                    {
                        row.password = "http://api.whatsapp.com/send?phone=91" + row.whatsAppNumber + "&text=Hii, " + row.name + "&source=&data=";
                    }
                }


                return View(data);
            }
            
        }

        public IActionResult UserProfile(int id)
        {
            Hashtable hash = new Hashtable();
            hash.Add("@userId", id);
            var data = new ManualDbContext(config).GetDataTable(StoredProcedure.USP_GET_USER_PROFILE, hash);

            if (data.IsNull())
            {
                return RedirectToAction("Users", new { id = id });
            }
            if (data.Rows.Count == 0)
            {
                return RedirectToAction("Users", new { id = id });
            }

            List<UserProfileDC> obj = new List<UserProfileDC>();
            string path = Path.Combine("/assets/img/Myicons/");

            obj.Add(new UserProfileDC()
            {
                name = Convert.ToString(data.Rows[0]["name"]),
                contactNumber = Convert.ToString(data.Rows[0]["contactNumber"]),
                address = Convert.ToString(data.Rows[0]["address"]),
                whatsAppNumber = Convert.ToString(data.Rows[0]["whatsAppNumber"]),
                adhar = Convert.ToString(data.Rows[0]["aadhar"]),
                createdDate = Convert.ToString(data.Rows[0]["created_date"]),
                designation = Convert.ToString(data.Rows[0]["designation"]),
                dob = Convert.ToString(data.Rows[0]["dob"]),
                education = Convert.ToString(data.Rows[0]["education"]),
                email = Convert.ToString(data.Rows[0]["email"]),
                location = Convert.ToString(data.Rows[0]["location"]),
                pan = Convert.ToString(data.Rows[0]["pan"]),
                qualification = Convert.ToString(data.Rows[0]["qualification"]),
                referByUser = Convert.ToString(data.Rows[0]["referByUserName"]),
                userId = Convert.ToInt64(data.Rows[0]["userId"]),
                lastLoginDate = Convert.ToString(data.Rows[0]["lastLoginDate"]),
                role = Convert.ToString(data.Rows[0]["role"]),
                about = Convert.ToString(data.Rows[0]["about"]),
                familyContactNumber = Convert.ToString(data.Rows[0]["familyContactNumber"]),
                familyMember = Convert.ToString(data.Rows[0]["familyMember"]),
                cityName = Convert.ToString(data.Rows[0]["cityName"]),
                deposit = Convert.ToString(data.Rows[0]["deposit"]),
                propertyName = Convert.ToString(data.Rows[0]["propertyName"]),
                rent = Convert.ToString(data.Rows[0]["rent"]),
                roomName = Convert.ToString(data.Rows[0]["roomName"]),
                imageURL = Convert.ToString(data.Rows[0]["photo"]).IsNullOrEmpty() ? "/dist/img/userDefault.jpg" : path + Convert.ToString(data.Rows[0]["photo"])
            });

            return View(obj);
        }

        [HttpPost]
        public IActionResult UserProfile(UserProfileDC obj, string email)
        {

            if (obj.userId.IsNotNull())
            {
                var userData = dbContext.UsersDbSet.Where(x => x.id == obj.userId).FirstOrDefault();
                userData.email = obj.email.IsNotNullOrEmpty() ? obj.email : userData.email;
                userData.address = obj.address.IsNotNullOrEmpty() ? obj.address : userData.address;
                userData.contactNumber = obj.contactNumber.IsNotNullOrEmpty() ? Convert.ToInt64(obj.contactNumber) : userData.contactNumber;
                userData.whatsAppNumber = obj.whatsAppNumber.IsNotNullOrEmpty() ? Convert.ToInt64(obj.whatsAppNumber) : userData.whatsAppNumber;
                userData.designation = obj.designation.IsNotNullOrEmpty() ? obj.designation : userData.designation;
                userData.location = obj.location.IsNotNullOrEmpty() ? obj.location : userData.location;
                userData.DOB = obj.dob.IsNotNullOrEmpty() ? Convert.ToDateTime(obj.dob) : userData.DOB;


                dbContext.Add(userData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();

                var userDetails = dbContext.UsersDetailsDbSet.Where(x => x.userId == obj.userId).FirstOrDefault();
                userDetails.pan = obj.pan.IsNotNullOrEmpty() ? obj.pan : userDetails.pan;
                userDetails.aadhar = obj.adhar.IsNotNullOrEmpty() ? obj.adhar : userDetails.aadhar;
                userDetails.education = obj.education.IsNotNullOrEmpty() ? obj.education : userDetails.education;
                userDetails.familyContactNumber = obj.familyContactNumber.IsNotNullOrEmpty() ? obj.familyContactNumber : userDetails.familyContactNumber;
                userDetails.familyMember = obj.familyMember.IsNotNullOrEmpty() ? obj.familyMember : userDetails.familyMember;
                userDetails.qualification = obj.qualification.IsNotNullOrEmpty() ? obj.qualification : userDetails.qualification;

                dbContext.Add(userDetails).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();
            }


            return RedirectToAction("UserProfile", new { id = obj.userId });
        }

        public IActionResult PayingGuest()
        {
            ViewBag.Search = string.Empty;
            ViewBag.Pages = null;
            Hashtable hash = new Hashtable();
            hash.Add("ipuserName", string.Empty);
            hash.Add("ippageNo", 1);
            hash.Add("ippageSize", 5);
            var roleId = Convert.ToInt32(HttpContext.User.FindFirstValue("roleId"));
            var userId = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));
            hash.Add("ipRoleId", roleId);
            hash.Add("ipUserId", userId);

            var data = new ManualDbContext(config).GetDataSet(StoredProcedure.USP_GET_ALL_USERS, hash);
            List<PayingGuestDC> obj = new List<PayingGuestDC>();

            if (data.IsNotNull())
            {
                if (data.Tables[0].Rows.Count > 0)
                {
                    var dataRows = (int)Math.Ceiling((double)Convert.ToInt32(data.Tables[1].Rows[0]["totalCount"]) / (double)5); ;
                    var pNo = new MasterDropdowns(dbContext, config).PaginationDD(1, dataRows);
                    ViewBag.Pages = pNo;
                }
                else
                {
                    var pNo = new MasterDropdowns(dbContext, config).PaginationDD(1, 1);
                    ViewBag.Pages = pNo;
                }
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    obj.Add(new PayingGuestDC()
                    {
                        activateDate = Convert.ToString(row["activateDate"]),
                        birthDate = Convert.ToString(row["birthDate"]),
                        emailAddress = Convert.ToString(row["emailAddress"]),
                        emergencyContact = Convert.ToString(row["emergencyContact"]),
                        familyMember = Convert.ToString(row["familyMember"]),
                        lastPaymentDate = Convert.ToString(row["lastPaymentDate"]),
                        mobileNumber = Convert.ToString(row["mobileNumber"]),
                        name = Convert.ToString(row["name"]),
                        nextPaymentDate = Convert.ToString(row["nextPaymentDate"]),
                        nextRenewDate = Convert.ToString(row["nextRenewDate"]),
                        Property = Convert.ToString(row["Property"]),
                        roomNo = Convert.ToString(row["roomNo"]),
                        uniqueId = Convert.ToString(row["uniqueId"]),
                        whatsAppLink = "http://api.whatsapp.com/send?phone=91"+ Convert.ToString(row["mobileNumber"]) + "&text=Hii&source=&data="
                    });
                }
            }
            ViewBag.Data = obj;
            return View();
        }

        [HttpPost]
        public IActionResult PayingGuest(string pageNo, string table_search = "")
        {
            table_search = table_search.IsNotNullOrEmpty() ? "%"+table_search+"%" : String.Empty;
            ViewBag.Pages = null;
            ViewBag.Search = table_search.Replace("%","");
            Hashtable hash = new Hashtable();
            hash.Add("ipuserName", table_search);
            int pageSize = ((Convert.ToInt32(pageNo) - 1) * 5);
            hash.Add("ippageNo", pageSize);
            
            hash.Add("ippageSize", 5);
            var roleId = Convert.ToInt32(HttpContext.User.FindFirstValue("roleId"));
            var userId = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));
            hash.Add("ipRoleId", roleId);
            hash.Add("ipUserId", userId);
            var data = new ManualDbContext(config).GetDataSet(StoredProcedure.USP_GET_ALL_USERS, hash);
            List<PayingGuestDC> obj = new List<PayingGuestDC>();

            if (data.IsNotNull())
            {
                if (data.Tables[0].Rows.Count > 0)
                {
                    var dataRows = (int)Math.Ceiling((double)Convert.ToInt32(data.Tables[1].Rows[0]["totalCount"]) / (double)5); ;
                    var pNo = new MasterDropdowns(dbContext, config).PaginationDD(1, dataRows);
                    ViewBag.Pages = pNo;
                }
                else
                {
                    var pNo = new MasterDropdowns(dbContext, config).PaginationDD(1, 1);
                    ViewBag.Pages = pNo;
                }

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    obj.Add(new PayingGuestDC()
                    {
                        activateDate = Convert.ToString(row["activateDate"]),
                        birthDate = Convert.ToString(row["birthDate"]),
                        emailAddress = Convert.ToString(row["emailAddress"]),
                        emergencyContact = Convert.ToString(row["emergencyContact"]),
                        familyMember = Convert.ToString(row["familyMember"]),
                        lastPaymentDate = Convert.ToString(row["lastPaymentDate"]),
                        mobileNumber = Convert.ToString(row["mobileNumber"]),
                        name = Convert.ToString(row["name"]),
                        nextPaymentDate = Convert.ToString(row["nextPaymentDate"]),
                        nextRenewDate = Convert.ToString(row["nextRenewDate"]),
                        Property = Convert.ToString(row["Property"]),
                        roomNo = Convert.ToString(row["roomNo"]),
                        uniqueId = Convert.ToString(row["uniqueId"]),
                        whatsAppLink = "http://api.whatsapp.com/send?phone=91" + Convert.ToString(row["mobileNumber"]) + "&text=Hii&source=&data="
                    });
                }
            }
            ViewBag.Data = obj;
            return View();
        }

        [HttpGet]
        public IActionResult PayingGuestRoomMapping(int id)
        {
            var data = dbContext.UsersDbSet.Where(x => x.id == id).FirstOrDefault();
            ViewBag.userName = data.name;
            ViewBag.userId = id;
            ViewBag.City = new MasterDropdowns(dbContext, config).City();
            return View();
        }

        [HttpPost]
        public IActionResult PayingGuestRoomMapping(int userId, string userName, string city)
        {
            return RedirectToAction("PayingGuestRoomMappingLocation", new { userId, userName, city });
        }

        //[HttpPost]
        [HttpGet]
        public IActionResult PayingGuestRoomMappingLocation(int userId, string userName, string city)
        {
            Int64 cityId = Convert.ToInt64(city);
            var cityData = dbContext.CityDbSet.Where(x => x.id == cityId).FirstOrDefault();
            ViewBag.userName = userName;
            ViewBag.userId = userId;
            ViewBag.city = cityData.name;
            ViewBag.cityId = city;
            ViewBag.Location = new MasterDropdowns(dbContext, config).LocationByCity(Convert.ToInt32(city));
            return View();
        }

        //[HttpPost]
        public IActionResult PayingGuestRoomMappingProperty(int userId, string userName, string city
            , string cityId, string ddlLocation)
        {
            var locationData = dbContext.LocationDbSet.Where(x => x.id == Convert.ToInt64(ddlLocation)).FirstOrDefault();
            ViewBag.userName = userName;
            ViewBag.userId = userId;
            ViewBag.city = city;
            ViewBag.cityId = cityId;
            ViewBag.location = locationData.name;
            ViewBag.locationId = ddlLocation;
            ViewBag.Property = new MasterDropdowns(dbContext, config).PropertyByLocation(Convert.ToInt32(ddlLocation));
            return View();
        }

        //[HttpPost]
        public IActionResult PayingGuestRoomMappingRooms(int userId, string userName, string city
            , string cityId, string location, string locationId, string ddlProperty)
        {

            MapPGToRoomDC obj = new MapPGToRoomDC();
            obj.name = userName;
            obj.userId = Convert.ToString(userId);
            obj.city = city;
            obj.cityId = cityId;
            obj.location = location;
            obj.locationId = locationId;
            obj.propertyId = ddlProperty;
            var property = dbContext.PGMasterDbSet.Where(x => x.id == Convert.ToInt64(ddlProperty)).FirstOrDefault();
            obj.property = property.name;
            //obj.boysAvailable = Convert.ToString(property.noOfRoomsAvailableBoys);
            //obj.girlsAvailable = Convert.ToString(property.noOfRoomsAvailableGirls);


            //var data = dbContext.PGRoomMasterDbSet.Where(x => x.pgMasterId == Convert.ToInt64(ddlProperty)).ToList();
            //if (data.IsNotNull())
            //{
            //    foreach (var item in data)
            //    {
            //        obj.pgRoomDetails.Add(new MapPGToRoomDCDetails()
            //        {
            //            capacity = Convert.ToString(item.pgCapacity),
            //            isAvailable = Convert.ToInt32(item.availablePG) > 0 ? 1 : 0,
            //            roomName = Convert.ToString(item.name),
            //            allocatedTo = Convert.ToString(item.allocatedTo),
            //            deposit = Convert.ToString(item.depositAmount),
            //            rent = Convert.ToString(item.RentAmount),
            //            roomId = Convert.ToString(item.id),
            //            backgroundColour = Convert.ToString(item.backgroundColour),
            //        });
            //    }
            //}

            Hashtable hash = new Hashtable();
            hash.Add("ippgMasterId", ddlProperty);
            var data = new ManualDbContext(config).GetDataTable(StoredProcedure.USP_GET_ROOMS_AVAILABILITY, hash);

            if (data.IsNotNull())
            {
                if(data.Rows.Count > 0)
                {
                    obj.boysAvailable = Convert.ToString(data.Rows[0]["noOfRoomsAvailableBoys"]);
                    obj.girlsAvailable = Convert.ToString(data.Rows[0]["noOfRoomsAvailableGirls"]);
                    foreach (DataRow row in data.Rows)
                    {
                        obj.pgRoomDetails.Add(new MapPGToRoomDCDetails()
                        {
                            capacity = Convert.ToString(row["pgCapacity"]),
                            isAvailable = Convert.ToString(row["isAvailable"]),
                            roomName = Convert.ToString(row["name"]),
                            allocatedTo = Convert.ToString(row["allocatedTo"]),
                            deposit = Convert.ToString(row["depositAmount"]),
                            rent = Convert.ToString(row["RentAmount"]),
                            roomId = Convert.ToString(row["roomId"]),
                            backgroundColour = Convert.ToString(row["backgroundColour"]),
                            allocation = Convert.ToString(row["allocation"]),
                            availability = Convert.ToDecimal(row["availability"])
                        });
                    }
                }

            }
            


            return View(obj);
        }


        //[HttpPost]
        public IActionResult PayingGuestRoomMappingRoomsPost(string userId, string propertyId, MapPGToRoomDC objIC)
        {
            var roomDetails = dbContext.PGRoomMasterDbSet.Where(x => x.id == Convert.ToInt64(propertyId)).FirstOrDefault();
            var payingGuestMaster = dbContext.PayingGuestMasterDbSet.Where(x => x.userId == Convert.ToInt64(userId)).FirstOrDefault();

            if (payingGuestMaster.IsNotNull())
            {
                payingGuestMaster.depositAmount = roomDetails.depositAmount;
                payingGuestMaster.rentAmount = roomDetails.RentAmount;
                payingGuestMaster.pgMasterId = Convert.ToInt64(roomDetails.pgMasterId);
                payingGuestMaster.pgRoomMasterId = roomDetails.id;
                payingGuestMaster.activatedDate = DateTime.Now;
                payingGuestMaster.userId = Convert.ToInt64(userId);

                dbContext.Add(payingGuestMaster).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();
            }
            else
            {
                Models.BusinessModels.PayingGuestMaster payingGuestMasterObj = new Models.BusinessModels.PayingGuestMaster();
                payingGuestMasterObj.depositAmount = roomDetails.depositAmount;
                payingGuestMasterObj.rentAmount = roomDetails.RentAmount;
                payingGuestMasterObj.pgMasterId = Convert.ToInt64(roomDetails.pgMasterId);
                payingGuestMasterObj.pgRoomMasterId = roomDetails.id;
                payingGuestMasterObj.activatedDate = DateTime.Now;
                payingGuestMasterObj.userId = Convert.ToInt64(userId);

                dbContext.Add(payingGuestMasterObj).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                dbContext.SaveChanges();
            }

            


            Hashtable hash = new Hashtable();
            hash.Add("ipPgMasterId", roomDetails.pgMasterId);
            var updatePGDashboard = new ManualDbContext(config).GetDataTable(StoredProcedure.USP_UPDATE_BOOKING_MASTER, hash);


            return RedirectToAction("PayingGuest");
        }

        public IActionResult ViewRoomBooking(int id)
        {
            Hashtable hash = new Hashtable();
            hash.Add("ipPgMasterId", id);
            var data = new ManualDbContext(config).GetDataSet(StoredProcedure.USP_VIEW_ROOM_BOOKING, hash);

            List<ViewRoomBookingDC> obj = new List<ViewRoomBookingDC>();
            foreach(DataRow row in data.Tables[0].Rows)
            {
                obj.Add(new ViewRoomBookingDC()
                {
                    roomId = Convert.ToString(row["roomId"]),
                    name = Convert.ToString(row["roomName"]),
                    allocatedTo = Convert.ToString(row["allocatedTo"]),
                    backgroundColour = Convert.ToString(row["backgroundColour"]),
                    depositAmount = Convert.ToString(row["depositAmount"]),
                    pgCapacity = Convert.ToString(row["pgCapacity"]),
                    rentAmount = Convert.ToString(row["rentAmount"]),
                });
            }


            foreach(DataRow row in data.Tables[1].Rows)
            {
                var roomId = Convert.ToString(row["pgRoomMasterId"]);

                foreach (var item in obj)
                {
                    if(item.roomId == roomId)
                    {
                        item.pgDetails.Add(new ViewRoomBookingPGDC()
                        {
                            nextRenewDate = Convert.ToString(row["nextrenewDate"]),
                            name = Convert.ToString(row["userName"]),
                            whatsAppNumber = Convert.ToString(row["whatsAppNumber"]),
                            backgroundColour = "green"
                        });
                        break;
                    }

                    
                }
            }

            foreach (var item in obj)
            {
                var cnt = item.pgDetails.Count;
                for (int i=0; i<Convert.ToInt32(item.pgCapacity) - cnt; i++)
                {
                    item.pgDetails.Add(new ViewRoomBookingPGDC()
                    {
                        nextRenewDate = Convert.ToString("----"),
                        name = "Not Allocated",
                        whatsAppNumber = "----",
                        backgroundColour ="skyblue"
                    });
                }
                
            }


            ViewBag.Data = obj;
            return View();
        }


        public IActionResult TestDataTable()
        {
            return View();
        }

        public IActionResult AddNewUser(int id)
        {
            
            if(id == 3)
            {
                ViewBag.UserRole = "Paying Guest";
            }else if(id == 2)
            {
                ViewBag.UserRole = "PG Admin";
            }
            else if (id == 1)
            {
                ViewBag.UserRole = "Super Admin";
            }
            else
            {
                return RedirectToAction("Index1", "Home");
            }
            ViewBag.Error = string.Empty;
            ViewBag.Gender = new MasterDropdowns(dbContext, config).Gender();
            ViewBag.Users = new MasterDropdowns(dbContext, config).Users();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewUser(string name, string designation, string email, string cotactNumber, string whatsApp
            ,string dob, string about, string gender, string qualification, string education, string aadhar, string pan,
            string familyMember, string refferedBy, List<IFormFile> postedFiles, string familyContact, string pAddress)
        {
            string userPhotoFile = postedFiles[0].FileName;
            try
            {

                string path = Path.Combine(this.environment.ContentRootPath, "wwwroot/assets/img/Myicons");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // get the root of localhost or server

                string filepath = path + "/" + Guid.NewGuid().ToString() + postedFiles[0].FileName.Replace(" ",""); // "/MSPDF/" + usermoneysign.money_sign + ".pdf";//this.Environment.ContentRootPath +"\\" + usermoneysign.money_sign + ".pdf";
                 // System.IO.FileStream fs = new FileStream(filepath, FileMode.Create);
                userPhotoFile = Path.GetFileName(filepath);
                using (Stream fileStream = new FileStream(filepath, FileMode.Create))
                {
                    await postedFiles[0].CopyToAsync(fileStream);
                }
            }
            catch(Exception ex)
            {

            }
           

            ViewBag.Error = string.Empty;
            Models.BusinessModels.UsersBO objUser = new Models.BusinessModels.UsersBO();
            objUser.name = name;
            objUser.address = pAddress;
            objUser.contactNumber = Convert.ToInt64(cotactNumber);
            objUser.about = about;
            objUser.designation = designation;
            objUser.email = email;
            objUser.location = string.Empty;
            objUser.roleId = designation == "Paying Guest" ? 3 : ( designation == "Super Admin" ? 1 : 2);
            objUser.whatsAppNumber = Convert.ToInt64(whatsApp);
            objUser.DOB = Convert.ToDateTime(dob);
            objUser.Gender = gender;
            objUser.photo = userPhotoFile;




            Models.BusinessModels.UserDetailsBO objDetails = new Models.BusinessModels.UserDetailsBO();
            
            objDetails.qualification = qualification;
            objDetails.education = education;
            objDetails.aadhar = aadhar;
            objDetails.pan = pan;
            objDetails.familyMember = familyMember;
            objDetails.familyContactNumber = familyContact;
            if (refferedBy.IsNotNullOrEmpty())
            {
                objDetails.referByUserId = Convert.ToInt64(refferedBy);
            }

            using (IDbContextTransaction tran = dbContext.Database.BeginTransaction())
            {
                try
                {
                    dbContext.UsersDbSet.Add(objUser);
                    dbContext.SaveChanges();

                    objDetails.userId = objUser.id;

                    dbContext.UsersDetailsDbSet.Add(objDetails);
                    dbContext.SaveChanges();

                    dbContext.Database.CommitTransaction();
                }
                catch(Exception ex)
                {
                    dbContext.Database.RollbackTransaction();
                    ViewBag.Error = "Something went wrong, Please try again!";
                    ViewBag.Gender = new MasterDropdowns(dbContext, config).Gender();
                    ViewBag.Users = new MasterDropdowns(dbContext, config).Users();
                    ViewBag.UserRole = "Paying Guest";
                    return View();

                }
            }

            if(designation == "Paying Guest")
            {
                return RedirectToAction("PayingGuest");
            }
            else if(designation == "Super Admin")
            {
                return RedirectToAction("Users", new {id = 1});
            }
            else
            {
                return RedirectToAction("Users",new {id = 2});
            }
            

           
            


           
        }

        public IActionResult AddNewPGAdmin()
        {
            return View();
        }

        public IActionResult PgAdminPropertyMapping(int pgAdminId)
        {
            ViewBag.userId = pgAdminId;
            //var data = dbContext.AdminPropertyMappingDbSet.Where(x => x.userId == pgAdminId).ToList();
            var pgMasterData = dbContext.PGMasterDbSet.ToList();
            var data = (from mapping in dbContext.AdminPropertyMappingDbSet.Where(x=>x.userId == pgAdminId)
                        join details in dbContext.PGMasterDbSet on mapping.pgmasterId equals details.id 
                        
                        select new
                        {
                            id = mapping.id,
                            name = details.name,
                            detailsId = details.id
                        }).ToList();
            List<SelectListItem> dd = new List<SelectListItem>();
            foreach(var items in pgMasterData)
            {
                if (!data.Select(x => x.detailsId).Contains(items.id))
                {
                    dd.Add(new SelectListItem()
                    {
                        Value = items.id.ToString(),
                        Text = items.name.ToString()
                    });
                }
               
            }
            List<AddPropertyRooms> rooms = new List<AddPropertyRooms>();
            foreach(var items in data)
            {
                rooms.Add(new AddPropertyRooms()
                {
                    roomNo = items.id.ToString(),
                    Deposit = items.name
                });
            }
            ViewBag.PropertyList = rooms;
            ViewBag.PGDropdown = dd;
            return View();
        }
        [HttpPost]
        public IActionResult PgAdminPropertyMapping(string property, string userId)
        {
            if (property.IsNotNullOrEmpty())
            {
                dbContext.AdminPropertyMappingDbSet.Add(new Models.BusinessModels.AdminPropertyMappingBO()
                {
                    pgmasterId = Convert.ToInt64(property),
                    userId = Convert.ToInt64(userId)
                }).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                dbContext.SaveChanges();
            }
            

            return RedirectToAction("PgAdminPropertyMapping", new { pgAdminId = userId });
        }

       
        public IActionResult PgAdminPropertyRemoveMapping(string property, string userId)
        {
            if (property.IsNotNullOrEmpty())
            {
                var data = dbContext.AdminPropertyMappingDbSet.Where(x => x.id == Convert.ToInt64(property)).FirstOrDefault();
                dbContext.AdminPropertyMappingDbSet.Remove(data);
                dbContext.SaveChanges();
            }


            return RedirectToAction("PgAdminPropertyMapping", new { pgAdminId = userId });
        }

        public IActionResult TenantList()
        {
            ViewBag.Search = string.Empty;
            ViewBag.Pages = null;
            Hashtable hash = new Hashtable();
            hash.Add("ipuserName", string.Empty);
            hash.Add("ippageNo", 1);
            hash.Add("ippageSize", 5);
            var roleId = Convert.ToInt32(HttpContext.User.FindFirstValue("roleId"));
            var userId = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));
            hash.Add("ipRoleId", roleId);
            hash.Add("ipUserId", userId);

            var data = new ManualDbContext(config).GetDataSet(StoredProcedure.USP_GET_ALL_USERS, hash);
            List<PayingGuestDC> obj = new List<PayingGuestDC>();

            if (data.IsNotNull())
            {
                if (data.Tables[0].Rows.Count > 0)
                {
                    var dataRows = (int)Math.Ceiling((double)Convert.ToInt32(data.Tables[1].Rows[0]["totalCount"]) / (double)5); ;
                    var pNo = new MasterDropdowns(dbContext, config).PaginationDD(1, dataRows);
                    ViewBag.Pages = pNo;
                }
                else
                {
                    var pNo = new MasterDropdowns(dbContext, config).PaginationDD(1, 1);
                    ViewBag.Pages = pNo;
                }
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    obj.Add(new PayingGuestDC()
                    {
                        activateDate = Convert.ToString(row["activateDate"]),
                        birthDate = Convert.ToString(row["birthDate"]),
                        emailAddress = Convert.ToString(row["emailAddress"]),
                        emergencyContact = Convert.ToString(row["emergencyContact"]),
                        familyMember = Convert.ToString(row["familyMember"]),
                        lastPaymentDate = Convert.ToString(row["lastPaymentDate"]),
                        mobileNumber = Convert.ToString(row["mobileNumber"]),
                        name = Convert.ToString(row["name"]),
                        nextPaymentDate = Convert.ToString(row["nextPaymentDate"]),
                        nextRenewDate = Convert.ToString(row["nextRenewDate"]),
                        Property = Convert.ToString(row["Property"]),
                        roomNo = Convert.ToString(row["roomNo"]),
                        uniqueId = Convert.ToString(row["uniqueId"]),
                        whatsAppLink = "http://api.whatsapp.com/send?phone=91" + Convert.ToString(row["mobileNumber"]) + "&text=Hii&source=&data="
                    });
                }
            }
            ViewBag.Data = obj;
            return View();
        }

        [HttpPost]
        public IActionResult TenantList(string pageNo, string table_search = "")
        {
            table_search = table_search.IsNotNullOrEmpty() ? "%" + table_search + "%" : String.Empty;
            ViewBag.Pages = null;
            ViewBag.Search = table_search.Replace("%", "");
            Hashtable hash = new Hashtable();
            hash.Add("ipuserName", table_search);
            int pageSize = ((Convert.ToInt32(pageNo) - 1) * 5);
            hash.Add("ippageNo", pageSize);

            hash.Add("ippageSize", 5);
            var roleId = Convert.ToInt32(HttpContext.User.FindFirstValue("roleId"));
            var userId = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));
            hash.Add("ipRoleId", roleId);
            hash.Add("ipUserId", userId);
            var data = new ManualDbContext(config).GetDataSet(StoredProcedure.USP_GET_ALL_USERS, hash);
            List<PayingGuestDC> obj = new List<PayingGuestDC>();

            if (data.IsNotNull())
            {
                if (data.Tables[0].Rows.Count > 0)
                {
                    var dataRows = (int)Math.Ceiling((double)Convert.ToInt32(data.Tables[1].Rows[0]["totalCount"]) / (double)5); ;
                    var pNo = new MasterDropdowns(dbContext, config).PaginationDD(1, dataRows);
                    ViewBag.Pages = pNo;
                }
                else
                {
                    var pNo = new MasterDropdowns(dbContext, config).PaginationDD(1, 1);
                    ViewBag.Pages = pNo;
                }

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    obj.Add(new PayingGuestDC()
                    {
                        activateDate = Convert.ToString(row["activateDate"]),
                        birthDate = Convert.ToString(row["birthDate"]),
                        emailAddress = Convert.ToString(row["emailAddress"]),
                        emergencyContact = Convert.ToString(row["emergencyContact"]),
                        familyMember = Convert.ToString(row["familyMember"]),
                        lastPaymentDate = Convert.ToString(row["lastPaymentDate"]),
                        mobileNumber = Convert.ToString(row["mobileNumber"]),
                        name = Convert.ToString(row["name"]),
                        nextPaymentDate = Convert.ToString(row["nextPaymentDate"]),
                        nextRenewDate = Convert.ToString(row["nextRenewDate"]),
                        Property = Convert.ToString(row["Property"]),
                        roomNo = Convert.ToString(row["roomNo"]),
                        uniqueId = Convert.ToString(row["uniqueId"]),
                        whatsAppLink = "http://api.whatsapp.com/send?phone=91" + Convert.ToString(row["mobileNumber"]) + "&text=Hii&source=&data="
                    });
                }
            }
            ViewBag.Data = obj;
            return View();
        }

        public IActionResult UserNotice(string userId)
        {
            try
            {
                var alreadyGenerated = dbContext.UserNoticeDbSet.Where(x => x.userId == Convert.ToInt64(userId)).FirstOrDefault();
                var data = dbContext.UsersDbSet.Where(x => x.id == Convert.ToInt64(userId)).FirstOrDefault();

                ViewBag.name = data.name;
                ViewBag.email = data.email;
                ViewBag.number = data.contactNumber;
                ViewBag.whatsApp = data.whatsAppNumber;
                ViewBag.address = data.location;
                ViewBag.userid = userId;
                var date = alreadyGenerated.IsNotNull() ? alreadyGenerated.lastDate : DateTime.Now.AddDays(30);
                ViewBag.lastdate = date.Date.ToString("yyyy-MM-dd");
                ViewBag.reason = alreadyGenerated.IsNotNull() ? alreadyGenerated.reason : String.Empty;
              

               
                return View();

            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                return RedirectToAction("Index1", "Home");
            }

            
        }

        [HttpPost]
        public IActionResult UserNotice(string name, string email, string dateip, string userid, string reason)
        {
            try
            {
                var alreadyGenerated = dbContext.UserNoticeDbSet.Where(x => x.userId == Convert.ToInt64(userid)).FirstOrDefault();

                if (alreadyGenerated.IsNotNull())
                {
                    alreadyGenerated.lastDate = Convert.ToDateTime(dateip);
                    alreadyGenerated.reason = reason;
                    dbContext.UserNoticeDbSet.Add(alreadyGenerated).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();
                }
                else
                {
                    Models.BusinessModels.UserNoticeBO obj = new Models.BusinessModels.UserNoticeBO();
                    obj.userId = Convert.ToInt64(userid);
                    obj.reason = reason;
                    obj.lastDate = Convert.ToDateTime(dateip);
                    dbContext.UserNoticeDbSet.Add(obj).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    dbContext.SaveChanges();
                }

                return RedirectToAction("UserNotice", new { userId = userid });

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return RedirectToAction("Index1", "Home");
            }


        }

        public IActionResult NoticeFromTenant(string propertyId)
        {

            var data = (
                from pgMaster in dbContext.PGMasterDbSet.Where(x=>x.id == Convert.ToInt64(propertyId))
                join payingGuest in dbContext.PayingGuestMasterDbSet on pgMaster.id equals payingGuest.pgMasterId
                join notice in dbContext.UserNoticeDbSet on payingGuest.userId equals notice.userId
                join users in dbContext.UsersDbSet on notice.userId equals users.id
                select new
                {
                    users.id,
                    users.name,
                    users.whatsAppNumber,
                    users.email,
                    Date = notice.lastDate.Date.ToString("yyyy-MM-dd"),
                    notice.reason
                }).ToList();

            List<NoticeFromTenantDC> obj = new List<NoticeFromTenantDC>();



            if (data.IsNotNull())
            {
                int i = 1;
                foreach(var items in data)
                {
                    obj.Add(new NoticeFromTenantDC()
                {
                    srNo = i,
                    id = Convert.ToString(items.id),
                    email = items.email,
                    lastDate = Convert.ToString(items.Date),
                    name = items.name,
                    reason = items.reason,
                    whatsApp = Convert.ToString(items.whatsAppNumber)
                });
                    i = i + 1;
                }
               
            }
            ViewBag.Data = obj;

            return View();
        }

    }
}
