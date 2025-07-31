using System.ComponentModel.DataAnnotations;

namespace LQMSApplication.Model.BoreHole
{
    public class BOHDetailsModel
    {
        [Key]
        public int Id { get; set; }
        public string? BHNumber { get; set; }
        public string? JobId { get; set; }
        public decimal? Depth { get; set; }
        public decimal? N { get; set; }
        public decimal? E { get; set; }
        public decimal? Level { get; set; }
        public decimal? Elevation { get; set; }
    }
}
