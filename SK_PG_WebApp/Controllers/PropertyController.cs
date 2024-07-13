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
using System.Linq;
using System.Security.Claims;

namespace SK_PG_WebApp.Controllers
{
    [Authorize]
    public class PropertyController : Controller
    {
        private readonly ILogger<PropertyController> _logger;
        private readonly IConfiguration config;
        private readonly DatabaseContext dbContext;

        public PropertyController(ILogger<PropertyController> logger, DatabaseContext databaseContext, IConfiguration _config)
        {
            _logger = logger;
            dbContext = databaseContext;
            config = _config;
        }


        public IActionResult PropertyDashboard()
        {
            var roleId = Convert.ToInt32(HttpContext.User.FindFirstValue("roleId"));
            if (roleId == 1)
            {
                var data = dbContext.PGMasterDbSet.ToList();
                return View(data);
            }
            else
            {
                var UserId = HttpContext.User.FindFirstValue("UserId");
                var data = (from mapping in dbContext.AdminPropertyMappingDbSet.Where(x => x.userId == Convert.ToInt64(UserId))
                            join master in dbContext.PGMasterDbSet on mapping.pgmasterId equals master.id
                            select new PGMasterBO
                            {
                                id = master.id,
                                name = master.name,
                                ownerContactNumber = master.ownerContactNumber,
                                ownerName = master.ownerName,
                                noOfRooms = master.noOfRooms,
                                noOfGirlsRooms = master.noOfGirlsRooms,
                                noOfBoysRooms = master.noOfBoysRooms,
                                cityId = master.cityId,
                                description = master.description,
                                noOfRoomsAvailableBoys = master.noOfRoomsAvailableBoys,
                                noOfRoomsAvailableGirls = master.noOfRoomsAvailableGirls,
                                noOfRoomsBookedBoys = master.noOfRoomsBookedBoys,
                                noOfRoomsBookedGirls = master.noOfRoomsBookedGirls,
                                ownerAddress = master.ownerAddress,
                                partiallyBookedBoys = master.partiallyBookedBoys,
                                stateId = master.stateId,
                                locationId = master.locationId,
                                partiallyBookedGirls = master.partiallyBookedGirls


                            }).ToList();


                return View(data);
            }
        }
        public IActionResult AddUpdateProperty()
        {
            ViewBag.City = new MasterDropdowns(dbContext, config).City();
            ViewBag.State = new MasterDropdowns(dbContext, config).State();
            ViewBag.Location = new MasterDropdowns(dbContext, config).Location();

            //AddUpdatePropertyDC list = new AddUpdatePropertyDC();

            return View();
        }

        public IActionResult EditProperty(int id)
        {
            Hashtable hash = new Hashtable();
            hash.Add("@id", 1);
            var data = new ManualDbContext(config).GetDataSet(StoredProcedure.USP_GET_PROPERTY_DETAILS, hash);
            EditPropertyDC obj = new EditPropertyDC();
            ViewBag.propertyName = Convert.ToString(data.Tables[0].Rows[0]["name"]);
            ViewBag.propertyDescription = Convert.ToString(data.Tables[0].Rows[0]["description"]);
            ViewBag.location = Convert.ToString(data.Tables[0].Rows[0]["location"]);
            ViewBag.state = Convert.ToString(data.Tables[0].Rows[0]["state"]);
            ViewBag.city = Convert.ToString(data.Tables[0].Rows[0]["city"]);

            ViewBag.fullName = Convert.ToString(data.Tables[0].Rows[0]["ownerName"]);
            ViewBag.address = Convert.ToString(data.Tables[0].Rows[0]["ownerAddress"]);
            ViewBag.contactNumber = Convert.ToString(data.Tables[0].Rows[0]["ownerContactNumber"]);
            ViewBag.propertyId = Convert.ToString(id);

            var pgRooms = dbContext.PGRoomMasterDbSet.Where(x => x.pgMasterId == id);

            var floors = dbContext.PropertyFloorDbSet.Where(x => x.pgMasterId == id).Count();
            if (floors == 0)
            {
                ViewBag.isAddButtonVisible = "none";
            }
            else
            {
                ViewBag.isAddButtonVisible = "initial";
            }
            //foreach(var items in pgRooms)
            //{
            //    items.allocatedTo = items.allocatedTo == "Boys" ? "skyblue" : "lightpink";
            //}

            //    obj.Add(new EditPropertyDC()
            //    {
            //        propertyName = Convert.ToString(data.Tables[0].Rows[0]["name"]),
            //    propertyDescription = Convert.ToString(data.Tables[0].Rows[0]["description"]),
            //    location = Convert.ToString(data.Tables[0].Rows[0]["locationId"]),
            //    state = Convert.ToString(data.Tables[0].Rows[0]["stateId"]),
            //    city = Convert.ToString(data.Tables[0].Rows[0]["cityId"]),

            //    fullName = Convert.ToString(data.Tables[0].Rows[0]["ownerName"]),
            //    address = Convert.ToString(data.Tables[0].Rows[0]["ownerAddress"]),
            //    contactNumber = Convert.ToString(data.Tables[0].Rows[0]["ownerContactNumber"]),
            //});


            ViewBag.data = pgRooms;

            return View();
        }

