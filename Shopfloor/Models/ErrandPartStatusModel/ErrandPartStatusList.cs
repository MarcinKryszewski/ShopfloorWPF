using System.Collections.Generic;

namespace Shopfloor.Models.ErrandPartStatusModel
{
    internal static class ErrandPartStatusList
    {
        public static readonly Dictionary<int, string> Status = new()
        {
            [-1] = "ERROR",
            [0] = "OFERTOWANIE",
            [1] = "ZATWIERDZANIE",
            [2] = "KOREKCJA",
            [3] = "ZAMAWIANIE",
            [4] = "DOSTARCZANIE",
            [5] = "REZERWOWANIE",
            [6] = "POBIERANIE",
            [7] = "ZAKOŃCZONE",
            [8] = "WSTRZYMANE",
            [9] = "ANULOWANE",
        };
    }
}