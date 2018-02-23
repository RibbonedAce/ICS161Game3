using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze {
    private int height;
    private int width;
    private List<MapNode> nodes;    // The mapnodes that make up the map

    // A constructor taking height and width
    public Maze (int nHeight, int nWidth)
    {
        height = nHeight;
        width = nWidth;
        nodes = new List<MapNode>(height * width);
        for (int i = 0; i < nodes.Capacity; ++i)
        {
            nodes.Add(new MapNode());
        }
        RandomizeMap();
    }

    // Get the MapNode at the x,y position in the map
    public MapNode NodeAt (int x, int y)
    {
        return (x < 0 || x >= width || y < 0 || y >= height) ? new MapNode() : nodes[y * width + x];
    }

    // Get the x,y position of the MapNode
    public Vector2Int PositionAt (MapNode mn)
    {
        int result = nodes.IndexOf(mn);
        return new Vector2Int(result % width, result / width);
    }

    // Get the x,y position of the index
    public Vector2Int PositionAt (int index)
    {
        return new Vector2Int(index % width, index / width);
    }

    // Get the index above the given one in the map
    private int NodeAbove (int index)
    {
        Vector2Int xy = PositionAt(index);
        return (xy.y < 0 || xy.y >= height - 1) ? -1 : index + width;
    }

    // Get the index below the given one in the map
    private int NodeBelow (int index)
    {
        Vector2Int xy = PositionAt(index);
        return (xy.y < 1 || xy.y >= height) ? -1 : index - width;
    }

    // Get the index left of the given one in the map
    private int NodeLeft (int index)
    {
        Vector2Int xy = PositionAt(index);
        return (index < 1 || index >= nodes.Count || xy.x < 1) ? -1 : index - 1;
    }

    // Get the index right of the given one in the map
    private int NodeRight (int index)
    {
        Vector2Int xy = PositionAt(index);
        return (index < 1 || index >= nodes.Count || xy.x >= width) ? -1 : index + 1;
    }

    // Get the node above the given one in the map
    private MapNode NodeAbove (MapNode mn)
    {
        Vector2Int xy = PositionAt(mn);
        return (xy.y < 0 || xy.y >= height - 1) ? new MapNode() : nodes[nodes.IndexOf(mn) + width];
    }

    // Get the node below the given one in the map
    private MapNode NodeBelow (MapNode mn)
    {
        Vector2Int xy = PositionAt(mn);
        return (xy.y < 1 || xy.y >= height) ? new MapNode() : nodes[nodes.IndexOf(mn) - width];
    }

    // Get the node left of the given one in the map
    private MapNode NodeLeft (MapNode mn)
    {
        int index = nodes.IndexOf(mn);
        Vector2Int xy = PositionAt(index);
        return (index < 1 || index >= nodes.Count || xy.x < 1) ? new MapNode() : nodes[index - 1];
    }

    // Get the node right of the given one in the map
    private MapNode NodeRight (MapNode mn)
    {
        int index = nodes.IndexOf(mn);
        Vector2Int xy = PositionAt(index);
        return (index < 1 || index >= nodes.Count || xy.x >= width) ? new MapNode() : nodes[nodes.IndexOf(mn) + 1];
    }

    // Connect two nodes together if adjacent
    private void ConnectNodes (MapNode mn1, MapNode mn2)
    {
        Vector2Int xy1 = PositionAt(mn1);
        Vector2Int xy2 = PositionAt(mn2);
        if ((xy2 - xy1).magnitude == 1.0f)
        {
            if (xy2.y - xy1.y == 1)
            {
                mn1.adjacents.Add(Direction.Up, mn2);
                mn2.adjacents.Add(Direction.Down, mn1);
            }
            else if (xy2.y - xy1.y == -1)
            {
                mn1.adjacents.Add(Direction.Down, mn2);
                mn2.adjacents.Add(Direction.Up, mn1);
            }
            else if (xy2.x - xy1.x == -1)
            {
                mn1.adjacents.Add(Direction.Left, mn2);
                mn2.adjacents.Add(Direction.Right, mn1);
            }
            else if (xy2.x - xy1.x == 1)
            {
                mn1.adjacents.Add(Direction.Right, mn2);
                mn2.adjacents.Add(Direction.Left, mn1);
            }
        }
    }

    // Find a path between two nodes, returning the closest one if possible
    private List<int> FindPath (int start, int end)
    {
        List<int> result = new List<int> { start };
        return result;
    }

    // Clear a path between two nodes
    private void MakePath (int start, int end)
    {
        List<int> path = FindPath(start, end);
        if (path[path.Count - 1] != end)
        {
            Vector2Int xys = PositionAt(path[path.Count - 1]);
            Vector2Int xye = PositionAt(end);
        }
    }

    // Make a random map
    private void RandomizeMap ()
    {
        for (int i = 0; i < nodes.Capacity; ++i)
        {
            nodes[i] = new MapNode();
        }
        foreach (MapNode mn in nodes)
        {
            Vector2Int xy = PositionAt(mn);
            if (xy.y < height - 1 && Random.Range(0, 2) == 1)
            {
                MapNode up = NodeAbove(mn);
                mn.adjacents.Add(Direction.Up, up);
                up.adjacents.Add(Direction.Down, mn);
            }
            if (xy.x < width - 1 && Random.Range(0, 1) == 1)
            {
                MapNode right = NodeRight(mn);
                mn.adjacents.Add(Direction.Right, right);
                right.adjacents.Add(Direction.Left, mn);
            }
        }
    }
}
