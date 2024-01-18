using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Features.Admin.Parts.Interfaces
{
    public interface IInputForm<T>
    {
        public void CleanForm();
        public bool IsDataValidate(T inputValue);
    }
}