using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace EditorHelpers
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute), true)]
    public class ShowIfAttributeDrawer : PropertyDrawer
    {
        #region Reflection helpers.
        private static MethodInfo GetMethod(object target, string methodName)
        {
            return GetAllMethods(target, m => m.Name.Equals(methodName, 
                      StringComparison.InvariantCulture)).FirstOrDefault();
        }

        private static FieldInfo GetField(object target, string fieldName)
        {
            return GetAllFields(target, f => f.Name.Equals(fieldName, 
                  StringComparison.InvariantCulture)).FirstOrDefault();
        }
        private static IEnumerable<FieldInfo> GetAllFields(object target, Func<FieldInfo, bool> predicate)
        {
            var types = new List<Type> { target.GetType() };

            while (types.Last().BaseType != null)
            {
                types.Add(types.Last().BaseType);
            }

            for (var i = types.Count - 1; i >= 0; i--)
            {
                var fieldInfos = types[i]
                    .GetFields(BindingFlags.Instance | BindingFlags.Static | 
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    .Where(predicate);

                foreach (var fieldInfo in fieldInfos)
                {
                    yield return fieldInfo;
                }
            }
        }
        private static IEnumerable<MethodInfo> GetAllMethods(object target, Func<MethodInfo, bool> predicate)
        {
            var methodInfos = target.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Static | 
                    BindingFlags.NonPublic | BindingFlags.Public)
                .Where(predicate);

            return methodInfos;
        }
        #endregion

        private bool MeetsConditions(SerializedProperty property)
        {
            var showIfAttribute = attribute as ShowIfAttribute;
            var target = property.serializedObject.targetObject;
            var conditionValues = new List<bool>();

            foreach (var condition in showIfAttribute.Conditions)
            {
                var conditionField = GetField(target, condition);
                if (conditionField != null &&
                    conditionField.FieldType == typeof(bool))
                {
                    conditionValues.Add((bool)conditionField.GetValue(target));
                }

                var conditionMethod = GetMethod(target, condition);
                if (conditionMethod != null &&
                    conditionMethod.ReturnType == typeof(bool) &&
                    conditionMethod.GetParameters().Length == 0)
                {
                    conditionValues.Add((bool)conditionMethod.Invoke(target, null));
                }
            }

            if (conditionValues.Count > 0)
            {
                bool met;
                if (showIfAttribute.Operator == ConditionOperator.And)
                {
                    met = conditionValues.Aggregate(true, (current, value) => current && value);
                }
                else
                {
                    met = conditionValues.Aggregate(false, (current, value) => current || value);
                }
                return met;
            }
            else
            {
                Debug.LogError("Invalid boolean condition fields or methods used!");
                return true;
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Calcluate the property height, if we don't meet the condition and the draw mode is DontDraw, then height will be 0.
            var meetsCondition = MeetsConditions(property);
            var showIfAttribute = attribute as ShowIfAttribute;

            if (!meetsCondition && showIfAttribute.Action == ActionOnConditionFail.HideAttribute) return 0;
            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var meetsCondition = MeetsConditions(property);
            // Early out, if conditions met, draw and go.
            if (meetsCondition)
            {
                EditorGUI.PropertyField(position, property, label, true);
                return; 
            }

            var showIfAttribute = attribute as ShowIfAttribute;
            switch (showIfAttribute.Action)
            {
                case ActionOnConditionFail.HideAttribute:
                    return;
                case ActionOnConditionFail.DisableAttribute:
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUI.PropertyField(position, property, label, true);
                    EditorGUI.EndDisabledGroup();
                    break;
            }

        }
    }
}