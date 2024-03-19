using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.OfferModel
{
    internal sealed class OfferProvider : IProvider<Offer>
    {
        private readonly DatabaseConnectionFactory _database;
        private const string _createSQL = @"
            INSERT INTO offers (offer_name, offer_number, parent)
            VALUES (@Name, @Number, @Parent)
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
        private const string _updateSQL = @"
            UPDATE offers
            SET
                offer_name = @Name,
                offer_number = @Number,
                parent = @Parent,
                active = @Active
            WHERE id = @Id
            ";
        private const string _deleteSQL = @"
            DELETE
            FROM offers
            WHERE id = @Id
            ";
        public OfferProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }
        #region CRUD
        public async Task<int> Create(Offer item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                //Name = item.Name,
                //Number = item.Number,
                //Parent = item.ParentId
            };
            await connection.ExecuteAsync(_createSQL, parameters);

            return 0;
        }
        public async Task<IEnumerable<Offer>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<OfferDTO> offerDTOs = await connection.QueryAsync<OfferDTO>(_getAllSQL);
            return offerDTOs.Select(ToModel);
        }
        public async Task<Offer> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id
            };
            OfferDTO? offerDTO = await connection.QuerySingleAsync<OfferDTO>(_getOneSQL, parameters);
            return ToModel(offerDTO);
        }
        public async Task Update(Offer item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                /*Id = item.Id,
                Name = item.Name,
                Number = item.Number,
                Parent = item.ParentId,
                Active = item.IsActive*/
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
        private static Offer ToModel(OfferDTO item)
        {
            return new Offer()
            {
                /*Id = (int)item.Id!,
                Name = item.Name,
                Number = item.Number,
                SapNumber = item.SapNumber,
                ParentId = item.Parent,
                IsActive = item.Active*/
            };
        }
    }
}