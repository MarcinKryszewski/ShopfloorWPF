using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.OfferModel
{
    internal sealed class OfferProvider : IProvider<Offer>
    {
        private const string _createSQL = @"
            INSERT INTO offers (offer_name, offer_number, parent)
            VALUES (@Name, @Number, @Parent)
            ";
        private const string _dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        private const string _deleteSQL = @"
            DELETE
            FROM offers
            WHERE id = @Id
            ";
        private const string _getAllSQL = @"
            SELECT
                id AS Id,
                offer_name AS Name,
                offer_number AS Number,
                sap_number AS SapNumber,
                parent AS Parent,
                active AS Active
            FROM offers
            ";
        private const string _getOneSQL = @"
            SELECT
                id AS Id,
                offer_name AS Name,
                offer_number AS Number,
                sap_number AS SapNumber,
                parent AS Parent,
                active AS Active
            FROM offers
            WHERE id = @Id
            ";
        private const string _updateSQL = @"
            UPDATE offers
            SET
                offer_name = @Name,
                offer_number = @Number,
                parent = @Parent,
                active = @Active
            WHERE id = @Id
            ";
        private readonly DatabaseConnectionFactory _database;
        public OfferProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }

        public async Task<int> Create(Offer item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                CreateDate = item.CreateDate.ToString(_dateTimeFormat),
                CreateBy = item.CreateById,
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
        public async Task<IEnumerable<Offer>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<OfferDto> offerDTOs = await connection.QueryAsync<OfferDto>(_getAllSQL);
            return offerDTOs.Select(ToModel);
        }
        public async Task<Offer> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id,
            };
            OfferDto? offerDTO = await connection.QuerySingleAsync<OfferDto>(_getOneSQL, parameters);
            return ToModel(offerDTO);
        }
        public async Task Update(Offer item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = item.Id,
                CreateDate = item.CreateDate.ToString(_dateTimeFormat),
                CreateBy = item.CreateById,
            };
            await connection.ExecuteAsync(_updateSQL, parameters);
        }
        private static Offer ToModel(OfferDto item)
        {
            return new Offer()
            {
                Id = (int)item.Id!,
                CreateDate = item.CreateDate,
                CreateById = item.CreateById,
            };
        }
    }
}