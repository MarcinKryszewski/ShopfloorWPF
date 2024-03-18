namespace Shopfloor.Models.MessageModel
{
    internal sealed class MessageProvider : IProvider<Message>
    {
        private readonly DatabaseConnectionFactory _database;
        private const string _createSQL = @"
            INSERT INTO messages (message_name, message_number, parent)
            VALUES (@Name, @Number, @Parent)
            ";
        private const string _getOneSQL = @"
            SELECT
                id AS Id,
                message_name AS Name,
                message_number AS Number,
                sap_number AS SapNumber,
                parent AS Parent,
                active AS Active
            FROM messages
            WHERE id = @Id
            ";
        private const string _getAllSQL = @"
            SELECT
                id AS Id,
                message_name AS Name,
                message_number AS Number,
                sap_number AS SapNumber,
                parent AS Parent,
                active AS Active
            FROM messages
            ";
        private const string _updateSQL = @"
            UPDATE messages
            SET
                message_name = @Name,
                message_number = @Number,
                parent = @Parent,
                active = @Active
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
        #region CRUD
        public async Task<int> Create(Message item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Name = item.Name,
                Number = item.Number,
                Parent = item.ParentId
            };
            await connection.ExecuteAsync(_createSQL, parameters);

            return 0;
        }
        public async Task<IEnumerable<Message>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<MessageDTO> messageDTOs = await connection.QueryAsync<MessageDTO>(_getAllSQL);
            return messageDTOs.Select(ToMessage);
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
                Name = item.Name,
                Number = item.Number,
                Parent = item.ParentId,
                Active = item.IsActive
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
        private static Message ToModel(MessageDTO item)
        {
            return new Message()
            {
                Id = (int)item.Id!,
                Name = item.Name,
                Number = item.Number,
                SapNumber = item.SapNumber,
                ParentId = item.Parent,
                IsActive = item.Active
            };
        }
    }
}