using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartOfferModel
{
    internal sealed class ErrandPartOfferProvider : IProvider<ErrandPartOffer>
    {
        private readonly DatabaseConnectionFactory _database;
        private const string _createSQL = @"
            INSERT INTO errand_parts_offers (errand_part, offer)
            VALUES (@ErrandPartId, @OfferId)
            ";
        private const string _getOneSQL = @"
            SELECT
                id AS Id,
                errand_part AS ErrandPart,
                offer AS Offer
            FROM errand_parts_offers
            WHERE id = @Id
            ";
        private const string _getAllSQL = @"
            SELECT
                id AS Id,
                errand_part AS ErrandPart,
                offer AS Offer
            FROM errand_parts_offers
            ";
        private const string _updateSQL = @"
            UPDATE errand_parts_offers
            SET
                errand_part = @ErrandPartId,
                offer = @OfferId
            WHERE id = @Id
            ";
        private const string _deleteSQL = @"
            DELETE
            FROM errand_parts_offers
            WHERE id = @Id
            ";
        public ErrandPartOfferProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }
        #region CRUD
        public async Task<int> Create(ErrandPartOffer item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                ErrandPartId = item.ErrandPartId,
                OfferId = item.OfferId,
            };
            await connection.ExecuteAsync(_createSQL, parameters);

            return 0;
        }
        public async Task<IEnumerable<ErrandPartOffer>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<ErrandPartOfferDTO> errandPartOfferDTOs = await connection.QueryAsync<ErrandPartOfferDTO>(_getAllSQL);
            return errandPartOfferDTOs.Select(ToModel);
        }
        public async Task<ErrandPartOffer> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id
            };
            ErrandPartOfferDTO? errandPartOfferDTO = await connection.QuerySingleAsync<ErrandPartOfferDTO>(_getOneSQL, parameters);
            return ToModel(errandPartOfferDTO);
        }
        public async Task Update(ErrandPartOffer item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = item.Id,
                ErrandPartId = item.ErrandPartId,
                OfferId = item.OfferId,
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
        private static ErrandPartOffer ToModel(ErrandPartOfferDTO item)
        {
            return new ErrandPartOffer()
            {
                Id = (int)item.Id!,
                ErrandPartId = item.ErrandPartId,
                OfferId = item.OfferId
            };
        }
    }
}