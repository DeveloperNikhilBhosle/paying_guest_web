using System;

namespace SK_PG_WebApp.Models.DynamicModels
{
    public class UserProfileDC
    {
        public Int64 userId { get; set; }
        public string name { get; set; }
        public string designation { get; set; }
        public string email { get; set; }
        public string location { get; set; }
        public string about { get; set; }
        public string createdDate { get; set; }
        public string contactNumber { get; set; }
        public string whatsAppNumber { get; set; }
        public string dob { get; set; }
        public string address { get; set; }
        public string qualification { get; set; }
        public string education { get; set; }
        public string adhar { get; set; }
        public string pan { get; set; }
        public string referByUser { get; set; }
        public string lastLoginDate { get; set; }
        public string role { get; set; }
        public string familyContactNumber { get; set; }
        public string familyMember { get; set; }

        // Added for PG apping

        public string cityName { get; set; }
        public string propertyName { get; set; }
        public string roomName { get; set; }
        public string deposit { get; set; }
        public string rent { get; set; }
        public string imageURL { get; set; }
    }
}
