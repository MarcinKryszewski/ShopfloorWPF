using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.WorkOrders;

namespace Shopfloor.Models.Lines
{
    internal class LineRepository : IRepository<LineModel>
    {
        public Task<LineModel> Create()
        {
            throw new NotImplementedException();
        }

        public Task<LineModel> Delete()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LineModel>> GetData()
        {
            List<LineModel> data = [
                new LineModel { Id = 1, Name = "Linia Montażowa Silników" },
                new LineModel { Id = 2, Name = "Linia Spawania Podwozi" },
                new LineModel { Id = 3, Name = "Linia Lakierowania Karoserii" },
                new LineModel { Id = 4, Name = "Linia Montażu Układów Elektrycznych" },
                new LineModel { Id = 5, Name = "Linia Testów Bezpieczeństwa" }
            ];

            await Task.Delay(0);
            return data;
        }

        public Task<LineModel> Update()
        {
            throw new NotImplementedException();
        }
    }
}