        [HttpPost]
        public IActionResult AddUpdateProperty(AddUpdatePropertyDC list, string address)
        {
            Hashtable hash = new Hashtable();
            hash.Add("@ipname", list.propertyName);
            hash.Add("@ipdescription", list.propertyDescription);
            hash.Add("@ipstateId", list.state);
            hash.Add("@ipcityId", list.city);
            hash.Add("@iplocationId", list.location);
            hash.Add("@ipownerName", list.ownerDetails.fullName);
            hash.Add("@ipownerNumber", list.ownerDetails.contactNumber);
            hash.Add("@ipownerAddress", address);

            var data = new ManualDbContext(config).GetDataTable(StoredProcedure.USP_ADD_PROPERTY, hash);

            return RedirectToAction("PropertyDashboard");
        }

        //[HttpPost]
        public IActionResult AddPGRoom(int id, int? isAlreadyExists)
        {
            var data = dbContext.PGMasterDbSet.Where(x => x.id == id).FirstOrDefault();
            ViewBag.propertyName = Convert.ToString(data.name);
            ViewBag.pgId = Convert.ToString(id);
            ViewBag.PGAllocatdTo = new MasterDropdowns(dbContext, config).PGAllocatdTo();
            ViewBag.Floor = new MasterDropdowns(dbContext, config).PropertyFloors(id);

            if (isAlreadyExists == 1)
            {
                ViewBag.Error = "Room Already Exists with Same Room No or Room Name.";
            }
            else
            {
                ViewBag.Error = "";
            }

            return View();
        }

        public IActionResult EditPGRoom(int id, int? isAlreadyExists)
        {
            var data = dbContext.PGRoomMasterDbSet.Where(x => x.id == id).FirstOrDefault();
            ViewBag.PGAllocatdTo = new MasterDropdowns(dbContext, config).PGAllocatdTo(data.isAllocatedToGirls);
            ViewBag.Data = data;
            return View(data);
        }

