using System;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Persons;

namespace Shopfloor.Models.Instructions
{
    internal class Instruction : IModel
    {
        public Person? AcceptedBy { get; set; }
        public int AcceptedById { get; set; }
        public string Name { get; } = string.Empty;
        public Person? Author { get; set; }
        public int AuthorId { get; init; }
        public DateTime CreationDate { get; set; }
        public string FilePath { get; set; } = string.Empty;
        required public int Id { get; init; }
        public bool IsAccepted { get; set; } = false;
        public int Version { get; set; }
        public void SetValues<T>(IModelCreationModel<T> data)
            where T : IModel
        {
            if (data is not InstructionCreation)
            {
                return;
            }

            InstructionCreation creation = (InstructionCreation)data;
        }
    }
}