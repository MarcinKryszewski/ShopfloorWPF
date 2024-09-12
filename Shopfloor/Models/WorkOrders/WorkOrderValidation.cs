using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;

namespace Shopfloor.Models.WorkOrders
{
    internal class WorkOrderValidation : IModelValidation<WorkOrderCreationModel>
    {
        public void Validate(WorkOrderCreationModel item)
        {
            Task.WhenAll(ValidateDescription(item), ValidateLine(item));
        }
        private async Task ValidateLine(WorkOrderCreationModel item)
        {
            string propertyName = nameof(item.Line);
            LineModel? value = item.Line;

            item.ClearErrors(propertyName);
            List<Task> validations = [
                Line_CheckNull(item, propertyName, value)
            ];

            await Task.WhenAll(validations);
        }
        private Task Line_CheckNull(WorkOrderCreationModel item, string propertyName, LineModel? value)
        {
            if (value == null)
            {
                const string errorMessage = "Na jakiej linii jest to działanie?";
                item.AddError(propertyName, errorMessage);
            }
            return Task.CompletedTask;
        }
        private async Task ValidateDescription(WorkOrderCreationModel item)
        {
            string propertyName = nameof(item.Description);
            string value = item.Description;

            item.ClearErrors(propertyName);
            List<Task> validations = [
                Description_CheckNull(item, propertyName, value),
                Description_CheckEmpty(item, propertyName, value),
                Description_CheckLength(item, propertyName, value)
            ];

            await Task.WhenAll(validations);
        }
        private Task Description_CheckEmpty(WorkOrderCreationModel item, string propertyName, string? value)
        {
            if (value == null)
            {
                const string errorMessage = "Co mam zrobić z tym zadaniem?";
                item.AddError(propertyName, errorMessage);
            }
            return Task.CompletedTask;
        }
        private Task Description_CheckLength(WorkOrderCreationModel item, string propertyName, string value)
        {
            const int minDescriptionLength = 5;
            const string errorMessage = "Opis jest za krótki. Minimum 5 znaków.";

            if (value?.Trim().Length < minDescriptionLength)
            {
                item.AddError(propertyName, errorMessage);
            }
            return Task.CompletedTask;
        }
        private Task Description_CheckNull(WorkOrderCreationModel item, string propertyName, string? value)
        {
            if (value?.Trim().Length == 0)
            {
                const string errorMessage = "Co mam zrobić z tym zadaniem?";
                item.AddError(propertyName, errorMessage);
            }
            return Task.CompletedTask;
        }
    }
}