namespace AdoXML.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrackServiceHistory")]
    public partial class TrackServiceHistory
    {
        [Key]
        public int intServiceHistoryId { get; set; }

        public int? intEquipmentID { get; set; }

        public int? intComponentId { get; set; }

        [Column(TypeName = "date")]
        public DateTime dRepairDate { get; set; }

        public int? intStopReason { get; set; }

        [StringLength(2000)]
        public string strDescriptionProblem { get; set; }

        public double? intMeterDue { get; set; }

        public int? intPmChecklist { get; set; }

        public int? intSMCSJob { get; set; }

        [StringLength(255)]
        public string strSMCSJob { get; set; }

        public int? intSMCSComponentID { get; set; }

        [StringLength(255)]
        public string strSMCSComponentID { get; set; }

        public int? intModifierId { get; set; }

        public int? intQuantityId { get; set; }

        public int? intJobReasonId { get; set; }

        public double intlLaborHours { get; set; }

        [StringLength(8)]
        public string repair_waiting_on { get; set; }

        [StringLength(255)]
        public string strDescriptiontFindings { get; set; }

        [StringLength(12)]
        public string old_job_no { get; set; }

        public int? Status { get; set; }

        public int? intRegNo { get; set; }

        public int? intSegment { get; set; }

        public int? intCountSegment { get; set; }

        public int? intLocationId { get; set; }

        [StringLength(50)]
        public string intNumberAlong { get; set; }

        public int? intEvalutionId { get; set; }
    }
}
