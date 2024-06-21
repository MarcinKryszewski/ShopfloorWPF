using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.MessageModel
{
    internal sealed class MessageProvider : IProvider<Message>
    {
        private readonly DatabaseConnectionFactory _database;
        private const string _createSQL = @"
            INSERT INTO messages (text, receiver_id, read)
            VALUES (@Text, @ReceiverId, @Read)
            ";
        private const string _getOneSQL = @"
            SELECT
                id AS Id,
                text AS Text,
                receiver_id AS ReceiverId,
                read AS Read
            FROM messages
            WHERE id = @Id
            ";
        private const string _getAllSQL = @"
            SELECT
                id AS Id,
                text AS Text,
                receiver_id AS ReceiverId,
                read AS Read
            FROM messages
            ";
        private const string _updateSQL = @"
            UPDATE messages
            SET
                text = @Text,
                receiver_id = @ReceiverId,
                read = @Read
            WHERE id = @Id
            ";
        private const string _deleteSQL = @"
            DELETE
            FROM messages
            WHERE id = @Id
            ";
        public MessageProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }
        public async Task<int> Create(Message item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Text = item.Text,
                ReceiverId = item.ReceiverId,
                Read = item.WasRead
            };
            await connection.ExecuteAsync(_createSQL, parameters);

            return 0;
        }
        public async Task<IEnumerable<Message>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<MessageDTO> messageDTOs = await connection.QueryAsync<MessageDTO>(_getAllSQL);
            return messageDTOs.Select(ToModel);
        }
        public async Task<Message> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id
            };
            MessageDTO? messageDTO = await connection.QuerySingleAsync<MessageDTO>(_getOneSQL, parameters);
            return ToModel(messageDTO);
        }
        public async Task Update(Message item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = item.Id,
                Text = item.Text,
                ReceiverId = item.ReceiverId,
                Read = item.WasRead
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
        private static Message ToModel(MessageDTO item)
        {
            return new Message()
            {
                Id = item.Id,
                Text = item.Text,
                ReceiverId = item.ReceiverId,
                WasRead = item.Read
            };
        }
    }
}