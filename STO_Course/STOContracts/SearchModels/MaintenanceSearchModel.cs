namespace STOContracts.SearchModels
{
    public class MaintenanceSearchModel
    {
        public int? Id { get; set; }
        public int? EmployerId { get; set; }
        public double? Cost { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? DateFrom { get; set; }
    }
}
