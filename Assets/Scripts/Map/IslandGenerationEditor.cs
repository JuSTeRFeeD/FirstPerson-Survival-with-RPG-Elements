using UnityEditor;
using UnityEngine;

namespace Map
{
    [CustomEditor(typeof(IslandGeneration))]
    public class IslandGenerationEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var script = (IslandGeneration)target;
            if(GUILayout.Button("Generate Noise"))
            {
                script.GenerateNoise();
            }
            if(GUILayout.Button("Generate Mesh"))
            {
                script.GenerateMesh();
            }
            if(GUILayout.Button("Clear Mesh"))
            {
                script.ClearMesh();
            }
        }
    }
}
