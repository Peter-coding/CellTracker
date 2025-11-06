using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Dto;
using CellTracker.Api.Services.CellService;
using CellTracker.Api.Services.FactoryService;
using CellTracker.Api.Services.ProductionLineService;
using CellTracker.Api.Services.WorkStationService;
using Microsoft.AspNetCore.Http.HttpResults;
using Npgsql.Replication;

namespace CellTracker.Api.Endpoints
{
    public static class FactoryConfigurator
    {
        public static void MapFactoryConfiguratorEndpoint(this IEndpointRouteBuilder app, string pathPrefix)
        {
            var path = $"/{pathPrefix}";

            app.MapPost($"{path}/CreateFactory", CreateFactoryAsync);
            app.MapGet($"{path}/FactoryById", GetFactoryByIdAsync);
            app.MapGet($"{path}/AllFactories", GetAllFactories);
            app.MapPut($"{path}/Factory", UpdateFactoryAsync);
            app.MapDelete($"{path}/Factory", DeleteFactoryAsync);

            app.MapPost($"{path}/CreateProductionLine", CreateProductionLineAsync);
            app.MapPost($"{path}/AddNextCellToProdLine", AddNextCellToProductionLineAsync);
            app.MapGet($"{path}/ProductionLineById", GetProductionLineByIdAsync);
            app.MapGet($"{path}/AllProductionLines", GetAllProductionLines);
            app.MapPut($"{path}/ProductionLine", UpdateProductionLineAsync);
            app.MapDelete($"{path}/ProductionLine", DeleteProductionLineAsync);

            app.MapPost($"{path}/CreateWorkStation", CreateWorkStationAsync);
            app.MapGet($"{path}/WorkStationById", GetWorkStationByIdAsync);
            app.MapGet($"{path}/AllWorkStations", GetAllWorkStations);
            app.MapPut($"{path}/WorkStation", UpdateWorkStationAsync);
            app.MapDelete($"{path}/WorkStation", DeleteWorkStationAsync);

            app.MapPost($"{path}/CreateCell", CreateCellAsync);
            app.MapGet($"{path}/GetCellById", GetCellByIdAsync);
            app.MapGet($"{path}/AllCells", GetAllCells);
            app.MapPut($"{path}/Cell", UpdateCellAsync);
            app.MapDelete($"{path}/Cell", DeleteCellAsync);
        }

        public async static Task<IResult> CreateFactoryAsync(IFactoryService factoryService, Factory factory)
        {
            var created = await factoryService.AddFactory(factory);
            if (created != null)
            {
                return Results.Ok(created);
            }
            return Results.BadRequest("Factory could not be created");
        }

        public async static Task<IResult> GetFactoryByIdAsync(IFactoryService factoryService, Guid factoryId)
        {
            var result = await factoryService.GetFactoryById(factoryId);
            if (result != null)
            {
                return Results.Ok(result);
            }
            return Results.NotFound();
        }

        public async static Task<IResult> GetAllFactories(IFactoryService factoryService)
        {
            var result = await factoryService.GetAllFactories();
            if (result != null)
            {
                return Results.Ok(result);
            }
            return Results.NotFound();
        }

        public async static Task<IResult> UpdateFactoryAsync(IFactoryService factoryService, Factory factory)
        {
            var result = await factoryService.UpdateFactory(factory);
            if (result != null)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest("Factory could not be updated");
        }

        public async static Task<IResult> DeleteFactoryAsync(IFactoryService factoryService, Guid guid)
        {
            var result = await factoryService.RemoveFactoryById(guid);
            if (result == true)
            {
                return Results.Ok();
            }

            return Results.NotFound();
        }

        public async static Task<IResult> AddNextCellToProductionLineAsync(IProductionLineService productionLineService, CellDto cellDto, Guid productionLineId)
        {
            var cell = await productionLineService.AddNextCellToProductionLine(cellDto, productionLineId);
            if (cell != null)
            {
                return Results.Ok(cell);
            }

            return Results.BadRequest("Cell could not be added to production line");
        }

