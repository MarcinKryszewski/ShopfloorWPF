using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.WorkOrderParts
{
    internal class WorkOrderPartProvider : IProvider<WorkOrderPartModel, WorkOrderPartCreationModel>
    {
        public async Task<int> Create(WorkOrderPartCreationModel item)
        {
            return await Task.FromResult(new Random().Next(21, 100));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public async Task<IEnumerable<WorkOrderPartModel>> GetAll()
        {
            IEnumerable<WorkOrderPartModel> data = [
                new WorkOrderPartModel { Id = 1, PartId = 3, WorkOrderId = 8, Amount = 10, ValuePerPiece = 3.5 },
                new WorkOrderPartModel { Id = 2, PartId = 15, WorkOrderId = 12, Amount = 22 },
                new WorkOrderPartModel { Id = 3, PartId = 1, WorkOrderId = 5, Amount = 18, ValuePerPiece = 6.3 },
                new WorkOrderPartModel { Id = 4, PartId = 10, WorkOrderId = 4, Amount = 14 },
                new WorkOrderPartModel { Id = 5, PartId = 18, WorkOrderId = 20, Amount = 15, ValuePerPiece = 4.1 },
                new WorkOrderPartModel { Id = 6, PartId = 7, WorkOrderId = 15, Amount = 27, ValuePerPiece = 5.6 },
                new WorkOrderPartModel { Id = 7, PartId = 2, WorkOrderId = 3, Amount = 11 },
                new WorkOrderPartModel { Id = 8, PartId = 5, WorkOrderId = 1, Amount = 19, ValuePerPiece = 5.0 },
                new WorkOrderPartModel { Id = 9, PartId = 6, WorkOrderId = 14, Amount = 29 },
                new WorkOrderPartModel { Id = 10, PartId = 12, WorkOrderId = 16, Amount = 20, ValuePerPiece = 6.5 },
                new WorkOrderPartModel { Id = 11, PartId = 4, WorkOrderId = 9, Amount = 25, ValuePerPiece = 3.9 },
                new WorkOrderPartModel { Id = 12, PartId = 11, WorkOrderId = 14, Amount = 13 },
                new WorkOrderPartModel { Id = 13, PartId = 17, WorkOrderId = 19, Amount = 12, ValuePerPiece = 5.4 },
                new WorkOrderPartModel { Id = 14, PartId = 8, WorkOrderId = 18, Amount = 26, ValuePerPiece = 7.2 },
                new WorkOrderPartModel { Id = 15, PartId = 13, WorkOrderId = 10, Amount = 17 },
                new WorkOrderPartModel { Id = 16, PartId = 14, WorkOrderId = 6, Amount = 24 },
                new WorkOrderPartModel { Id = 17, PartId = 2, WorkOrderId = 13, Amount = 30, ValuePerPiece = 6.1 },
                new WorkOrderPartModel { Id = 18, PartId = 19, WorkOrderId = 5, Amount = 23 },
                new WorkOrderPartModel { Id = 19, PartId = 3, WorkOrderId = 15, Amount = 11 },
                new WorkOrderPartModel { Id = 20, PartId = 10, WorkOrderId = 11, Amount = 28, ValuePerPiece = 4.7 },
                new WorkOrderPartModel { Id = 21, PartId = 16, WorkOrderId = 4, Amount = 21, ValuePerPiece = 4.0 },
                new WorkOrderPartModel { Id = 22, PartId = 12, WorkOrderId = 9, Amount = 9 },
                new WorkOrderPartModel { Id = 23, PartId = 14, WorkOrderId = 8, Amount = 12 },
                new WorkOrderPartModel { Id = 24, PartId = 18, WorkOrderId = 2, Amount = 17, ValuePerPiece = 6.6 },
                new WorkOrderPartModel { Id = 25, PartId = 9, WorkOrderId = 13, Amount = 13 },
                new WorkOrderPartModel { Id = 26, PartId = 4, WorkOrderId = 1, Amount = 23, ValuePerPiece = 8.0 },
                new WorkOrderPartModel { Id = 27, PartId = 2, WorkOrderId = 17, Amount = 10, ValuePerPiece = 4.3 },
                new WorkOrderPartModel { Id = 28, PartId = 11, WorkOrderId = 19, Amount = 27 },
                new WorkOrderPartModel { Id = 29, PartId = 16, WorkOrderId = 7, Amount = 18, ValuePerPiece = 7.5 },
                new WorkOrderPartModel { Id = 30, PartId = 7, WorkOrderId = 3, Amount = 19 },
                new WorkOrderPartModel { Id = 31, PartId = 5, WorkOrderId = 13, Amount = 29, ValuePerPiece = 5.7 },
                new WorkOrderPartModel { Id = 32, PartId = 12, WorkOrderId = 16, Amount = 15 },
                new WorkOrderPartModel { Id = 33, PartId = 6, WorkOrderId = 18, Amount = 26 },
                new WorkOrderPartModel { Id = 34, PartId = 15, WorkOrderId = 7, Amount = 14, ValuePerPiece = 4.6 },
                new WorkOrderPartModel { Id = 35, PartId = 9, WorkOrderId = 20, Amount = 22 },
                new WorkOrderPartModel { Id = 36, PartId = 1, WorkOrderId = 10, Amount = 25 },
                new WorkOrderPartModel { Id = 37, PartId = 4, WorkOrderId = 14, Amount = 10 },
                new WorkOrderPartModel { Id = 38, PartId = 17, WorkOrderId = 6, Amount = 12, ValuePerPiece = 8.3 },
                new WorkOrderPartModel { Id = 39, PartId = 10, WorkOrderId = 11, Amount = 20 },
                new WorkOrderPartModel { Id = 40, PartId = 13, WorkOrderId = 19, Amount = 16, ValuePerPiece = 5.5 },
            ];

            return await Task.FromResult(data);
        }
        public Task<WorkOrderPartModel> GetById(int id)
        {
            throw new NotImplementedException();
        }
        public Task Update(WorkOrderPartModel item)
        {
            return Task.CompletedTask;
        }
    }
}