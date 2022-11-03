using System;
using UnityEngine;

namespace EditorHelpers
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited =false)]
    public class ShowIfAttribute : PropertyAttribute
    {
        public ActionOnConditionFail Action { get; private set; }
        public ConditionOperator Operator { get; private set; }
        public string[] Conditions { get; private set; }

        public ShowIfAttribute(ActionOnConditionFail action, ConditionOperator conditionOperator, params string[] conditions)
        {
            Action = action;
            Operator = conditionOperator;
            Conditions = conditions;
        }
    }

    public enum ActionOnConditionFail
    {
        HideAttribute,
        DisableAttribute,
    }
    public enum ConditionOperator
    {
        And,
        Or
    }
}