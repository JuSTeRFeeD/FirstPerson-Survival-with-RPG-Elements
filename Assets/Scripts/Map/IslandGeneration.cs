using System;
using UnityEngine;
using Utils;

namespace Map
{
    public class IslandGeneration : MonoBehaviour
    {
        #region Noise Generation       

        [Header("Island ground settings")]
        public Vector2 originPosition = Vector2.zero;
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
        public FastNoiseLite.CellularReturnType fractalReturnType = FastNoiseLite.CellularReturnType.Distance2;
        public int fractalOctaves = 4;
        public float fractalLacunarity = 2;

        [Space] 
        public FastNoiseLite.DomainWarpType domainWarpType = FastNoiseLite.DomainWarpType.OpenSimplex2;
        public float domainWarpAmp = 100;
        
        // Private fields
        private FastNoiseLite _noise;
        private float[] _noiseData;

        [Header("== dev ===")] 
        public bool isShowGizmos = true;


        private int _cutPointsCount;
        
        #endregion
        
        #region Mesh Generation

        [Space]
        [Min(0)]
        public float circleCutRadius = 10f;
        
        private MeshRenderer _groundMeshRenderer;
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
            return x * x + z * z <= circleCutRadius * circleCutRadius;
        }

        #region Noise Generation Methods
        public void GenerateNoise()
        {
            // noise setup
            _noise = new FastNoiseLite();
            _noise.SetNoiseType(noiseType);
            _noise.SetFrequency(Frequency);
            
            _noise.SetFractalType(fractalType);
            _noise.SetCellularReturnType(fractalReturnType);
            _noise.SetFractalOctaves(fractalOctaves);
            _noise.SetFractalLacunarity(fractalLacunarity);
            
            _noise.SetDomainWarpType(domainWarpType);
            _noise.SetDomainWarpAmp(domainWarpAmp);
            
            // generation
            _noiseData = new float[(size + 1) * (size + 1)];
            var index = 0;
            _cutPointsCount = 0;
            for (var y = 0; y <= size; y++)
            {
                for (var x = 0; x <= size; x++)
                {
                    if (!IsPointInsideCircle(x, y))
                    {
                        index++;
                        continue;
                    }

                    _cutPointsCount++;
                    _noiseData[index++] = _noise.GetNoise(x, y);
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (!isShowGizmos) return;
            var len = _noiseData.GetLength(0);
            if (_noiseData != null && len == 0) return;

            var index = 0;
            var count = Mathf.Sqrt(len);
            var gizmosCubeSize = new Vector3(0.1f, 0.1f, 0.1f);
            
            for (var y = 0 ; y < count; y++)
            {
                for (var x = 0; x < count; x++)
                {
                    Gizmos.color = IsPointInsideCircle(x, y) ? Color.green : Gizmos.color = Color.grey;
                    var pos = new Vector3(x, _noiseData[index] * heightMultiplier, y);
                    Gizmos.DrawCube(pos, gizmosCubeSize);
                    index++;
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
        
        public void GenerateMesh()
        {
            // TODO: вынести меш в дочерний объект и тз брать ссылки на компоненты 
            if (_groundMeshFilter is null)
            {
                gameObject.AddComponent<MeshFilter>();
            }

            _groundMeshRenderer = GetComponent<MeshRenderer>();
            _groundMeshFilter = GetComponent<MeshFilter>();

            if (_groundMesh)
            {
                _groundMesh.Clear();
            }
            _groundMesh = new Mesh { name = "GroundMesh" };
            _groundMeshFilter.mesh = _groundMesh;
            
            
            // Mesh calc
            var vertices = new Vector3[(_cutPointsCount + 1) * (_cutPointsCount + 1)]; // TODO: на вершинах можно объекты спавнить
            var uv = new Vector2[vertices.Length];
            var floatSize = (float)_cutPointsCount;
            for (int i = 0, y = 0; y <= _cutPointsCount; y++) {
                for (var x = 0; x <= _cutPointsCount; x++, i++) {
                    if (!IsPointInsideCircle(x, y)) continue;
                    
                    vertices[i] = new Vector3(x, _noiseData[i] * heightMultiplier, y);
                    uv[i] = new Vector2(x / floatSize, y / floatSize);
                }
            }
            _groundMesh.vertices = vertices;
            _groundMesh.uv = uv;

            var triangles = new int[_cutPointsCount * _cutPointsCount * 6];
            for (int ti = 0, vi = 0, y = 0; y < _cutPointsCount; y++, vi++) {
                for (var x = 0; x < _cutPointsCount; x++, ti += 6, vi++) {
                    if (!IsPointInsideCircle(x, y)) continue;
                    
                    triangles[ti] = vi;
                    triangles[ti + 1] = triangles[ti + 4] = vi + _cutPointsCount + 1;
                    triangles[ti + 2] = triangles[ti + 3] = vi + 1;
                    triangles[ti + 5] = vi + _cutPointsCount + 2;
                }
            }
            _groundMesh.triangles = triangles;
            
            _groundMesh.RecalculateBounds();
            _groundMesh.RecalculateNormals();
        }

        #endregion

    }
}
