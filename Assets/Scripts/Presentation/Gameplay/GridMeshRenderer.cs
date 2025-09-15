using Repositories.Application;
using UnityEngine;

namespace Presentation.Gameplay
{
    public class GridMeshRenderer : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private MeshFilter _meshFilter;
        
        [SerializeField] private GridConfigSo _config; // todo исправить
        
        private const int VertsPerCell = 4;
        private const int TrisPerCell = 2;
        
        private Mesh _mesh;
        private Color[] _colors;

        private int _width;
        private int _height;
        private float _cellSize;
        private Vector3 _origin;

        public void Build(int width, int height)
        {
            _meshRenderer.sharedMaterial = _config.TileMaterial;
            _cellSize = _config.CellSize;
            _origin = _config.Origin;
            
            _width = width;
            _height = height;
            _mesh = new Mesh();
            _mesh.name = "GridMesh";
            
            Vector3[] verts = new Vector3[width * height * VertsPerCell];
            int[] tris = new int[width * height * TrisPerCell * 3];
            _colors = new Color[verts.Length];

            int v = 0; 
            int t = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x<width; x++)
                {
                    Vector3 bl = GridToWorld(x,y) + new Vector3(-_cellSize/2f, 0, -_cellSize/2f);
                
                    verts[v+0] = bl;
                    verts[v+1] = bl + new Vector3(_cellSize, 0, 0);
                    verts[v+2] = bl + new Vector3(_cellSize, 0, _cellSize);
                    verts[v+3] = bl + new Vector3(0, 0, _cellSize);

                    tris[t+0] = v+0; tris[t+1] = v+2; tris[t+2] = v+1;
                    tris[t+3] = v+0; tris[t+4] = v+3; tris[t+5] = v+2;

                    Color chosen = Random.value < 0.5f ? _config.DefaultColorA : _config.DefaultColorB;

                    _colors[v+0] = chosen;
                    _colors[v+1] = chosen;
                    _colors[v+2] = chosen;
                    _colors[v+3] = chosen;

                    v += 4; t += 6;
                }
            }

            _mesh.vertices = verts;
            _mesh.triangles = tris;
            _mesh.colors = _colors;
            _mesh.RecalculateBounds();
            _mesh.RecalculateNormals();
            _meshFilter.mesh = _mesh;
        }

        public void SetCellColor(int x, int y, Color color)
        {
            if (_mesh == null) return;
            if (x < 0 || x >= _width || y < 0 || y >= _height) return;
            int cellIndex = y * _width + x;
            int baseVert = cellIndex * VertsPerCell;
            _colors[baseVert + 0] = color;
            _colors[baseVert + 1] = color;
            _colors[baseVert + 2] = color;
            _colors[baseVert + 3] = color;
            _mesh.colors = _colors;
        }

        private Vector3 GridToWorld(int x, int y)
        {
            float wx = _origin.x + x * _cellSize + _cellSize/2f;
            float wz = _origin.z + y * _cellSize + _cellSize/2f;
            return new Vector3(wx, _origin.y, wz);
        }
    }
}
