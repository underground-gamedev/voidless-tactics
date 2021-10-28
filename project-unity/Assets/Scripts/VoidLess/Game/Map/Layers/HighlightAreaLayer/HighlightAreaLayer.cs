using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using VoidLess.Game.Map.Base;

namespace VoidLess.Game.Map.Layers.HighlightAreaLayer
{
    public class HighlightAreaLayer: MonoBehaviour, IMapLayer
    {
        [SerializeField]
        private MeshFilter meshFilter;
    
        [Flags]
        private enum PositionConnectionType
        {
            None =   0,
            Right =  1 << 0,
            Bottom = 1 << 1,
            Left =   1 << 2,
            Up =     1 << 3,
        }
    
        private void AddPosition(List<Vector3> vertices, List<Vector3Int> triangles, 
            Vector2Int pos, PositionConnectionType con)
        {
            const float hBottom = 0.1f;
            const float hTop = 0.14f;
            var upVector = new Vector3(0, 0, hTop - hBottom);
        
            const float w = 1;
            const float h = 1;
        
            var rbVertexBottom = new Vector3(pos.x+w, pos.y, hBottom);
            var lbVertexBottom = new Vector3(pos.x, pos.y, hBottom);
            var ruVertexBottom = new Vector3(pos.x+w , pos.y+h, hBottom);
            var luVertexBottom = new Vector3(pos.x, pos.y + h, hBottom);

            var rbVertexBottomIndex = vertices.IndexOf(rbVertexBottom);
            var lbVertexBottomIndex = vertices.IndexOf(lbVertexBottom);
            var ruVertexBottomIndex = vertices.IndexOf(ruVertexBottom);
            var luVertexBottomIndex = vertices.IndexOf(luVertexBottom);

            void AddDirectionLine(ref int firstVertexIndex, ref int secondVertexIndex,
                Vector3 firstVertex, Vector3 secondVertex)
            {
                var firstVertexTop = firstVertex + upVector;
                var firstVertexTopIndex = firstVertexIndex + 1;
                if (firstVertexIndex == -1)
                {
                    firstVertexIndex = vertices.Count;
                    firstVertexTopIndex = firstVertexIndex + 1;
                    vertices.Add(firstVertex);
                    vertices.Add(firstVertexTop);
                }

                var secondVertexTop = secondVertex + upVector;
                var secondVertexTopIndex = secondVertexIndex + 1;
                if (secondVertexIndex == -1)
                {
                    secondVertexIndex = vertices.Count;
                    secondVertexTopIndex = secondVertexIndex + 1;
                    vertices.Add(secondVertex);
                    vertices.Add(secondVertexTop);
                }
            
                triangles.Add(new Vector3Int(secondVertexIndex, firstVertexIndex, firstVertexTopIndex));
                triangles.Add(new Vector3Int(secondVertexIndex, firstVertexTopIndex, secondVertexTopIndex));
            }

            if (!con.HasFlag(PositionConnectionType.Right))
            {
                AddDirectionLine(
                    ref ruVertexBottomIndex, ref rbVertexBottomIndex,
                    ruVertexBottom, rbVertexBottom);
            }

            if (!con.HasFlag(PositionConnectionType.Bottom))
            {
                AddDirectionLine(
                    ref rbVertexBottomIndex, ref lbVertexBottomIndex, 
                    rbVertexBottom, lbVertexBottom);
            }

            if (!con.HasFlag(PositionConnectionType.Left))
            {
                AddDirectionLine(
                    ref lbVertexBottomIndex, ref luVertexBottomIndex,
                    lbVertexBottom, luVertexBottom);
            }

            if (!con.HasFlag(PositionConnectionType.Up))
            {
                AddDirectionLine(
                    ref luVertexBottomIndex, ref ruVertexBottomIndex,
                    luVertexBottom, ruVertexBottom);
            }
        }
    
        public void HighlightArea(Vector2Int[] positions)
        {
            var vertices = new List<Vector3>();
            var triangles = new List<Vector3Int>();
            foreach (var pos in positions)
            {
                var connectionType = PositionConnectionType.None;
                foreach (var neigh in positions)
                {
                    if ((pos - neigh).magnitude > 1.0 || pos == neigh) continue;
                    if (neigh.x > pos.x) connectionType |= PositionConnectionType.Right;
                    else if (neigh.x < pos.x) connectionType |= PositionConnectionType.Left;
                    else if (neigh.y > pos.y) connectionType |= PositionConnectionType.Up;
                    else if (neigh.y < pos.y) connectionType |= PositionConnectionType.Bottom;
                }
            
                AddPosition(vertices, triangles, pos, connectionType);
            }

            meshFilter.mesh = new Mesh
            {
                vertices = vertices.ToArray(), 
                triangles = triangles.SelectMany(triangle => new int[] {triangle.x, triangle.y, triangle.z}).ToArray()
            };
        }

        public void OnAttached(ILayeredMap map)
        {
        }

        public void OnDeAttached()
        {
        }
    }
}