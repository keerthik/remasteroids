using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace EditorAux {
    public static class Properties {
        public static object GetValue(this SerializedProperty prop) {
            return prop.type switch {
                "bool"  => prop.boolValue,
                "float" => prop.floatValue,
                "int"   => prop.intValue,
                "Enum"  => prop.enumValueFlag,
                _ => null,
            };
        }
    }
}
