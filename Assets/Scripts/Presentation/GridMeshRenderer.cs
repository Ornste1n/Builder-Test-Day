using Application.Interfaces.Grid;
using UnityEngine;
using Application.UseCases.Grid;

namespace Presentation
{
    public class GridMeshRenderer : MonoBehaviour, IGridRenderer
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private MeshFilter _meshFilter;
        
        private const int VertsPerCell = 4;
        private const int TrisPerCell = 2;

        public void Render(GridRenderData data)
        {
            Mesh mesh = BuildMesh(data);
            _meshFilter.mesh = mesh;
            _meshRenderer.material = data.GridMaterial;
        }

        public void UpdateCell(int x, int y, Color color)
        {
            
        }

        private Mesh BuildMesh(GridRenderData data)
        {
            Mesh mesh = new() { name = "GridMesh" }; 
            Vector3[] verts = new Vector3[data.Width * data.Height * VertsPerCell];
            int[] tris = new int[data.Width * data.Height * TrisPerCell * 3]; 
            Color[] colors = new Color[verts.Length]; 
            
            int v = 0, t = 0;
            for (int y = 0; y < data.Height; y++)
            {
                for (int x = 0; x < data.Width; x++)
                {
                    Vector3 bl = GridToWorld(x, y, data.CellSize, data.Origin);
                    verts[v + 0] = bl; 
                    verts[v + 1] = bl + new Vector3(data.CellSize, 0, 0);
                    verts[v + 2] = bl + new Vector3(data.CellSize, 0, data.CellSize);
                    verts[v + 3] = bl + new Vector3(0, 0, data.CellSize); 
                    
                    tris[t + 0] = v + 0;
                    tris[t + 1] = v + 2; 
                    tris[t + 2] = v + 1;
                    tris[t + 3] = v + 0;
                    tris[t + 4] = v + 3; 
                    tris[t + 5] = v + 2; 
                    
                    Color chosen = Random.value > 0.5f ? data.DefaultColorA : data.DefaultColorB;
                    
                    colors[v + 0] = chosen;
                    colors[v + 1] = chosen; 
                    colors[v + 2] = chosen; 
                    colors[v + 3] = chosen; 
                    
                    v += 4; t += 6;
                }
            } 
            mesh.vertices = verts;
            mesh.triangles = tris;
            mesh.colors = colors; 
            mesh.RecalculateBounds(); 
            mesh.RecalculateNormals(); 
            return mesh;
        }

        private Vector3 GridToWorld(int x, int y, float cellSize, Vector3 origin)
        {
            float wx = origin.x + x * cellSize + cellSize / 2f;
            float wz = origin.z + y * cellSize + cellSize / 2f; 
            return new Vector3(wx, origin.y, wz);
        }
    }
}
