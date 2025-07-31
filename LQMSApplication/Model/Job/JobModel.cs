using System.ComponentModel.DataAnnotations;

namespace LQMSApplication.Model.Job
{
    public class JobModel
    {
        [Key]
        public int Id { get; set; }
        public string? JobId { get; set; }
        public DateTime? ScheduledStartDate { get; set; }
        public DateTime? ScheduledEndDate { get; set; }
        public string? CustomerName { get; set; }
        public string? ProjectName { get; set; }
        public string? Location { get; set; }
        public int? NumOfBoreHoles { get; set; }
        public string? LabourId { get; set; }
        public string? EngineerId { get; set; }
    }
    public class JobStatusModel
    {
        [Key]
        public int Id { get; set; }
        public string? BHNumber { get; set; }
        public string? Status { get; set; }
        public decimal? Depth { get; set; }
        public decimal? CompletedDepth { get; set; }
        public decimal? RemainingDepth { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? Deleted { get; set; }
        public string? EngineerStatus { get; set; }
    }
}
