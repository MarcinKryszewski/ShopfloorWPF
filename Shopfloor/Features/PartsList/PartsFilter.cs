using System;
using Shopfloor.Shared;

namespace Shopfloor.Features.PartsList
{
    internal class PartsFilter
    {
        private string _line = string.Empty;
        private string _machine = string.Empty;
        private string _type = string.Empty;
        private string _manufacturer = string.Empty;
        private string _number = string.Empty;
        private string _index = string.Empty;
        private bool _confirmed;
        public event EventHandler? FiltersChanged;
        public string Line
        {
            get => _line;
            set
            {
                _line = value;
                OnFiltersChanged(EventArgs.Empty);
            }
        }
        public string Machine
        {
            get => _machine;
            set
            {
                _machine = value;
                OnFiltersChanged(EventArgs.Empty);
            }
        }
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnFiltersChanged(EventArgs.Empty);
            }
        }
        public string Manufacturer
        {
            get => _manufacturer;
            set
            {
                _manufacturer = value;
                OnFiltersChanged(EventArgs.Empty);
            }
        }
        public string Number
        {
            get => _number;
            set
            {
                _number = value;
                OnFiltersChanged(EventArgs.Empty);
            }
        }
        public string Index
        {
            get => _index;
            set
            {
                _index = value;
                OnFiltersChanged(EventArgs.Empty);
            }
        }
        public bool Confirmed
        {
            get => _confirmed;
            set
            {
                _confirmed = value;
                OnFiltersChanged(EventArgs.Empty);
            }
        }
        protected void OnFiltersChanged(EventArgs e) => FiltersChanged?.Invoke(this, e);
    }
}