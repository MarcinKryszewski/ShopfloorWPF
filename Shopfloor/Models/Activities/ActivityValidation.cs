using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.ActivityTypes;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Workshops;

namespace Shopfloor.Models.Activities
{
    internal class ActivityValidation : IModelValidation<ActivityCreation>
    {
        public void Validate(ActivityCreation item)
        {

            List<Task> tasks = [];

            tasks.Add(ValidateDescription(item));
            tasks.Add(ValidateMachine(item));
            tasks.Add(ValidateWorkshop(item));
            tasks.Add(ValidateType(item));
            tasks.Add(ValidateOccuranceValue(item));

            Task.WhenAll(tasks);
        }
        public async Task ValidateOccuranceValue(ActivityCreation item)
        {
            string propertyName = nameof(ActivityCreation.OccuranceValue);
            int value = item.OccuranceValue;
            item.ClearErrors(propertyName);
            List<Task> tasks = [];

            tasks.Add(OccuranceValue_CheckMinimumValue(item, propertyName, value));

            await Task.WhenAll(tasks);
        }
        public async Task ValidateWorkshop(ActivityCreation item)
        {
            string propertyName = nameof(ActivityCreation.Workshop);
            Workshop? value = item.Workshop;
            item.ClearErrors(propertyName);
            List<Task> tasks = [];

            tasks.Add(Workshop_CheckNull(item, propertyName, value));

            await Task.WhenAll(tasks);
        }
        public async Task ValidateType(ActivityCreation item)
        {
            string propertyName = nameof(ActivityCreation.Type);
            ActivityType? value = item.Type;
            item.ClearErrors(propertyName);
            List<Task> tasks = [];

            tasks.Add(Type_CheckNull(item, propertyName, value));

            await Task.WhenAll(tasks);
        }
        public async Task ValidateDescription(ActivityCreation item)
        {
            string propertyName = nameof(ActivityCreation.Description);
            string value = item.Description;
            item.ClearErrors(propertyName);
            List<Task> tasks = [];

            tasks.Add(Description_CheckNull(item, propertyName, value));
            tasks.Add(Description_CheckEmpty(item, propertyName, value));
            tasks.Add(Description_CheckLength(item, propertyName, value));

            await Task.WhenAll(tasks);
        }
        public async Task ValidateMachine(ActivityCreation item)
        {
            string propertyName = nameof(ActivityCreation.Machine);
            Machine? value = item.Machine;
            item.ClearErrors(propertyName);
            List<Task> tasks = [];

            tasks.Add(Machine_CheckNull(item, propertyName, value));

            await Task.WhenAll(tasks);
        }
        private Task Description_CheckEmpty(ActivityCreation item, string propertyName, string? value)
        {
            const string errorText = "Wprowadź opis";
            if (value == null)
            {
                item.AddError(propertyName, errorText);
            }
            return Task.CompletedTask;
        }
        private Task Description_CheckLength(ActivityCreation item, string propertyName, string value)
        {
            int minDescriptionLength = 5;
            const string errorText = "Opis jest za krótki. Minimum 5 znaków.";
            if (value?.Trim().Length < minDescriptionLength)
            {
                item.AddError(propertyName, errorText);
            }
            return Task.CompletedTask;
        }
        private Task Description_CheckNull(ActivityCreation item, string propertyName, string? value)
        {
            const string errorText = "Opis nie może być pusty";
            if (value?.Trim().Length == 0)
            {
                item.AddError(propertyName, errorText);
            }
            return Task.CompletedTask;
        }
        private Task Machine_CheckNull(ActivityCreation item, string propertyName, Machine? value)
        {
            const string errorText = "Wprowadź maszynę";
            if (value == null)
            {
                item.AddError(propertyName, errorText);
            }
            return Task.CompletedTask;
        }
        private Task Type_CheckNull(ActivityCreation item, string propertyName, ActivityType? value)
        {
            const string errorText = "Wprowadź rodzaj działania";
            if (value == null)
            {
                item.AddError(propertyName, errorText);
            }
            return Task.CompletedTask;
        }
        private Task Workshop_CheckNull(ActivityCreation item, string propertyName, Workshop? value)
        {
            const string errorText = "Wprowadź warsztat odpowiedzialny";
            if (value == null)
            {
                item.AddError(propertyName, errorText);
            }
            return Task.CompletedTask;
        }
        private Task OccuranceValue_CheckMinimumValue(ActivityCreation item, string propertyName, int value)
        {
            const string errorText = "Minimum 1";
            int minimumValue = 1;
            if (value < minimumValue)
            {
                item.AddError(propertyName, errorText);
            }
            return Task.CompletedTask;
        }
    }
}