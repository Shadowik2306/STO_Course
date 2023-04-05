using STODataModels.Models;

namespace STOContracts.SearchModels
{
    public class StorekeeperSearchModel
    {
        public int? Id { get; set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }
    }
}
