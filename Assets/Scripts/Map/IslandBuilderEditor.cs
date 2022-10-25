using UnityEditor;
using UnityEngine;

namespace Map
{
    [CustomEditor(typeof(IslandBuilder))]
    public class IslandBuilderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var script = (IslandBuilder)target;
            if(GUILayout.Button("Generate Noise"))
            {
                script.GenerateNoise();
            }
            if(GUILayout.Button("Generate Mesh"))
            {
                script.GenerateMesh();
            }
            if(GUILayout.Button("Generation Resources"))
            {
                script.GenerationResources();
            }
            GUILayout.Space(10);
            if(GUILayout.Button("Clear Mesh & Environment"))
            {
                script.ClearMesh();
            }
            if(GUILayout.Button("Clear Environment"))
            {
                script.ClearEnvironment();
            }
        }
    }
}
