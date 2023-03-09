using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Zlitz.Utilities
{
    public static class TilemapExtensions
    {
        public static Vector3Int[] ColliderCast(this Tilemap tilemap, Collider2D collider, bool getNullTiles)
        {
            List<Vector3Int> res = new List<Vector3Int>();
            
            Bounds colliderBounds = collider.bounds;
            Vector3Int minCell = tilemap.WorldToCell(colliderBounds.min);
            Vector3Int maxCell = tilemap.WorldToCell(colliderBounds.max);

            for (int x = minCell.x; x <= maxCell.x; x++)
                for (int y = minCell.y; y <= maxCell.y; y++)
                    for (int z = minCell.z; z <= maxCell.z; z++)
                    {
                        Vector3Int cell = new Vector3Int(x, y, z);
                        if (tilemap.WorldToCell(collider.ClosestPoint(tilemap.GetCellCenterWorld(cell))) == cell && (getNullTiles || tilemap.GetTile(cell) != null))
                        {
                            res.Add(cell);
                        }
                    }


            return res.ToArray();
        }
    }
}
