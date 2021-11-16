using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentManager
{
    [System.AttributeUsage(System.AttributeTargets.Class |
                       System.AttributeTargets.Struct | System.AttributeTargets.Property)
]
    public class EditablePropertyAttribute : System.Attribute
    {
    }
}