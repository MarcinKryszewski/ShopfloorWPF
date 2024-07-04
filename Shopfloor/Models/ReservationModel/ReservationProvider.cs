using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ReservationModel
{
    internal sealed class ReservationProvider : IProvider<Reservation>
    {
        private const string _createSQL = @"
            INSERT INTO reservations (errand_part_id, amount, create_date, expiration_date, completed)
            VALUES @ErrandPartId, @Amount, @CreateDate, @ExpirationDate, @Completed)
            ";
        private const string _dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        private const string _deleteSQL = @"
            DELETE
            FROM reservations
            WHERE id = @Id
            ";
        private const string _getAllSQL = @"
            SELECT
                id AS Id,
                errand_part_id AS ErrandPartId,
                amount AS Amount,
                create_date AS CreateDate,
                expiration_date AS ExpirationDate,
                completed AS Completed
            FROM reservations
            ";
        private const string _getOneSQL = @"
            SELECT
                id AS Id,
                errand_part_id AS ErrandPartId,
                amount AS Amount,
                create_date AS CreateDate,
                expiration_date AS ExpirationDate,
                completed AS Completed
            FROM reservations
            WHERE id = @Id
            ";
        private const string _updateSQL = @"
            UPDATE reservations
            SET
                errand_part_id = @ErrandPartId,
                amount = @Amount,
                create_date = @CreateDate,
                expiration_date = @ExpirationDate,
                completed = @Completed
            WHERE id = @Id
            ";
        private readonly DatabaseConnectionFactory _database;
        public ReservationProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }
        public async Task<int> Create(Reservation item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                ErrandPartId = item.ErrandPartId,
                Amount = item.Amount,
                CreateDate = item.CreateDate.ToString(_dateTimeFormat),
                ExpirationDate = item.ExpirationDate.ToString(_dateTimeFormat),
                Completed = item.Completed,
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
        public async Task<IEnumerable<Reservation>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<ReservationDto> reservationDTOs = await connection.QueryAsync<ReservationDto>(_getAllSQL);
            return reservationDTOs.Select(ToModel);
        }
        public async Task<Reservation> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id,
            };
            ReservationDto? reservationDTO = await connection.QuerySingleAsync<ReservationDto>(_getOneSQL, parameters);
            return ToModel(reservationDTO);
        }
        public async Task Update(Reservation item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = item.Id,
                ErrandPartId = item.ErrandPartId,
                Amount = item.Amount,
                CreateDate = item.CreateDate,
                ExpirationDate = item.ExpirationDate,
                Completed = item.Completed,
            };
            await connection.ExecuteAsync(_updateSQL, parameters);
        }
        private static Reservation ToModel(ReservationDto item)
        {
            return new Reservation()
            {
                Id = item.Id,
                ErrandPartId = item.ErrandPartId,
                Amount = item.Amount,
                CreateDate = item.CreateDate,
                ExpirationDate = item.ExpirationDate,
                Completed = item.Completed,
            };
        }
    }
}