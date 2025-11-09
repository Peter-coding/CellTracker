using CellTracker.Api.Models.Configuration;

namespace CellTracker.Api.Models.Dto
{
    public class UpdateProductionLineDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int OrdinalNumber { get; set; }
        public string? Description { get; set; }  
        public ProductionLineStatus Status { get; set; }
        public Guid FactoryId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
