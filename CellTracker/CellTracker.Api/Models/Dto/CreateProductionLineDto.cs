using CellTracker.Api.Models.Configuration;
using System.ComponentModel.DataAnnotations;

namespace CellTracker.Api.Models.Dto
{
    public class CreateProductionLineDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ProductionLineStatus Status { get; set; }
        public Guid FactoryId { get; set; }


    }
}
