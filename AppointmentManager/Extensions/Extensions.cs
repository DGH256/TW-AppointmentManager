using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using AppointmentManager.UserManagement;
using System.Reflection;

namespace AppointmentManager
{
    public static partial class Extensions
    {
        public static Dictionary<Type, List<PropertyInfo>> cachedPropertyInfos = new Dictionary<Type, List<PropertyInfo>>();

        /// <summary>
        /// Copies the property values from _source_ object to _destination_ object
        /// Set includingIDS to true if you want it to copy id's too like id, id_user, id_appointment
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="includingIDs"></param>
        public static void CopyProperties(this object destination, object source)
        {
            // If any this null throw an exception
            if (source == null || destination == null)
                throw new Exception("Source or/and Destination Objects are null");
            // Getting the Types of the objects
            Type typeDest = destination.GetType();
            Type typeSrc = source.GetType();


            // Iterate the Properties of the source instance and  
            // populate them from their desination counterparts  
            List<PropertyInfo> srcProps;

            if (!cachedPropertyInfos.ContainsKey(typeSrc))
            {
                var props_to_cache = typeSrc.GetProperties().Where(p => Attribute.IsDefined(p, typeof(EditablePropertyAttribute))).ToList();
                cachedPropertyInfos.Add(typeSrc, props_to_cache);
            }

            srcProps = cachedPropertyInfos[typeSrc];

            foreach (PropertyInfo srcProp in srcProps)
            {
                if (!srcProp.CanRead)
                {
                    continue;
                }

                PropertyInfo targetProperty = typeDest.GetProperty(srcProp.Name);
                if (targetProperty == null)
                {
                    continue;
                }
                if (!targetProperty.CanWrite)
                {
                    continue;
                }
                if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true).IsPrivate)
                {
                    continue;
                }
                if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
                {
                    continue;
                }
                if (!targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType))
                {
                    continue;
                }
                // Passed all tests, lets set the value
                targetProperty.SetValue(destination, srcProp.GetValue(source, null), null);
            }
        }
    }
}