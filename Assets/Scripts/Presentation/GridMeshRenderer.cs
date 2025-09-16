using UnityEngine;
using Application.UseCases.Grid;
using Application.Interfaces.Grid;

namespace Presentation
{
    public class GridMeshRenderer : MonoBehaviour, IGridRenderer
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshCollider _meshCollider;
        
        private const int VertsPerCell = 4;
        private const int TrisPerCell = 2;

        private Color[] _colors;
        private GridRenderData _gridRender;

        private int _highlightedX = -1;
        private int _highlightedY = -1;
        private Color _highlightedOriginalColor;
        private Color _highlightColor = Color.green;
        
        public GridRenderData GridRenderData => _gridRender;
        
        public void Render(GridRenderData data)
        {
            Mesh mesh = BuildMesh(data);
            
            _gridRender = data;
            _meshFilter.mesh = mesh;
            _meshCollider.sharedMesh = mesh;
            _meshRenderer.material = data.GridMaterial;
        }

        public void UpdateCell(int x, int y, Color color)
        {
            if (x < 0 || x >= _gridRender.Width || y < 0 || y >= _gridRender.Height) return;

            int v = (y * _gridRender.Width + x) * VertsPerCell;
            for (int i = 0; i < VertsPerCell; i++)
                _colors[v + i] = color;

            _meshFilter.mesh.colors = _colors;
        }

        public Color GetCellColor(int x, int y)
        {
            if (x < 0 || x >= _gridRender.Width || y < 0 || y >= _gridRender.Height)
                return GetDefaultCellColor();

            int v = (y * _gridRender.Width + x) * VertsPerCell;
            return _colors[v];
        }

        public void HighlightCellAtWorldPos(Vector3 worldPos)
        {
            if (_gridRender == null) return;

            float cellSize = _gridRender.CellSize;
            Vector3 origin = _gridRender.Origin;

            float localX = worldPos.x - origin.x - cellSize / 2f;
            float localZ = worldPos.z - origin.z - cellSize / 2f;

            int x = Mathf.Clamp(Mathf.FloorToInt(localX / cellSize), 0, _gridRender.Width - 1);
            int y = Mathf.Clamp(Mathf.FloorToInt(localZ / cellSize), 0, _gridRender.Height - 1);
            
            if (x == _highlightedX && y == _highlightedY) return;

            if (_highlightedX >= 0 && _highlightedY >= 0)
                UpdateCell(_highlightedX, _highlightedY, _highlightedOriginalColor);

            _highlightedOriginalColor = GetCellColor(x, y);

            UpdateCell(x, y, _highlightColor);

            _highlightedX = x;
            _highlightedY = y;
        }

        private Color GetDefaultCellColor() => Random.value > 0.5f ? _gridRender.DefaultColorA : _gridRender.DefaultColorB;

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
            _colors = colors;
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
