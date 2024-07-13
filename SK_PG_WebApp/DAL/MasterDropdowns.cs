using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using SK_PG_WebApp.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SK_PG_WebApp.DAL
{
    public class MasterDropdowns
    {
        private readonly IConfiguration config;
        private readonly DatabaseContext dbContext;

        public MasterDropdowns( DatabaseContext databaseContext, IConfiguration _config)
        {
            dbContext = databaseContext;
            config = _config;
        }

        /// <summary>
        /// Payment Type Master
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<SelectListItem> PaymentTypeMaster()
        {
            List<SelectListItem> _return = new List<SelectListItem>();
            try
            {
                DataTable dt = new ManualDbContext(config).GetDataTable(StoredProcedure.USP_PAYMENT_TYPE_MASTER);
                _return.Add(new SelectListItem()
                {

                    Value = string.Empty,
                    Text = "Select Payment Type"
                });
                if (dt.IsNotNull())
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        _return.Add(new SelectListItem()
                        {
                            Value = Convert.ToString(row["id"]).IsNotNullOrEmpty() ? Convert.ToString(row["id"]) : string.Empty,
                            Text = Convert.ToString(row["name"]).IsNotNullOrEmpty() ? Convert.ToString(row["name"]) : string.Empty
                        });
                    }
                }


            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return _return;
        }

        /// <summary>
        /// PG Rooms
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<SelectListItem> PaymentGateway()
        {
            List<SelectListItem> _return = new List<SelectListItem>();
            try
            {
                DataTable dt = new ManualDbContext(config).GetDataTable(StoredProcedure.USP_PAYMENT_GATEWAY);

                if (dt.IsNotNull())
                {
                    _return.Add(new SelectListItem()
                    {

                        Value = string.Empty,
                        Text = "Select Payment Gateway"
                    });
                    foreach (DataRow row in dt.Rows)
                    {
                        _return.Add(new SelectListItem()
                        {
                            
                            Value = Convert.ToString(row["id"]).IsNotNullOrEmpty() ? Convert.ToString(row["id"]) : string.Empty,
                            Text = Convert.ToString(row["name"]).IsNotNullOrEmpty() ? Convert.ToString(row["name"]) : string.Empty
                        });
                    }
                }


            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return _return;
        }

        public List<SelectListItem> PayingGuest(int pgAdminId)
        {
            List<SelectListItem> _return = new List<SelectListItem>();
            try
            {
                Hashtable hash = new Hashtable();
                hash.Add("@adminId", pgAdminId);

                DataTable dt = new ManualDbContext(config).GetDataTable(StoredProcedure.USP_PAYING_GUEST,hash);

                if (dt.IsNotNull())
                {
                    _return.Add(new SelectListItem()
                    {

                        Value =  string.Empty,
                        Text = "Select Paying Guest"
                    });
                    foreach (DataRow row in dt.Rows)
                    {
                        _return.Add(new SelectListItem()
                        {

                            Value = Convert.ToString(row["id"]).IsNotNullOrEmpty() ? Convert.ToString(row["id"]) : string.Empty,
                            Text = Convert.ToString(row["name"]).IsNotNullOrEmpty() ? Convert.ToString(row["name"]) : string.Empty
                        });
                    }
                }


            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return _return;
        }


        /// <summary>
        /// City
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> City()
        {
            List<SelectListItem> _return = new List<SelectListItem>();
            try
            {
                DataTable dt = new ManualDbContext(config).GetDataTable(StoredProcedure.USP_CITY);

                if (dt.IsNotNull())
                {
                    _return.Add(new SelectListItem()
                    {

                        Value = string.Empty,
                        Text = "Select City"
                    });
                    foreach (DataRow row in dt.Rows)
                    {
                        _return.Add(new SelectListItem()
                        {

                            Value = Convert.ToString(row["id"]).IsNotNullOrEmpty() ? Convert.ToString(row["id"]) : string.Empty,
                            Text = Convert.ToString(row["name"]).IsNotNullOrEmpty() ? Convert.ToString(row["name"]) : string.Empty
                        });
                    }
                }


            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return _return;
        }

        /// <summary>
        /// City
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> State()
        {
            List<SelectListItem> _return = new List<SelectListItem>();
            try
            {
                DataTable dt = new ManualDbContext(config).GetDataTable(StoredProcedure.USP_STATE);

                if (dt.IsNotNull())
                {
                    _return.Add(new SelectListItem()
                    {

                        Value = string.Empty,
                        Text = "Select State"
                    });
                    foreach (DataRow row in dt.Rows)
                    {
                        _return.Add(new SelectListItem()
                        {

                            Value = Convert.ToString(row["id"]).IsNotNullOrEmpty() ? Convert.ToString(row["id"]) : string.Empty,
                            Text = Convert.ToString(row["name"]).IsNotNullOrEmpty() ? Convert.ToString(row["name"]) : string.Empty
                        });
                    }
                }


            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return _return;
        }


        /// <summary>
        /// Location
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> Location()
        {
            List<SelectListItem> _return = new List<SelectListItem>();
            try
            {
                DataTable dt = new ManualDbContext(config).GetDataTable(StoredProcedure.USP_LOCATION);

                if (dt.IsNotNull())
                {
                    _return.Add(new SelectListItem()
                    {

                        Value = string.Empty,
                        Text = "Select Location"
                    });
                    foreach (DataRow row in dt.Rows)
                    {
                        _return.Add(new SelectListItem()
                        {

                            Value = Convert.ToString(row["id"]).IsNotNullOrEmpty() ? Convert.ToString(row["id"]) : string.Empty,
                            Text = Convert.ToString(row["name"]).IsNotNullOrEmpty() ? Convert.ToString(row["name"]) : string.Empty
                        });
                    }
                }


            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return _return;
        }

        /// <summary>
        /// Location by City
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> LocationByCity(int cityId)
        {
            List<SelectListItem> _return = new List<SelectListItem>();
            try
            {
                Hashtable hash = new Hashtable();
                hash.Add("ipcityId", cityId);
                DataTable dt = new ManualDbContext(config).GetDataTable(StoredProcedure.USP_LOCATION_BY_CITY, hash);

                if (dt.IsNotNull())
                {
                    _return.Add(new SelectListItem()
                    {

                        Value = string.Empty,
                        Text = "Select Location"
                    });
                    foreach (DataRow row in dt.Rows)
                    {
                        _return.Add(new SelectListItem()
                        {

                            Value = Convert.ToString(row["id"]).IsNotNullOrEmpty() ? Convert.ToString(row["id"]) : string.Empty,
                            Text = Convert.ToString(row["name"]).IsNotNullOrEmpty() ? Convert.ToString(row["name"]) : string.Empty
                        });
                    }
                }


            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return _return;
        }

        /// <summary>
        /// Get Property by Location
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public List<SelectListItem> PropertyByLocation(int locationId)
        {
            List<SelectListItem> _return = new List<SelectListItem>();
            try
            {
                Hashtable hash = new Hashtable();
                hash.Add("ipLocationId", locationId);
                DataTable dt = new ManualDbContext(config).GetDataTable(StoredProcedure.USP_PROPERTY_BY_LOCATION, hash);

                if (dt.IsNotNull())
                {
                    _return.Add(new SelectListItem()
                    {

                        Value = string.Empty,
                        Text = "Select Property"
                    });
                    foreach (DataRow row in dt.Rows)
                    {
                        _return.Add(new SelectListItem()
                        {

                            Value = Convert.ToString(row["id"]).IsNotNullOrEmpty() ? Convert.ToString(row["id"]) : string.Empty,
                            Text = Convert.ToString(row["name"]).IsNotNullOrEmpty() ? Convert.ToString(row["name"]) : string.Empty
                        });
                    }
                }


            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return _return;
        }

        /// <summary>
        /// PG Allocated To - Boys Or Girls
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> PGAllocatdTo(bool isAllocatedToGirls = false)
        {
            List<SelectListItem> _return = new List<SelectListItem>();
            try
            {
                if (!isAllocatedToGirls)
                {
                    _return.Add(new SelectListItem()
                    {

                        Value = "1",
                        Text = "Boys"
                    });
                    _return.Add(new SelectListItem()
                    {

                        Value = "0",
                        Text = "Girls"
                    });
                }
                else
                {
                    
                    _return.Add(new SelectListItem()
                    {

                        Value = "0",
                        Text = "Girls"
                    });
                    _return.Add(new SelectListItem()
                    {

                        Value = "1",
                        Text = "Boys"
                    });
                }
                

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return _return;
        }

        /// <summary>
        /// Pagination
        /// </summary>
        /// <param name="activePageNo"></param>
        /// <param name="maxPageNo"></param>
        /// <returns></returns>
        public List<SelectListItem> PaginationDD(int activePageNo, int maxPageNo)
        {
            List<SelectListItem> _return = new List<SelectListItem>();
            try
            {
                for(int i=1; i<= maxPageNo; i++)
                {
                    _return.Add(new SelectListItem()
                    {

                        Value = i.ToString(),
                        Text = i.ToString(),
                        Selected = i == activePageNo ? true : false
                        
                    });
                }

               

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return _return;
        }

        /// <summary>
        /// Gender
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> Gender()
        {
            List<SelectListItem> _return = new List<SelectListItem>();
            try
            {

                _return.Add(new SelectListItem()
                {

                    Value = "Male",
                    Text = "Male"
                });

                _return.Add(new SelectListItem()
                {

                    Value = "FeMale",
                    Text = "FeMale"
                });


            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return _return;
        }

        /// <summary>
        /// Users 
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> Users()
        {
            List<SelectListItem> _return = new List<SelectListItem>();
            try
            {
                var data = dbContext.UsersDbSet.ToList();

                if (data.IsNotNull())
                {
                    _return.Add(new SelectListItem()
                    {

                        Value = string.Empty,
                        Text = "Select User"
                    });
                    foreach (var item in data)
                    {
                        _return.Add(new SelectListItem()
                        {

                            Value = Convert.ToString(item.id),
                            Text = Convert.ToString(item.name)
                        });
                    }
                }


            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return _return;
        }

        /// <summary>
        /// Get Property Floors By Id
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> PropertyFloors(int propertyId)
        {
            List<SelectListItem> _return = new List<SelectListItem>();
            try
            {
                var data = dbContext.PropertyFloorDbSet.Where(x=>x.pgMasterId == propertyId).ToList();

                if (data.IsNotNull())
                {
                    
                    foreach (var row in data)
                    {
                        _return.Add(new SelectListItem()
                        {

                            Value = Convert.ToString(row.id).IsNotNullOrEmpty() ? Convert.ToString(row.id) : string.Empty,
                            Text = Convert.ToString(row.name).IsNotNullOrEmpty() ? Convert.ToString(row.name) : string.Empty
                        });
                    }
                }


            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return _return;
        }
    }
}