        [HttpPost]
        public IActionResult AddPGRoomPost(PGRoomMasterBO objIC, string ddlPGAllocatedTo, string ddlFloor)
        {
            var pgMaster = dbContext.PGMasterDbSet.Where(x => x.id == objIC.pgMasterId).FirstOrDefault();
            pgMaster.noOfRooms = pgMaster.noOfRooms + 1;
            objIC.floorId = Convert.ToInt64(ddlFloor);

            if (ddlPGAllocatedTo == "0") // for girls
            {
                objIC.allocatedTo = "Girls";
                objIC.isAllocatedToGirls = true;
                objIC.backgroundColour = "lightpink";
                pgMaster.noOfGirlsRooms = pgMaster.noOfGirlsRooms + 1;
                pgMaster.noOfRoomsAvailableGirls = pgMaster.noOfRoomsAvailableGirls + 1;

            }
            else
            {
                pgMaster.noOfBoysRooms = pgMaster.noOfBoysRooms + 1;
                pgMaster.noOfRoomsAvailableBoys = pgMaster.noOfRoomsAvailableBoys + 1;
            }

            var validate = dbContext.PGRoomMasterDbSet.Where(x => x.pgMasterId == objIC.pgMasterId &&
                       x.name == objIC.name).FirstOrDefault();





            if (validate.IsNotNull())
            {
                return RedirectToAction("AddPGRoom", new { id = objIC.pgMasterId, isAlreadyExists = 1 });
            }

            objIC.id = 0;
            dbContext.PGRoomMasterDbSet.Add(objIC).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            dbContext.SaveChanges();

            dbContext.PGMasterDbSet.Add(pgMaster).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dbContext.SaveChanges();

            return RedirectToAction("EditProperty", new { id = objIC.pgMasterId });
        }

