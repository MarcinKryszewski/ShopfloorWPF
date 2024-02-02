using Dapper;
using Shopfloor.Database;

using Shopfloor.Interfaces;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.PartModel
{
    public class PartProvider : IProvider<Part>
    {
        private readonly DatabaseConnectionFactory _database;

        #region SQLCommands

        private const string _createSQL = @"
            INSERT INTO parts (name_pl, name_original, type_id, indeks, number, details, producer_id, supplier_id)
            VALUES (@NamePl, @NameOriginal, @TypeId, @Indeks, @Number, @Details, @ProducerId, @SupplierId)
            ";

        private const string _getOneSQL = @"
            SELECT *
            FROM parts
            WHERE id = @Id
            ";

        private const string _getAllSQL = @"
            SELECT *
            FROM parts
            ";

        private const string _getAllActiveSQL = @"
            SELECT *
            FROM parts
            WHERE active = TRUE
            ";

        private const string _updateSQL = @"
            UPDATE parts
            SET
                name_pl = @NamePl,
                name_original = @NameOriginal,
                type_id = @TypeId,
                indeks = @Indeks,
                number = @Number,
                details = @Details,
                producer_id = @ProducerId,
                supplier_id = @SupplierId
            WHERE id = @Id
            ";

        private const string _deleteSQL = @"
            DELETE
            FROM parts
            WHERE id = @Id
            ";

        #endregion SQLCommands

        public PartProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }

        #region CRUD

        public async Task<int> Create(Part item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                NamePl = item.NamePl,
                NameOriginal = item.NameOriginal,
                TypeId = item.TypeId,
                Indeks = item.Index,
                Number = item.Number,
                Details = item.Details,
                ProducerId = item.ProducerId,
                SupplierId = item.SupplierId
            };
            await connection.ExecuteAsync(_createSQL, parameters);

            return 0;
        }

        public async Task<IEnumerable<Part>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<PartDTO> partDTOs = await connection.QueryAsync<PartDTO>(_getAllSQL);
            return partDTOs.Select(ToPart);
        }

        public async Task<IEnumerable<Part>> GetAllActive()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<PartDTO> partDTOs = await connection.QueryAsync<PartDTO>(_getAllActiveSQL);
            return partDTOs.Select(ToPart);
        }

        public async Task<Part> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id
            };
            PartDTO? partDTO = await connection.QuerySingleAsync<PartDTO>(_getOneSQL, parameters);
            return ToPart(partDTO);
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
                Indeks = item.Index,
                Number = item.Number,
                Details = item.Details,
                ProducerId = item.ProducerId,
                SupplierId = item.SupplierId
            };
            await connection.ExecuteAsync(_updateSQL, parameters);
        }

        public async Task Delete(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id
            };
            await connection.ExecuteAsync(_deleteSQL, parameters);
        }

        #endregion CRUD

        private static Part ToPart(PartDTO item)
        {
            return new Part(
                item.Id,
                item.Name_Pl,
                item.Name_Original,
                item.Type_Id,
                item.Indeks,
                item.Number,
                item.Details,
                item.Producer_Id,
                item.Supplier_Id,
                item.Unit);
        }
    }
}