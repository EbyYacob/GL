using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LQMSApplication.Model.SiteActivity
{
    public class SiteActivity
    {
    }
    public class SiteActivityRequest
    {
        public List<DrillerSheetModel> DrillerSheet { get; set; }
        public List<TestingModel> Testing { get; set; }
        public List<MobilizationModel> Mobilization { get; set; }
        public List<MaintenanceModel> Maintenance { get; set; }
        public List<PurchaseModel> Purchase { get; set; }
        public List<DelayStopWorkModel> DelayStopWork { get; set; }
        public List<AccidentsIncidentsModel> AccidentsIncidents { get; set; }
        public List<HSEModel> HSE { get; set; }
        public List<GroutingModel> Grouting { get; set; }
        public List<TimeSheetModel> TimeSheet { get; set; }

    }

    public class TestingModel
    {

        [Key]
        public int Id { get; set; }
        public DateTime? TodaysDate { get; set; }
        public string? BHNumber { get; set; }
        public string? TimeFrom { get; set; }
        public string? TimeTo { get; set; }
        public string? PressureMeter { get; set; }
        public string? Remarks { get; set; }
        public string? Attachment { get; set; }
        public string? lattitude { get; set; }
        public string? Longitude { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public string? DeviceId { get; set; }
        public string? UserId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? Deleted { get; set; }
        public string? AttachmentID { get; set; }
    }

    public class MaintenanceModel
    {

        [Key]
        public int Id { get; set; }
        public DateTime? TodaysDate { get; set; }
        public string? BHNumber { get; set; }
        public string? TimeFrom { get; set; }
        public string? TimeTo { get; set; }
        public string? Equipment { get; set; }
        public string? Parts { get; set; }
        public decimal? Costs { get; set; }
        public string? Remarks { get; set; }
        public string? Attachment { get; set; }
        public string? lattitude { get; set; }
        public string? Longitude { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public string? DeviceId { get; set; }
        public string? UserId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? AttachmentID { get; set; }
    }

    public class PurchaseModel
    {

        [Key]
        public int Id { get; set; }
        [Column("Todays Date")]
        public DateTime? TodaysDate { get; set; }
        public string? BHNumber { get; set; }
        public string? Item { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public string? Remarks { get; set; }
        public string? Attachment { get; set; }
        public string? lattitude { get; set; }
        public string? Longitude { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public string? DeviceId { get; set; }
        public string? UserId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? Deleted { get; set; }
        public string? AttachmentID { get; set; }
    }

    public class DelayStopWorkModel
    {

        [Key]
        public int Id { get; set; }
        public DateTime? TodaysDate { get; set; }
        public string? BHNumber { get; set; }
        public string? TimeFrom { get; set; }
        public string? TimeTo { get; set; }
        public string? Remark { get; set; }
        public string? Attachment { get; set; }
        public string? lattitude { get; set; }
        public string? Longitude { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public string? Voice { get; set; }
        public string? DeviceId { get; set; }
        public string? UserId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? Deleted { get; set; }
        public string? AttachmentID { get; set; }
    }
   
    public class AccidentsIncidentsModel
    {

        [Key]
        public int Id { get; set; }
        public DateTime? TodaysDate { get; set; }
        public string? BHNumber { get; set; }
        public string? TypeofIncident { get; set; }
        public string? PersonInvolved { get; set; }
        [Column("Description of Incident")]
        public string? DescriptionofIncident { get; set; }  //[Description of Incident]
        public string? Remarks { get; set; }
        public string? Attachment { get; set; }
        public string? lattitude { get; set; }
        public string? Longitude { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public string? Voice { get; set; }
        public string? DeviceId { get; set; }
        public string? UserId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? Deleted { get; set; }
        public string? AttachmentID { get; set; }
    }

    public class HSEModel
    {

        [Key]
        public int Id { get; set; }
        public DateTime? TodaysDate { get; set; }
        public string? BHNumber { get; set; }
        public string? TimeFrom { get; set; }
        public string? TimeTo { get; set; }
        public string? TypeOfActivity { get; set; }  
        public string? Description { get; set; }
        public string? Remarks { get; set; }
        public string? Attachment { get; set; }
        public string? lattitude { get; set; }
        public string? Longitude { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public string? Voice { get; set; }
        public string? DeviceId { get; set; }
        public string? UserId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? Deleted { get; set; }
        public string? AttachmentID { get; set; }
    }

    public class GroutingModel
    {

        [Key]
        public int Id { get; set; }
        public DateTime? TodaysDate { get; set; }
        public string? BHNumber { get; set; }
        public string? TimeFrom { get; set; }
        public string? TimeTo { get; set; }
        public string? Remark { get; set; }  
        public string? Attachment { get; set; }
        public string? lattitude { get; set; }
        public string? Longitude { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public string? Voice { get; set; }
        public string? DeviceId { get; set; }
        public string? UserId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? Deleted { get; set; }
        public string? AttachmentID { get; set; }
    }

    public class TimeSheetModel
    {

        [Key]
        public int Id { get; set; }
        public DateTime? TodaysDate { get; set; }
        public string? BHNumber { get; set; }
        public string? TimeFrom { get; set; }
        public string? TimeTo { get; set; }
        public decimal? OTHours { get; set; }
        public string? Remarks { get; set; }
        public string? Attachment { get; set; }
        public string? lattitude { get; set; }
        public string? Longitude { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public string? Voice { get; set; }
        public string? DeviceId { get; set; }
        public string? UserId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? Deleted { get; set; }
        public string? AttachmentID { get; set; }
        public string? DutyInLattitude { get; set; }
        public string? DutyInLongitude { get; set; }
        public DateTime? DutyInDate { get; set; }
        public string? DutyInTime { get; set; }
        public string? DutyOutTime { get; set; }

    }

    public class MobilizationModel
    {
        [Key]  // Marks this property as the primary key
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment ID
        public int Id { get; set; }
        public string? BHNumber { get; set; }
        public DateTime? TodaysDate { get; set; }
        public string? TimeFrom { get; set; }
        public string? TimeTo { get; set; }
        public string? Remark { get; set; }
        public string? Attachment { get; set; }
        public string? lattitude { get; set; }
        public string? Longitude { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public string? DeviceId { get; set; }
        public string? UserId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? Deleted { get; set; }
        public string? AttachmentID { get; set; }

    }

    public class EquipmentDetailsModel
    {
        [Key]
        public int id { get; set; }
        public string EquipmentType { get; set; }
        public string EquipmentCode { get; set; }
        public string Condition { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }  // Add this
        public string ReferenceNumber { get; set; }  // Add this

        public string? DeviceId { get; set; }
        public string? UserId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? Deleted { get; set; }

    }

    public class DrillerSheetModel
    {
        [Key]
        public int Id { get; set; }
        //public DateOnly? TodaysDate { get; set; }
        public DateTime? TodaysDate { get; set; }
        public string? RigName { get; set; }
        public string? DrillingMethod { get; set; }
        public string? FluidType { get; set; }
        public string? BitType { get; set; }
        public decimal? Waterlevel { get; set; }
        public decimal? BoringDiameter { get; set; }
        public decimal? CasingDiameter { get; set; }
        public string? CoreBarrellType { get; set; }
        public decimal? CoreDiameter { get; set; }
        public string? WhetherCondition { get; set; }
        public string? EquipmentCondition { get; set; }
        public string? BHNumber { get; set; }
        public decimal? SampleDepth { get; set; }
        public string? SampleType { get; set; }
        public decimal? DepthFrom { get; set; }
        public decimal? DepthTo { get; set; }
        public decimal? SampleNumber { get; set; }
        public decimal? CasingDepth { get; set; }
        public string? DescriptionData { get; set; }
        public decimal? InitialPenetration { get; set; }
        public decimal? RecoveryLength { get; set; }
        public decimal? NValue1 { get; set; }
        public decimal? NValue2 { get; set; }
        public decimal? NValue3 { get; set; }
        public decimal? NValue4 { get; set; }
        public decimal? NValue5 { get; set; }
        public decimal? NValue6 { get; set; }
        public decimal? NValue { get; set; }
        public decimal? TCR { get; set; }
        public decimal? SCR { get; set; }
        public decimal? RQD { get; set; }
        public decimal? WL { get; set; }
        public decimal? FL { get; set; }
        public string? Remarks { get; set; }
        public string? Attachment { get; set; }
        public string? lattitude { get; set; }
        public string? Longitude { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public string? Voice { get; set; }
        public string? DeviceId { get; set; }
        public string? UserId { get; set; }
        public string? AttachmentID { get; set; }
        public string? Note { get; set; }
        public decimal? RL { get; set; }
        public decimal? SPTRecovery { get; set; }
        public string? WaterLevelTime { get; set; }
        //public string? ImageName { get; set; }
        //public string? ImagePath { get; set; }
        //public string? BoreholeDescription { get; set; }

        // Navigation Property
        public List<EquipmentListModel>? EquipmentList { get; set; }
    }

    [Table("T_SI_MAPP_EquipmentDetails")] // Mapping to correct table
    public class EquipmentListModel
    {
        [Key]
        public int Id { get; set; }
        public string EquipmentName { get; set; }
        public string ReferenceNumber { get; set; }
        public string Status { get; set; }
        // Foreign Key
        public int DrillSheetId { get; set; }

        [ForeignKey("DrillSheetId")]
        public DrillerSheetModel DrillerSheet { get; set; }
    }

    public class AttachmentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Ensure EF Core treats ID as auto-increment
        public int ID { get; set; }
        public string? AttachmentID { get; set; }
        public string? BaseFile { get; set; }
        public string? AttachmentType { get; set; }
        public byte[]? BinaryFile { get; set; }
        public string? BinaryFileNew { get; set; }

    }

    public class UpdateDrillerSheetModel
    {
        public int Id { get; set; }
        public string? ImageName { get; set; }
        public string? ImagePath { get; set; }
        public string? BoreholeDescription { get; set; }

    }

    public class EngineerDrillerSheetModel
    {
        [Key]
        public int BoreholeLoggingId { get; set; }
        public int DrillerSheetId { get; set; }
        public string? BHNumber { get; set; }
        public decimal? DepthFrom { get; set; }
        public decimal? DepthTo { get; set; }
        public decimal? NValue1 { get; set; }
        public decimal? NValue2 { get; set; }
        public decimal? NValue3 { get; set; }
        public decimal? NValue4 { get; set; }
        public decimal? NValue5 { get; set; }
        public decimal? NValue6 { get; set; }
        public decimal? NValue { get; set; }
        public decimal? TCR { get; set; }
        public decimal? SCR { get; set; }
        public decimal? RQD { get; set; }
        public string? Remarks { get; set; }
        public string? Note { get; set; }
        public string? ImageName { get; set; }
        public string? ImagePath { get; set; }
        public decimal? EngDepthFrom { get; set; }
        public decimal? EngDepthTo { get; set; }
        public string? SampleType { get; set; }
        public decimal? SampleNumber { get; set; }
        public decimal? SPTRecovery { get; set; }
        public string? AttachmentID { get; set; }
        public string? DescriptionData { get; set; }
        public string? EngLattitude { get; set; }
        public string? EngLongitude { get; set; }

    }

}
