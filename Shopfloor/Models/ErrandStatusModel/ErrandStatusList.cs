namespace Shopfloor.Models.ErrandStatusModel
{
    public static class ErrandStatusList
    {
        public const string NoPartsList = "SPECYFIKACJA"; //Nowe zadanie
        public const string PartsListCompleted = "WYSPECYFIKOWANO"; //Zakończyła się praca mechanika, części zostały przypisane do zadania
        public const string NoPartsOnStock = "ZAMAWIANIE"; //Rozpoczyna się praca planisty - ofertowanie, zamawianie
        public const string PartsReady = "CZĘŚCI DOSTĘPNE"; //Części gotowe do dostarczenia dla mechanika, wszystkie są zarezerwowane na magazynie, można pobrać
        public const string TaskFinished = "ZAKOŃCZONO"; //Części dostarczone na warsztat
        public const string PartDeclined = "ODMOWA CZĘŚCI"; //Jakaś część została odrzucona
        public const string ErrandEdited = "ZMIENIONO"; //Edytowano zadanie
    }
}