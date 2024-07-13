namespace SK_PG_WebApp.Helper
{
    public static class StoredProcedure
    {
        public static readonly string USP_DASHBOARD = "usp_Dashboard";

        /// <summary>
        /// Accept Parameters 
        /// userId : int
        /// </summary>
        public static readonly string USP_GET_USER_PROFILE = "usp_getUserDetails";
        public static readonly string USP_GET_ALL_USERS = "usp_getAllUsers";

        #region Masters

        public static readonly string USP_PAYMENT_TYPE_MASTER= "usp_getPaymentTypeMaster";
        /// <summary>
        /// Accept Parameters 
        /// </summary>
        public static readonly string USP_PG_Rooms = "usp_getPayingGuistRooms";
        public static readonly string USP_PAYMENT_GATEWAY = "usp_getPaymentGateway";
        public static readonly string USP_CITY = "usp_city";
        public static readonly string USP_STATE = "usp_state";
        public static readonly string USP_LOCATION = "usp_location";
        public static readonly string USP_LOCATION_BY_CITY = "usp_locationByCity";
        public static readonly string USP_PROPERTY_BY_LOCATION= "usp_getPropertyByLocation";
        /// <summary>
        /// Accept Parameters 
        /// adminId : int
        /// </summary>
        public static readonly string USP_PAYING_GUEST = "usp_getPayingGuest";
        /// <summary>
        /// Accept Parameters 
        /// IN ipuserId bigint, IN ippayToUserId bigint,ipamount decimal(18,2), ipdiscount decimal(18,2), ipdateRange nvarchar(100)
        /// ,ippaymentDate nvarchar(50), ippaymnetTypeId bigint, ippaymentDetails nvarchar(100),ippaymentGatewayId bigint
        /// , ippaymentTypeDescription nvarchar(100)
        /// </summary>
        public static readonly string USP_PG_PAYMENT = "usp_PG_Payment";

        /// <summary>
        /// Accept Parameters
        /// ipuserId : int
        /// </summary>
        public static readonly string USP_GENERATE_INVOICE = "usp_GenerateInvoice";

        public static readonly string USP_ADD_PROPERTY = "usp_addProperty";
        public static readonly string USP_GET_PROPERTY_DETAILS = "usp_getPropertyDetails";
        public static readonly string USP_GET_PROPERTY = "usp_getProperty";
        public static readonly string USP_GET_PROPERTY_DETAIL_DASHBOARD = "usp_get_propertyDetailsDashboard";
        /// <summary>
        /// Get Rooms Availability
        /// Accepts : ippgMasterId
        /// </summary>
        public static readonly string USP_GET_ROOMS_AVAILABILITY = "usp_getRoomAvailability";
        public static readonly string USP_VIEW_ROOM_BOOKING = "usp_viewRoomBooking";
        public static readonly string USP_UPDATE_BOOKING_MASTER = "usp_updateBookingMaster";

        public static readonly string USP_USER_INVOICE_LIST = "usp_userInvoiceList";
        public static readonly string USP_GET_USER_DASHBOARD = "usp_getInvoiceDashboard";

        #endregion
    }
}
