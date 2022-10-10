using UnityEngine;

namespace Utils
{
    public static class NullRefCheck
    {
        public static void CheckNullable<T>(T component, string msg = "")
        {
            if (component != null) return;
            Debug.LogError($"NULL REFERENCE. TypeOf: {typeof(T)}. {msg}");
            Application.Quit();
        }
    }
}
