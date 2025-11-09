using CellTracker.Api.Models.Configuration;
using System.ComponentModel.DataAnnotations;

namespace CellTracker.Api.Models.Dto
{
    public class ProductionLineDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }  // optional Description
        public ProductionLineStatus Status { get; set; }
        public Guid FactoryId { get; set; }


    }
}
