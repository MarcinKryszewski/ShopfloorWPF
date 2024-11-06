using System;
using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Persons;

namespace Shopfloor.Models.Instructions
{
    internal class InstructionCreation : ModelValidationBase, IModelCreationModel<Instruction>
    {
        public Person? AcceptedBy { get; set; }
        public int AcceptedById { get; set; }
        public Person? Author { get; set; }
        public int AuthorId { get; init; }
        public DateTime CreationDate { get; set; }
        public string FilePath { get; set; } = string.Empty;
        required public int Id { get; init; }
        public bool IsAccepted { get; set; } = false;
        public int Version { get; set; }
        public Instruction CreateModel(int id)
        {
            return new Instruction
            {
                Id = id,
                AcceptedBy = AcceptedBy,
                AcceptedById = AcceptedById,
                Author = Author,
                AuthorId = AuthorId,
                CreationDate = CreationDate,
                FilePath = FilePath,
                IsAccepted = IsAccepted,
                Version = Version,
            };
        }
    }
}