        public async static Task<IResult> CreateProductionLineAsync(IProductionLineService productionLineService, ProductionLine productionLine)
        {
            var result = await productionLineService.AddProductionLine(productionLine);
            if (result != null)
            {
                return Results.Ok(result);
            }

            return Results.BadRequest("Production line could not be created");
        }

        public async static Task<IResult> GetProductionLineByIdAsync(IProductionLineService productionLineService, Guid productionLineId)
        {
            var result = await productionLineService.GetProductionLineById(productionLineId);
            if (result != null)
            {
                return Results.Ok(result);
            }

            return Results.NotFound();
        }

        public async static Task<IResult> GetAllProductionLines(IProductionLineService productionLineService)
        {
            var result = await productionLineService.GetAllProductionLines();
            if (result != null)
            {
                return Results.Ok(result);
            }
            return Results.NotFound();
        }

        public async static Task<IResult> UpdateProductionLineAsync(IProductionLineService productionLineService, ProductionLine productionLine)
        {
            var result = await productionLineService.UpdateProductionLine(productionLine);
            if (result != null)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest("Production line could not be updated");
        }

        public async static Task<IResult> DeleteProductionLineAsync(IProductionLineService productionLineService, Guid productionLineId)
        {
            var result = await productionLineService.RemoveProductionLineById(productionLineId);
            if (result == true)
            {
                return Results.Ok();
            }
            return Results.NotFound();
        }
        public async static Task<IResult> CreateWorkStationAsync(IWorkStationService workStationService, WorkStation workStation)
        {
            var result = await workStationService.AddWorkStation(workStation);
            if (result != null)
            {
                return Results.Ok(result);
            }

            return Results.BadRequest("WorkStation could not be created");
        }

        public async static Task<IResult> GetWorkStationByIdAsync(IWorkStationService workStationService, Guid workStationId)
        {
            var result = await workStationService.GetWorkStationById(workStationId);
            if (result != null)
            {
                return Results.Ok(result);
            }

            return Results.NotFound();
        }

        public async static Task<IResult> GetAllWorkStations(IWorkStationService workStationService)
        {
            var result = await workStationService.GetAllWorkStations();
            if (result != null)
            {
                return Results.Ok(result);
            }
            return Results.NotFound();
        }

        public async static Task<IResult> UpdateWorkStationAsync(IWorkStationService workStationService, WorkStation workStation)
        {
            var result = await workStationService.UpdateWorkStation(workStation);
            if (result != null)
            {
                return Results.Ok(result);
            }

            return Results.BadRequest("WorkStation could not be updated");
        }

        public async static Task<IResult> DeleteWorkStationAsync(IWorkStationService workStationService, Guid workStationId)
        {
            var result = await workStationService.RemoveWorkStationById(workStationId);
            if (result == true)
            {
                return Results.Ok();
            }

            return Results.NotFound();
        }

        public async static Task<IResult> CreateCellAsync(ICellService cellService, Cell cell)
        {
            var result = await cellService.AddCell(cell);
            if (result != null)
            {
                return Results.Ok(result);
            }

            return Results.BadRequest("Cell could not be created");
        }

        public async static Task<IResult> GetCellByIdAsync(ICellService cellService, Guid cellId)
        {
            var result = await cellService.GetCellById(cellId);
            if (result != null)
            {
                return Results.Ok(result);
            }

            return Results.NotFound();
        }

        public async static Task<IResult> GetAllCells(ICellService cellService)
        {
            var result = await cellService.GetAllCells();
            if (result != null)
            {
                return Results.Ok(result);
            }
            return Results.NotFound();
        }

        public async static Task<IResult> UpdateCellAsync(ICellService cellService, Cell cell)
        {
            var result = await cellService.UpdateCell(cell);
            if (result != null)
            {
                return Results.Ok(result);
            }

            return Results.BadRequest("Cell could not be updated");
        }

        public async static Task<IResult> DeleteCellAsync(ICellService cellService, Guid cellId)
        {
            var result = await cellService.RemoveCellById(cellId);
            if (result == true)
            {
                return Results.Ok();
            }

            return Results.NotFound();
        }
    }
}
