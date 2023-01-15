using QEnergy.ExcelUploadApp.Utils.Enums;
using System;

namespace QEnergy.ExcelUploadApp
{
    public class ProjectRow
    {
        // Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProjectNumber { get; set; }
        public DateTime? AcquisitionDate { get; set; }
        public string Number3lCode { get; set; }
        public ProjectDealTypes DealType { get; set; }
        public string Group { get; set; }
        public ProjectStatuses Status { get; set; }
        public string WTGNumbers { get; set; }
        public long? Kw { get; set; }
        public int? MonthsAquired { get; set; }
        public int CompanyId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public Guid CreatedBy { get; set; } // Creation user id 
        public Guid? ModifiedBy { get; set; } // Modification user id
        public bool Deleted { get; set; }
    }
}