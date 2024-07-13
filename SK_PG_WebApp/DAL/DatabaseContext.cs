using Microsoft.EntityFrameworkCore;

namespace SK_PG_WebApp.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Models.BusinessModels.UsersBO> UsersDbSet { get; set; }
        public DbSet<Models.BusinessModels.UserDetailsBO> UsersDetailsDbSet { get; set; }
        public DbSet<Models.BusinessModels.PayingGuestPaymentBO> PayingGuestPaymentDbSet { get; set; }
        public DbSet<Models.BusinessModels.PGMasterBO> PGMasterDbSet { get; set; }
        public DbSet<Models.BusinessModels.PGRoomMasterBO> PGRoomMasterDbSet { get; set; }
        public DbSet<Models.BusinessModels.CityBO> CityDbSet { get; set; }
        public DbSet<Models.BusinessModels.LocationBO> LocationDbSet { get; set; }
        public DbSet<Models.BusinessModels.PayingGuestMaster> PayingGuestMasterDbSet { get; set; }
        public DbSet<Models.BusinessModels.AdminPropertyMappingBO> AdminPropertyMappingDbSet { get; set; }
        public DbSet<Models.BusinessModels.PropertyFloorBO> PropertyFloorDbSet { get; set; }
        public DbSet<Models.BusinessModels.UserNoticeBO> UserNoticeDbSet { get; set; }

    }
}
