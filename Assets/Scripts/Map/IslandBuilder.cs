using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Map
{
    public class IslandBuilder : MonoBehaviour
    {
    #region Noise Generation

        [Header("Island ground settings")] 
        public int seed = 1;
        [Range(0, 256)]
        public int size = 128;
        public float heightMultiplier = 10f;
        [Space]

        [Space]
        public FastNoiseLite.NoiseType noiseType = FastNoiseLite.NoiseType.OpenSimplex2;
        public float frequency = 15;
        private float Frequency => frequency / 1000;
        
        [Space]
        public FastNoiseLite.FractalType fractalType = FastNoiseLite.FractalType.None;
        public int fractalOctaves = 4;
        public float fractalLacunarity = 2;

        [Space] 
        public FastNoiseLite.CellularDistanceFunction cellularDistanceFunction = FastNoiseLite.CellularDistanceFunction.Hybrid;
        public FastNoiseLite.CellularReturnType cellularReturnType = FastNoiseLite.CellularReturnType.Distance2;
        public float celluralJitter = 0;
        [Space] 
        public FastNoiseLite.DomainWarpType domainWarpType = FastNoiseLite.DomainWarpType.OpenSimplex2;
        public float domainWarpAmp = 100;
        
        // Data
        private FastNoiseLite _noise;
        private FastNoiseLite _cuttingNoise;
        private float[] _noiseData;
        private float[] _cuttingData;
        private List<Vector3> _groundPoints;

        [Header("Cutting settings")]
        [Range(10, 256)]
        public float circleCutRadius = 10f;
        // [Range(0, 1)]
        public float cuttingBelowHeight = 0.3f;
        [Space] 
        public FastNoiseLite.NoiseType cuttingNoiseType = FastNoiseLite.NoiseType.Cellular;
        public float cuttingFrequency = 50;
        private float CuttingFrequency => cuttingFrequency / 1000;
        public FastNoiseLite.FractalType cuttingFractalType = FastNoiseLite.FractalType.None;
        public FastNoiseLite.CellularReturnType cuttingFractalReturnType = FastNoiseLite.CellularReturnType.CellValue;
        public int cuttingFractalOctaves = 4;
        public float cuttingFractalLacunarity = 2;
        public FastNoiseLite.DomainWarpType cuttingDomainWarpType = FastNoiseLite.DomainWarpType.OpenSimplex2;
        public float cuttingDomainWarpAmplitude = 100;

        [Header("Environment")] 
        [SerializeField] private Transform resourcesContainer;
        [SerializeField] private List<MapObject> resources = new List<MapObject>();

        [Header("Dev")] 
        public bool isShowGizmos = true;
        public bool isShowGround = true;
        public bool isShowCutting = true;

    #endregion
        
    #region Mesh Generation
        
        private Mesh _groundMesh;
        private MeshFilter _groundMeshFilter;

    #endregion
        
        private Vector3 Center => transform.position + new Vector3((float)size / 2, 0, (float)size / 2);
        
        private bool IsPointInsideCircle(float x, float z)
        {
            // if (isGlobalZeroPos) 
            // return x * x + y * y <= circleCutRadius * circleCutRadius;
            x -= Center.x;
            z -= Center.z;
            return x * x + z * z <= circleCutRadius + circleCutRadius;
        }

        private bool IsIslandPoint(float x, float y, int index)
        {
            var inCircle = IsPointInsideCircle(x, y);
            var isNotCutting = _cuttingData[index] > cuttingBelowHeight;
            return inCircle && isNotCutting;
        }

    #region Noise Generation Methods

        public void GenerateNoise()
        {
            _noise = new FastNoiseLite();
            _noise.SetNoiseType(noiseType);
            _noise.SetFrequency(Frequency);
            
            _noise.SetFractalType(fractalType);
            _noise.SetFractalOctaves(fractalOctaves);
            _noise.SetFractalLacunarity(fractalLacunarity);
            
            _noise.SetCellularDistanceFunction(cellularDistanceFunction);
            _noise.SetCellularReturnType(cellularReturnType);
            _noise.SetCellularJitter(celluralJitter);
            
            _noise.SetDomainWarpType(domainWarpType);
            _noise.SetDomainWarpAmp(domainWarpAmp);

            _cuttingNoise = new FastNoiseLite();
            _cuttingNoise.SetNoiseType(cuttingNoiseType);
            _cuttingNoise.SetFrequency(CuttingFrequency);
            
            _cuttingNoise.SetFractalType(cuttingFractalType);
            _cuttingNoise.SetCellularReturnType(cuttingFractalReturnType);
            _cuttingNoise.SetFractalOctaves(cuttingFractalOctaves);
            _cuttingNoise.SetFractalLacunarity(cuttingFractalLacunarity);
            
            _cuttingNoise.SetDomainWarpType(cuttingDomainWarpType);
            _cuttingNoise.SetDomainWarpAmp(cuttingDomainWarpAmplitude);
            
            _noise.SetSeed(seed);
            _cuttingNoise.SetSeed(seed);
            
            _noiseData = new float[(size + 1) * (size + 1)];
            _cuttingData = new float[(size + 1) * (size + 1)];
            var index = 0;
            for (var y = 0; y <= size; y++)
            {
                for (var x = 0; x <= size; x++)
                {
                    _noiseData[index] = _noise.GetNoise(x, y);
                    _cuttingData[index] = _cuttingNoise.GetNoise(x, y);
                    index++;
                }
            }
        }
        

        private void OnDrawGizmos()
        {
            if (!isShowGizmos) return;
            
            var index = 0;
            var gizmosCubeSize = new Vector3(0.1f, 0.1f, 0.1f);

            if (isShowGround && _noiseData != null)
            {
                for (var y = 0; y <= size; y++)
                {
                    for (var x = 0; x <= size; x++)
                    {
                        Gizmos.color = IsIslandPoint(x, y, index) ? Color.green : Color.grey;

                        Gizmos.DrawCube(new Vector3(x, _noiseData[index] * heightMultiplier, y), gizmosCubeSize);
                        index++;
                    }
                }
            }

            if (!isShowCutting || _cuttingData == null || _cuttingData.Length <= size) return; 
            index = 0;
            Gizmos.color = Color.yellow;
            for (var y = 0; y <= size; y++)
            {
                for (var x = 0; x <= size; x++)
                {
                    Gizmos.DrawCube(new Vector3(x, _cuttingData[index++] * heightMultiplier, y), gizmosCubeSize);
                }
            }
        }
        
    #endregion

    #region Mesh Generation Methods

        
        // For tests
        public void ClearMesh()
        {
            _groundMesh.Clear();
        }

        public void ClearEnvironment()
        {
            for (var i = resourcesContainer.childCount - 1; i >= 0; i--)
            {
                #if UNITY_EDITOR
                    DestroyImmediate(resourcesContainer.GetChild(i).gameObject);
                #else
                    Destroy(resourcesContainer.GetChild(i).gameObject);
                #endif
            }
        }
        

        public void GenerateMesh()
        {
            // TODO: вынести меш в дочерний объект и тз брать ссылки на компоненты 
            if (_groundMeshFilter is null)
            {
                gameObject.AddComponent<MeshFilter>();
            }
            
            _groundMeshFilter = GetComponent<MeshFilter>();

            if (_groundMesh)
            {
                _groundMesh.Clear();
            }

            _groundMesh = new Mesh { name = "GroundMesh" };
            _groundMeshFilter.mesh = _groundMesh;
            

            // Mesh calc
            var groundVertices = new Vector3[(size + 1) * (size + 1)]; // TODO: на вершинах можно объекты спавнить
            var uv = new Vector2[groundVertices.Length];
            _groundPoints = new List<Vector3>();
            var i = 0;
            var noiseIndex = 0;
            for (var y = 0; y <= size; y++) {
                for (var x = 0; x <= size; x++)
                {
                    var pos = new Vector3(x, _noiseData[noiseIndex++] * heightMultiplier, y);
                    groundVertices[i] = pos;
                    // uv[i] = new Vector2(x / (float) size, y / (float)size);
                    
                    if (IsIslandPoint(x, y, i))
                    {
                        uv[i] = new Vector2(x / (float) size, y / (float)size);
                        _groundPoints.Add(pos);
                    }
                    
                    i++;
                }
            }
            _groundMesh.vertices = groundVertices;
            _groundMesh.uv = uv;

            var triangles = new int[size * size * 6];
            i = 0;
            for (int ti = 0, vi = 0, y = 0; y < size; y++, vi++) {
                for (var x = 0; x < size; x++, ti += 6, vi++) {
                    if (!IsIslandPoint(x, y, i++)) continue;
                    
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + size + 1;
                    triangles[ti + 5] = vi + size + 2;
                }
            }
            _groundMesh.triangles = triangles;
            
            _groundMesh.RecalculateBounds();
            _groundMesh.RecalculateNormals();
        }
        
        // private void Smooth(Mesh mesh)
        // {
        //     var vertices = mesh.vertices;
        //     var triangles = mesh.triangles;
        //     var unmergedNormals = new Vector3[vertices.Length];
        //     var mergedNormals = new Vector3[vertices.Length];
        //
        //     for (int i = 0; i < triangles.Length; i += 3) {
        //         var i0 = triangles[i + 0];
        //         var i1 = triangles[i + 1];
        //         var i2 = triangles[i + 2];
        //
        //         var v0 = vertices[i0];
        //         var v1 = vertices[i1];
        //         var v2 = vertices[i2];
        //
        //         var normal = Vector3.Cross(v1 - v0, v2 - v0).normalized;
        //
        //         unmergedNormals[i0] += normal * Vector3.Angle(v1 - v0, v2 - v0);
        //         unmergedNormals[i1] += normal * Vector3.Angle(v0 - v1, v2 - v1);
        //         unmergedNormals[i2] += normal * Vector3.Angle(v0 - v2, v1 - v2);
        //     }
        //
        //     for (int i = 0; i < vertices.Length; i++) {
        //         for (int j = 0; j < vertices.Length; j++) {
        //             if (vertices[i] == vertices[j]) {
        //                 mergedNormals[i] += unmergedNormals[j];
        //             }
        //         }
        //     }
        //
        //     for (int i = 0; i < mergedNormals.Length; i++) {
        //         mergedNormals[i] = mergedNormals[i].normalized;
        //     }
        //
        //     mesh.normals = mergedNormals;
        // }

    #endregion


    #region Map Generation Methods


        public void GenerationResources()
        {
            foreach (var item in resources.Where(item => !(item.chance < 0)))
            {
                for (var i = 0; i < item.maxCount; i++)
                {
                    var pos = _groundPoints[Random.Range(0, _groundPoints.Count)];
                    _groundPoints.Remove(pos);
                    pos.z -= heightMultiplier;
                    
                    Instantiate(item.prefab, pos,  Quaternion.Euler(-90, 0, 90), resourcesContainer);
                }
            }
        }

    #endregion
    
    }
}
