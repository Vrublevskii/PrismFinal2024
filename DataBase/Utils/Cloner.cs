using System.Reflection;

namespace Database.Utils
{
    public static class Cloner
    {
        public static void ObjectCloner<T>(this T cloneObject, T objectToClone)
        {
            if (cloneObject == null || objectToClone == null) return;

            Type typeObjectToClone = objectToClone.GetType();
            Type typeCloneObject = cloneObject.GetType();
            PropertyInfo[] propertyInfos = typeObjectToClone.GetProperties();

            foreach (var item in propertyInfos)
            {
                string name = item.Name;
                var propertyInfo = typeCloneObject.GetProperty(name);
                propertyInfo?.SetValue(cloneObject, item.GetValue(objectToClone));
            }
        }

        public static void ObjectClonerWithoutId<T>(this T cloneObject, T objectToClone)
        {
            if (cloneObject == null || objectToClone == null) return;

            Type typeObjectToClone = objectToClone.GetType();
            Type typeCloneObject = cloneObject.GetType();
            PropertyInfo[] propertyInfos = typeObjectToClone.GetProperties();

            foreach (var item in propertyInfos)
            {
                string name = item.Name;
                if (name == "Id") continue;
                var propertyInfo = typeCloneObject.GetProperty(name);
                propertyInfo?.SetValue(cloneObject, item.GetValue(objectToClone));
            }
        }

        public static void ObjectClonerWithout<T>(this T cloneObject, T objectToClone, params string[] NamesOfSkipableProps)
        {
            if (cloneObject == null || objectToClone == null) return;

            Type typeObjectToClone = objectToClone.GetType();
            Type typeCloneObject = cloneObject.GetType();
            PropertyInfo[] propertyInfos = typeObjectToClone.GetProperties();

            foreach (var item in propertyInfos)
            {
                string name = item.Name;
                if (IsPropertySkipable(name, NamesOfSkipableProps))
                {
                    continue;
                }
                var propertyInfo = typeCloneObject.GetProperty(name);
                propertyInfo?.SetValue(cloneObject, item.GetValue(objectToClone));
            }

            bool IsPropertySkipable(string name, params string[] NamesOfSkipableProps)
            {
                bool propIsSkipable = false;
                foreach (var nameSkipable in NamesOfSkipableProps)
                {
                    if (name == nameSkipable)
                    {
                        propIsSkipable = true;
                    }
                }

                return propIsSkipable;
            }
        }
    }

}
