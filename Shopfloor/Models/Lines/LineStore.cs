using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Interfaces;

namespace Shopfloor.Models.Lines
{
    internal class LineStore : IStore<LineModel>
    {
        private readonly LineRepository _repository;
        private List<LineModel> _data = [];
        private bool _dataLoaded = false;

        public LineStore(LineRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<LineModel>> GetDataAsync()
        {
            if (!_dataLoaded)
            {
                _data = await LoadData();
            }
            return _data;
        }
        public async Task ReloadData()
        {
            List<LineModel> testData = [
                new LineModel { Id = 1, Name = "Linia Montażowa Silników" },
                new LineModel { Id = 2, Name = "Linia Spawania Podwozi" },
                new LineModel { Id = 3, Name = "Linia Lakierowania Karoserii" },
                new LineModel { Id = 4, Name = "Linia Montażu Układów Elektrycznych" },
                new LineModel { Id = 5, Name = "Linia Testów Bezpieczeństwa" }
            ];

            await Task.Delay(0);
            _data.Clear();
            _data.AddRange(testData);
        }
        private async Task<List<LineModel>> LoadData()
        {
            List<LineModel> data = await _repository.GetData();
            _dataLoaded = true;
            return data;
        }
    }
}