﻿using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartModel.Store.Combine
{
    internal sealed class ErrandPartCombiner : ICombinerManager<ErrandPart>
    {
        private readonly ErrandPartToErrandPartStatus _errandPartStatusToErrandPart;
        private readonly ErrandPartToErrand _errandToErrandPart;
        private readonly ErrandPartToPart _partToErrandPart;
        private readonly ErrandPartToUser _userToErrandPart;
        public ErrandPartCombiner(ErrandPartToUser userToErrandPart, ErrandPartToPart partToErrandPart, ErrandPartToErrand errandToErrandPart, ErrandPartToErrandPartStatus errandPartStatusToErrandPart)
        {
            _userToErrandPart = userToErrandPart;
            _partToErrandPart = partToErrandPart;
            _errandToErrandPart = errandToErrandPart;
            _errandPartStatusToErrandPart = errandPartStatusToErrandPart;
        }
        public bool IsCombined { get; private set; }
        public async Task Combine(bool shouldForce = false)
        {
            if (IsCombined || !shouldForce) return;
            List<Task> tasks = [];

            tasks.Add(_userToErrandPart.Combine());
            tasks.Add(_partToErrandPart.Combine());
            tasks.Add(_errandToErrandPart.Combine());
            tasks.Add(_errandPartStatusToErrandPart.Combine());

            await Task.WhenAll(tasks);
            IsCombined = true;
        }
    }
}