        [HttpPost]
        public IActionResult EditPGRoomPost(PGRoomMasterBO objIC, string ddlPGAllocatedTo)
        {
            var pgMaster = dbContext.PGMasterDbSet.Where(x => x.id == objIC.pgMasterId).FirstOrDefault();

            var validate = dbContext.PGRoomMasterDbSet.Where(x => x.id == objIC.id).FirstOrDefault();

            if (validate.isAllocatedToGirls == Convert.ToBoolean(Convert.ToInt32(ddlPGAllocatedTo)))
            {

                if (ddlPGAllocatedTo == "0") // for girls
                {
                    validate.allocatedTo = "Girls";
                    validate.isAllocatedToGirls = true;
                    validate.backgroundColour = "lightpink";
                    pgMaster.noOfGirlsRooms = pgMaster.noOfGirlsRooms + 1;
                    pgMaster.noOfRoomsAvailableGirls = pgMaster.noOfRoomsAvailableGirls + 1;
                    pgMaster.noOfBoysRooms = pgMaster.noOfBoysRooms - 1;
                    pgMaster.noOfRoomsAvailableBoys = pgMaster.noOfRoomsAvailableBoys - 1;

                }
                else
                {
                    validate.allocatedTo = "Boys";
                    validate.isAllocatedToGirls = false;
                    validate.backgroundColour = "skyblue";
                    pgMaster.noOfGirlsRooms = pgMaster.noOfGirlsRooms - 1;
                    pgMaster.noOfRoomsAvailableGirls = pgMaster.noOfRoomsAvailableGirls - 1;
                    pgMaster.noOfBoysRooms = pgMaster.noOfBoysRooms + 1;
                    pgMaster.noOfRoomsAvailableBoys = pgMaster.noOfRoomsAvailableBoys + 1;
                }
            }

            validate.depositAmount = objIC.depositAmount;
            validate.RentAmount = objIC.RentAmount;
            validate.noOfHall = objIC.noOfHall;
            validate.noOfBedroom = objIC.noOfBedroom;
            validate.noOfKitchen = objIC.noOfKitchen;
            validate.noticePeriodDays = objIC.noticePeriodDays;
            validate.noticePeriodMonths = objIC.noticePeriodMonths;
            validate.pgCapacity = objIC.pgCapacity;



            dbContext.PGRoomMasterDbSet.Add(validate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dbContext.SaveChanges();

            dbContext.PGMasterDbSet.Add(pgMaster).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dbContext.SaveChanges();

            return RedirectToAction("EditProperty", new { id = objIC.pgMasterId });
        }

        public IActionResult PropertyDetailsDashboard(int propertyId)
        {
            Hashtable hash = new Hashtable();
            hash.Add("ipPropertyId", propertyId);
            ViewBag.propertyId = propertyId;
            var data = new ManualDbContext(config).GetDataSet(StoredProcedure.USP_GET_PROPERTY_DETAIL_DASHBOARD, hash);
            //ViewBag.propertyId = propertyId;

            if (data.IsNull())
            {
                return RedirectToAction("PropertyDashboard");
            }
            PropertyDetailsDashboardDC obj = new PropertyDetailsDashboardDC();
            obj.propertyId = Convert.ToString(propertyId);
            obj.description = Convert.ToString(data.Tables[0].Rows[0]["description"]);
            obj.owner = Convert.ToString(data.Tables[0].Rows[0]["ownerName"]);
            obj.ownerNumber = Convert.ToString(data.Tables[0].Rows[0]["ownerContactNumber"]);
            obj.totalPaymentCollection = Convert.ToString(data.Tables[0].Rows[0]["totalPaymentCollection"]);
            obj.totalNP = Convert.ToString(data.Tables[0].Rows[0]["totalNP"]);
            obj.totalTenant = Convert.ToString(data.Tables[0].Rows[0]["totalTenant"]);
            obj.name = Convert.ToString(data.Tables[0].Rows[0]["name"]);

            ViewBag.Details = obj;
            List<PropertyDetailsDashboard_PGFloorDC> objFloor = new List<PropertyDetailsDashboard_PGFloorDC>();

            foreach (DataRow row in data.Tables[1].Rows)
            {
                objFloor.Add(new PropertyDetailsDashboard_PGFloorDC()
                {
                    background = Convert.ToString(row["background"]),
                    name = Convert.ToString(row["name"]),
                    totalPG = Convert.ToString(row["totalPG"]),
                    totalRooms = Convert.ToString(row["totalRooms"]),
                    id = Convert.ToString(row["id"]),
                    visibilityRemoveButton = (Convert.ToInt32(row["totalPG"]) > 0 || Convert.ToInt32(row["totalRooms"]) > 0) ? "none" : "initial"
                });
            }

            ViewBag.FloorData = objFloor;
            return View();
        }

        public IActionResult AddPropertyFloor(int propertyId)
        {
            var data = dbContext.PGMasterDbSet.Where(x => x.id == propertyId).FirstOrDefault();
            if (data.IsNull())
            {
                return RedirectToAction("PropertyDashboard");
            }
            ViewBag.Name = data.name;
            ViewBag.id = data.id;
            ViewBag.PGAllocatdTo = new MasterDropdowns(dbContext, config).PGAllocatdTo();
            return View();
        }

        [HttpPost]
        public IActionResult AddPropertyFloor(string propertyId, string name, string ddlPGAllocatedTo)
        {
            var data = dbContext.PGMasterDbSet.Where(x => x.id == Convert.ToInt64(propertyId)).FirstOrDefault();
            if (propertyId.IsNullOrEmpty() || name.IsNullOrEmpty() || ddlPGAllocatedTo.IsNullOrEmpty())
            {
                return RedirectToAction("PropertyDashboard");
            }

            dbContext.PropertyFloorDbSet.Add(new PropertyFloorBO()
            {
                name = name,
                pgMasterId = Convert.ToInt64(propertyId),
                allocatedTo = ddlPGAllocatedTo == "1" ? "Boys" : "Girls",
                isAllocatedToGirls = ddlPGAllocatedTo == "2" ? true : false
            }).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            dbContext.SaveChanges();

            return RedirectToAction("PropertyDetailsDashboard", new { propertyId = propertyId });

        }

        public IActionResult RemoveFloor(string floorId, string propertyId)
        {
            var data = (from floor in dbContext.PropertyFloorDbSet.Where(x => x.id == Convert.ToInt32(floorId))
                        join master in dbContext.PayingGuestMasterDbSet on floor.id equals master.floorId
                        select new
                        {
                            floor.id
                        }).ToList();

            if (data.Count > 0)
            {
                return RedirectToAction("PropertyDashboard");
            }

            var floors = dbContext.PropertyFloorDbSet.Where(x => x.id == Convert.ToInt64(floorId)).FirstOrDefault();

            dbContext.Remove(floors);
            dbContext.SaveChanges();

            return RedirectToAction("PropertyDetailsDashboard", new { propertyId = propertyId });
        }

        public IActionResult PropertyDetailsDashboardV2()
        {
            return View();
        }
    }
}
