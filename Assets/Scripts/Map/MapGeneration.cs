using System;
using UnityEngine;
using Utils;

namespace Map
{
    public class MapGeneration : MonoBehaviour
    {
        public float scale = 1f;
        [Range(0, 0.01f)]
        public float frequency = 0.015f;
        [Range(0, 768)] 
        public int size = 64;
        public float heightMultiplier = 10f;
        private float[] noiseData;

        public FastNoiseLite.NoiseType noiseType = FastNoiseLite.NoiseType.Value;
        [Space]
        [Header("Fractal")]
        public FastNoiseLite.FractalType fractalType = FastNoiseLite.FractalType.None;
        public FastNoiseLite.CellularReturnType returnType = FastNoiseLite.CellularReturnType.Distance2;
        
        private void Start()
        {
            FastNoiseLite noise = new FastNoiseLite();
            noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);

            // Gather noise data
            noiseData = new float[size * size];
            var index = 0;

            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    noiseData[index++] = noise.GetNoise(x, y);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            var noise = new FastNoiseLite();
            noise.SetNoiseType(noiseType);
            noise.SetFrequency(frequency);
            
            noise.SetFractalType(fractalType);
            noise.SetCellularReturnType(returnType);
            noise.SetFractalOctaves(4);
            noise.SetFractalLacunarity(2);
            
            noise.SetDomainWarpType(FastNoiseLite.DomainWarpType.OpenSimplex2);
            noise.SetDomainWarpAmp(100);
            // noise.
            
            // Gather noise data
            noiseData = new float[size * size];
            var index = 0;
            var gizmosCubeSize = new Vector3(0.5f, 0.5f, 0.5f);
            
            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    noiseData[index] = noise.GetNoise(x * scale, y * scale);
                    Gizmos.DrawCube(new Vector3(x, noiseData[index] * heightMultiplier, y), gizmosCubeSize);
                    index++;
                }
            }
            
            // if (noiseData.Length == 0) return;
            // var index = 0;
            // for (var y = 0; y < 128; y++)
            // {
            //     for (var x = 0; x < 128; x++)
            //     {
            //         Gizmos.DrawSphere(new Vector3(x, y, noiseData[index]), 0.25f);
            //     }
            // }
        }
    }
}
