using Database.Entity;
using ModuleUI.Exceptions;

namespace ModuleUI.Managers
{
    /*
    Создание связи между:
     1) выбранной записи БД и(или) номером активной вкладки Result(активной таблицей ДБ) 
     2) DialogManager
    */
    public static class DialogManagerCaller
    {
        private static Dictionary<Type, IDialogManager> managerForTypeContainers = new();
        private static Dictionary<int, IDialogManager> managerForIntContainers = new();

        public static void RegisterDialogManagerForEntity(IDialogManager dialogManager, Type entityType) => managerForTypeContainers.Add(entityType, dialogManager);

        public static void RegisterDialogManagerForTabNumber(IDialogManager dialogManager, int tabNumber) => managerForIntContainers.Add(tabNumber, dialogManager);

        public static IDialogManager GetDialogManager(this DbEntity entity)
        {
            IDialogManager? dialogManager;
            if (managerForTypeContainers.TryGetValue(entity.GetType(), out dialogManager)) return dialogManager;
            else throw new DialogManagerCallerRegistrationException($"There is no registered instance of DialogManager for DbEntity: {entity.GetType().Name}");
        }

        public static IDialogManager GetDialogManager(this int tabNumber)
        {
            IDialogManager? dialogManager;
            if (managerForIntContainers.TryGetValue(tabNumber, out dialogManager)) return dialogManager;
            else throw new DialogManagerCallerRegistrationException($"There is no registered instance of DialogManager for tabNumber: {tabNumber}");
        }
    }
}