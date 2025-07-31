using Microsoft.EntityFrameworkCore;
using LQMSApplication.Model.User;
using LQMSApplication.Model.Job;
using LQMSApplication.Model.Master;
using LQMSApplication.Model.BoreHole;
using LQMSApplication.Model.SiteActivity;
using static LQMSApplication.Model.Master.MasterModel;
using System.IO;

namespace LQMSApplication.Data
{
    public class LQMSDBContext : DbContext
    {
        public LQMSDBContext(DbContextOptions<LQMSDBContext> options) : base(options) { }

        //User Controller Table
        public DbSet<UserModel> T_SI_MAPP_User { get; set; }

        //Job Controller Table
        public DbSet<JobModel> T_SI_MAPP_Job { get; set; }

        //Bore Hole Table
        public DbSet<BOHDetailsModel> T_SI_MAPP_BOHDetails { get; set; }

        //Job Status Table
        public DbSet<JobStatusModel> T_SI_MAPP_JobStatus { get; set; }

        //Master Controller Tables
        public DbSet<RigNameModel> T_SI_MAPP_RigName { get; set; } //Rig Name
        public DbSet<DrillingMethodModel> T_SI_MAPP_DrillingMethod { get; set; } //Drilling Method Name
        public DbSet<FluidTypeModel> T_SI_MAPP_FluidType { get; set; }  //Fluid Type Name
        public DbSet<BitTypeModel> T_SI_MAPP_BitType { get; set; } //Bit Type Name
        public DbSet<CoreBarrellTypeModel> T_SI_MAPP_CoreBarrellType { get; set; } //Core Barrell Type Name
        public DbSet<WhetherConditionModel> T_SI_MAPP_WhetherCondition { get; set; } //Whether Condition Name
        public DbSet<SampleTypeModel> T_SI_MAPP_SampleType { get; set; } //Sample Type Name
        public DbSet<PressureMeterModel> T_SI_MAPP_PressureMeter { get; set; } //Pressure Meter Name
        public DbSet<EquipmentModel> T_SI_MAPP_Equipment { get; set; } //Equipment Meter Name
        public DbSet<PartsModel> T_SI_MAPP_Parts { get; set; } //Parts Name
        public DbSet<CostsModel> T_SI_MAPP_Costs { get; set; } //Costs Name
        public DbSet<ItemModel> T_SI_MAPP_Item { get; set; } //Item Name
        public DbSet<TypeofIncidentModel> T_SI_MAPP_TypeofIncident { get; set; } //Type Of Incident Name
        public DbSet<TypeOfActivityModel> T_SI_MAPP_TypeOfActivity { get; set; } //Type Of Activity Name
        public DbSet<DescriptionDataModel> T_SI_MAPP_DescriptionData { get; set; } //Description Data Name
        public DbSet<EquipmentConditionModel> T_SI_MAPP_EquipmentCondition { get; set; } //Equipment Condition Name


        //SiteActivty Controller Tables
        public DbSet<DrillerSheetModel> T_SI_MAPP_DrillerSheet { get; set; }   //DrillerSheet Controller Table
        public DbSet<TestingModel> T_SI_MAPP_Testing { get; set; }    //Testing table
        public DbSet<MaintenanceModel> T_SI_MAPP_Maintenance { get; set; }  //Maintenance table
        public DbSet<PurchaseModel> T_SI_MAPP_Purchase { get; set; }  //Purchase table
        public DbSet<DelayStopWorkModel> T_SI_MAPP_DelayStopzwork { get; set; }  //DelayStopzwork table
        public DbSet<AccidentsIncidentsModel> T_SI_MAPP_AccidentsIncidents { get; set; }  //AccidentsIncidents table
        public DbSet<HSEModel> T_SI_MAPP_HSE { get; set; }  //HSE table
        public DbSet<GroutingModel> T_SI_MAPP_Grouting { get; set; }  //Grouting table
        public DbSet<TimeSheetModel> T_SI_MAPP_TimeSheet { get; set; }  //TimeSheet table
        public DbSet<MobilizationModel> T_SI_MAPP_Mobilization { get; set; }  //Mobilization Controller
        public DbSet<EquipmentListModel> T_SI_MAPP_EquipmentDetails { get; set; }

        public DbSet<EngineerDrillerSheetModel> T_SI_MAPP_EngineerDrillerSheet { get; set; }  //engineer DrillerSheet table

        public DbSet<AttachmentModel> T_SI_MAPP_AttachmentFile { get; set; }  //File Upload
        public DbSet<ImageModel> T_SI_MAPP_Images { get; set; }  //Images
    }
}
