using System.ComponentModel.DataAnnotations.Schema;

namespace GreenCross.Mammals.Entities
{
    public class HarvestMouseRecord : IEntity<int>
    {
        public int Id
        {
            get => RecordId;
            set => RecordId = value;
        }

        public int RecordId { get; set; }

        public int SourceRecordId { get; set; }

        public DateOnly DateRecorded { get; set; }

        public int RecorderId { get; set; }

        [ForeignKey("RecorderId")]
        public Recorder Recorder { get; set; } = null!;

        public int LocationId { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; } = null!;

        public int VerificationStatusId { get; set; }

        [ForeignKey("VerificationStatusId")]
        public VerificationStatus VerificationStatus { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
