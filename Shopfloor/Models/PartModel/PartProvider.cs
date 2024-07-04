using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.PartModel
{
    internal sealed class PartProvider : IProvider<Part>
    {
        private const string _createSQL = @"
            INSERT INTO parts (name_pl, name_original, type_id, indeks, number, details, producer_id, supplier_id, unit)
            VALUES (@NamePl, @NameOriginal, @TypeId, @Index, @Number, @Details, @ProducerId, @SupplierId, @Unit)
            ";
        private const string _deleteSQL = @"
            DELETE
            FROM parts
            WHERE id = @Id
            ";
        private const string _getAllActiveSQL = @"
            SELECT
                id AS Id,
                name_pl AS NamePl,
                name_original AS NameOriginal,
                type_id AS TypeId,
                indeks AS [Index],
                number AS Number,
                details AS Details,
                producer_id AS ProducerId,
                supplier_id AS SupplierId,
                unit AS Unit,
                storage_amount as StorageAmount,
                storage_value as StorageValue
            FROM parts
            WHERE active = TRUE
            ";
        private const string _getAllSQL = @"
            SELECT
                id AS Id,
                name_pl AS NamePl,
                name_original AS NameOriginal,
                type_id AS TypeId,
                indeks AS [Index],
                number AS Number,
                details AS Details,
                producer_id AS ProducerId,
                supplier_id AS SupplierId,
                unit AS Unit,
                storage_amount as StorageAmount,
                storage_value as StorageValue
            FROM parts
            ";
        private const string _getOneSQL = @"
            SELECT
                id AS Id,
                name_pl AS NamePl,
                name_original AS NameOriginal,
                type_id AS TypeId,
                indeks AS [Index],
                number AS Number,
                details AS Details,
                producer_id AS ProducerId,
                supplier_id AS SupplierId,
                unit AS Unit,
                storage_amount as StorageAmount,
                storage_value as StorageValue
            FROM parts
            WHERE id = @Id
            ";
        private const string _updateSQL = @"
            UPDATE parts
            SET
                name_pl = @NamePl,
                name_original = @NameOriginal,
                type_id = @TypeId,
                indeks = @Index,
                number = @Number,
                details = @Details,
                producer_id = @ProducerId,
                supplier_id = @SupplierId
            WHERE id = @Id
            ";
        private readonly DatabaseConnectionFactory _database;
        public PartProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }

        public async Task<int> Create(Part item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                NamePl = item.NamePl,
                NameOriginal = item.NameOriginal,
                TypeId = item.TypeId,
                Index = item.Index,
                Number = item.ProducerNumber,
                Details = item.Details,
                ProducerId = item.ProducerId,
                SupplierId = item.SupplierId,
                Unit = item.Unit,
            };
            await connection.ExecuteAsync(_createSQL, parameters);

            return 0;
        }

        public async Task Delete(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id,
            };
            await connection.ExecuteAsync(_deleteSQL, parameters);
        }
        public async Task<IEnumerable<Part>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<PartDto> partDTOs = await connection.QueryAsync<PartDto>(_getAllSQL);
            return partDTOs.Select(ToPart);
        }

        public async Task<IEnumerable<Part>> GetAllActive()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<PartDto> partDTOs = await connection.QueryAsync<PartDto>(_getAllActiveSQL);
            return partDTOs.Select(ToPart);
        }

        public async Task<Part> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id,
            };
            PartDto? partDTO = await connection.QuerySingleAsync<PartDto>(_getOneSQL, parameters);
            return ToPart(partDTO);
        }

        public async Task StorageUpdate(List<Part> parts)
        {
            if (parts.Count == 0)
            {
                return;
            }

            string sqlCommand;
            int batchSize = 100;
            int batches = (parts.Count + batchSize - 1) / batchSize;
            List<Task> tasks = [];

            using IDbConnection connection = _database.Connect();
            for (int batch = 1; batch <= batches; batch++)
            {
                int minIndex = (batch - 1) * batchSize;
                int maxIndex = Math.Min(batchSize, parts.Count - minIndex);
                sqlCommand = GetStorageUpdateBulkCommand(parts.GetRange(minIndex, maxIndex));
                tasks.Add(ExecuteUpdate(sqlCommand, connection));
            }
            if (tasks.Count > 0)
            {
                await Task.WhenAll(tasks);
            }
        }
        public async Task Update(Part item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = item.Id,
                NamePl = item.NamePl,
                NameOriginal = item.NameOriginal,
                TypeId = item.TypeId,
                Index = item.Index,
                Number = item.ProducerNumber,
                Details = item.Details,
                ProducerId = item.ProducerId,
                SupplierId = item.SupplierId,
            };
            await connection.ExecuteAsync(_updateSQL, parameters);
        }
        private static string GetStorageUpdateBulkCommand(IEnumerable<Part> parts)
        {
            string amounts = string.Empty;
            string values = string.Empty;
            string indexes = string.Empty;

            foreach (Part part in parts)
            {
                if (part.Index is null)
                {
                    continue;
                }

                if (part.Index == 0)
                {
                    continue;
                }

                int index = (int)part.Index;

                string indexText = index.ToString();
                string value = part.StorageValue?.ToString() ?? string.Empty;
                string amount = part.StorageAmount?.ToString() ?? string.Empty;

                amounts += $"WHEN [indeks] = {indexText} THEN '{amount}' ";
                values += $"WHEN [indeks] = {indexText} THEN '{value}' ";
                indexes += $"{indexText},";
            }
            indexes = indexes.Remove(indexes.Length - 1, 1);

            return @$"
            UPDATE parts
            SET
                storage_amount =
                    CASE
                        {amounts}
                    END,
                storage_value =
                    CASE
                        {values}
                    END
            WHERE [indeks] IN ({indexes});
            ";
        }
        private static Part ToPart(PartDto item)
        {
            return new Part()
            {
                Id = item.Id,
                NamePl = item.NamePl,
                NameOriginal = item.NameOriginal,
                TypeId = item.TypeId,
                Index = item.Index,
                ProducerNumber = item.Number,
                Details = item.Details,
                ProducerId = item.ProducerId,
                SupplierId = item.SupplierId,
                Unit = item.Unit,
            };
        }
        private async Task ExecuteUpdate(string command, IDbConnection connection)
        {
            await connection.ExecuteAsync(command, connection);
        }
    }
}