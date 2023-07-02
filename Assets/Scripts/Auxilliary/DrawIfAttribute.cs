using System;
using UnityEngine;

namespace EditorAux {
    public enum ComparisonType {
        Equals = 1,
        NotEqual = 2,
        GreaterThan = 3,
        LessThan = 4,
        LesserOrEqual = 5,
        GreaterOrEqual = 6
    }

    public enum DisablingType {
        ReadOnly = 2,
        DontDraw = 3
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class DrawIfAttribute : PropertyAttribute
    {

        public string comparedPropertyName { get; private set; }
        public object comparedToValue { get; private set; }
        public ComparisonType comparisonType { get; private set; }
        public DisablingType disablingType { get; private set; }

        public DrawIfAttribute(string comparedPropertyName, object comparedToValue, ComparisonType comparisonType, DisablingType disablingType) {
            this.comparedPropertyName = comparedPropertyName;
            this.comparedToValue = comparedToValue;
            this.comparisonType = comparisonType;
            this.disablingType = disablingType;
        }
    }
}