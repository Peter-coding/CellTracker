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

            app.MapPost($"{path}/Factory/Add", CreateFactoryAsync);
            app.MapGet($"{path}/Factory/Get", GetFactoryByIdAsync);
            app.MapGet($"{path}/Factory/GetAll", GetAllFactories);
            app.MapPut($"{path}/Factory/Update", UpdateFactoryAsync);
            app.MapDelete($"{path}/Factory/Delete", DeleteFactoryAsync);

            app.MapPost($"{path}/ProdLine/Add", CreateProductionLineAsync);
            app.MapGet($"{path}/ProdLine/Get", GetProductionLineByIdAsync);
            app.MapGet($"{path}/ProdLine/GetAll", GetAllProductionLines);
            app.MapPut($"{path}/ProdLine/Update", UpdateProductionLineAsync);
            app.MapDelete($"{path}/ProdLine/Delete", DeleteProductionLineAsync);
            app.MapPut($"{path}/ProdLine/SetProdLineStatus", SetProdLineStatus);
            app.MapGet($"{path}/ProdLine/CellsInProdLine", GetCellsInProdLine);

            app.MapPost($"{path}/WorkStation/Add", CreateWorkStationAsync);
            app.MapGet($"{path}/WorkStation/Get", GetWorkStationByIdAsync);
            app.MapGet($"{path}/WorkStation/GetAll", GetAllWorkStations);
            app.MapPut($"{path}/WorkStation/Update", UpdateWorkStationAsync);
            app.MapDelete($"{path}/WorkStation/Delete", DeleteWorkStationAsync);

            app.MapPost($"{path}/Cell/Add", CreateCellAsync);
            app.MapGet($"{path}/Cell/Get", GetCellByIdAsync);
            app.MapGet($"{path}/Cell/GetAll", GetAllCells);
            app.MapPut($"{path}/Cell/Update", UpdateCellAsync);
            app.MapDelete($"{path}/Cell/Delete", DeleteCellAsync);
            app.MapGet($"{path}/Cell/WorkStationsInCell", GetWorkStationsInCell);
        }

        public async static Task<IResult> CreateFactoryAsync(IFactoryService factoryService, CreateFactoryDto factoryDto)
        {
            var created = await factoryService.AddFactory(factoryDto);
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

        public async static Task<IResult> UpdateFactoryAsync(IFactoryService factoryService, UpdateFactoryDto factoryDto)
        {
            var result = await factoryService.UpdateFactory(factoryDto);
            if (result != null)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest("Factory could not be updated");
        }

        public async static Task<IResult> DeleteFactoryAsync(IFactoryService factoryService, Guid factoryId)
        {
            var result = await factoryService.RemoveFactoryById(factoryId);
            if (result)
            {
                return Results.Ok();
            }

            return Results.NotFound();
        }

        public async static Task<IResult> CreateProductionLineAsync(IProductionLineService productionLineService, CreateProductionLineDto productionLineDto)
        {
            var result = await productionLineService.AddProductionLine(productionLineDto);
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

        public async static Task<IResult> UpdateProductionLineAsync(IProductionLineService productionLineService, UpdateProductionLineDto productionLineDto)
        {
            var result = await productionLineService.UpdateProductionLine(productionLineDto);
            if (result != null)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest("Production line could not be updated");
        }

        public async static Task<IResult> GetCellsInProdLine(IProductionLineService productionLineService, Guid productionLineId)
        {
            var result = await productionLineService.GetCellsInProdLine(productionLineId);
            if (result != null)
            {
                return Results.Ok(result);
            }
            return Results.NotFound();
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

        public async static Task<IResult> SetProdLineStatus(IProductionLineService productionLineService, Guid productionLineId, ProductionLineStatus status)
        {
            var result = await productionLineService.SetProductionLineStatus(productionLineId, status);
            if (result != null)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest("Production line status could not be updated");
        }
        public async static Task<IResult> CreateWorkStationAsync(IWorkStationService workStationService, CreateWorkStationDto workStationDto)
        {
            var result = await workStationService.AddWorkStation(workStationDto);
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

        public async static Task<IResult> UpdateWorkStationAsync(IWorkStationService workStationService, UpdateWorkStationDto workStationDto)
        {
            var result = await workStationService.UpdateWorkStation(workStationDto);
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

        public async static Task<IResult> CreateCellAsync(ICellService cellService, CreateCellDto cellDto)
        {
            var result = await cellService.AddCell(cellDto);
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

        public async static Task<IResult> UpdateCellAsync(ICellService cellService, UpdateCellDto cellDto)
        {
            var result = await cellService.UpdateCell(cellDto);
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

        public async static Task<IResult> GetWorkStationsInCell(ICellService cellService, Guid cellId)
        {
            var result = await cellService.GetWorkStationsOfCellAsync(cellId);
            if (result != null)
            {
                return Results.Ok(result);
            }
            return Results.NotFound();
        }
    }
}
