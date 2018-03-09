using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze {
    public static Maze instance;        // The instance to reference
    private int height;                 // The height of the map
    private int width;                  // The width of the map
    public static int _width;
    public static int _height;
    private float chance;           // The amount of walls to spawn
    public List<MapNode> nodes;     // The mapnodes that make up the map
    private static bool foundDest = false;
    // A constructor taking height and width
    public Maze (int nHeight, int nWidth)
    {
        instance = this;
        height = nHeight;
        width = nWidth;
        _width = width;
        _height = height;
        //chance = nChance;
        chance = ((float)GameController.difficulty + 1) / 4;
        nodes = new List<MapNode>(height * width);
        for (int i = 0; i < nodes.Capacity; ++i)
        {
            nodes.Add(new MapNode());
        }
        RandomizeMap();
    }
    // Return the maze nodes
    public List<MapNode> GetNodes ()
    {
        return new List<MapNode>(nodes);
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
        return (index < 0 || index >= nodes.Count || xy.x < 1) ? -1 : index - 1;
    }

    // Get the index right of the given one in the map
    private int NodeRight (int index)
    {
        Vector2Int xy = PositionAt(index);
        return (index < 0 || index >= nodes.Count || xy.x >= width) ? -1 : index + 1;
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
        return (index < 0 || index >= nodes.Count || xy.x < 1) ? new MapNode() : nodes[index - 1];
    }

    // Get the node right of the given one in the map
    private MapNode NodeRight (MapNode mn)
    {
        int index = nodes.IndexOf(mn);
        Vector2Int xy = PositionAt(index);
        return (index < 0 || index >= nodes.Count || xy.x >= width) ? new MapNode() : nodes[nodes.IndexOf(mn) + 1];
    }

    // Get the node in a specified direction on the map
    private MapNode NodeInDirection (MapNode mn, Direction d)
    {
        switch (d)
        {
            case Direction.Up:
                return NodeAbove(mn);
            case Direction.Down:
                return NodeBelow(mn);
            case Direction.Left:
                return NodeLeft(mn);
            case Direction.Right:
                return NodeRight(mn);
            default:
                return NodeRight(mn);
        }
    }

    // Get the node in a specified direction on the map
    private int NodeInDirection (int index, Direction d)
    {
        switch (d)
        {
            case Direction.Up:
                return NodeAbove(index);
            case Direction.Down:
                return NodeBelow(index);
            case Direction.Left:
                return NodeLeft(index);
            case Direction.Right:
                return NodeRight(index);
            default:
                return NodeRight(index);
        }
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
                mn1.adjacents[Direction.Up] = mn2;
                mn2.adjacents[Direction.Down] = mn1;
            }
            else if (xy2.y - xy1.y == -1)
            {
                mn1.adjacents[Direction.Down] = mn2;
                mn2.adjacents[Direction.Up] = mn1;
            }
            else if (xy2.x - xy1.x == -1)
            {
                mn1.adjacents[Direction.Left] = mn2;
                mn2.adjacents[Direction.Right] = mn1;
            }
            else if (xy2.x - xy1.x == 1)
            {
                mn1.adjacents[Direction.Right] = mn2;
                mn2.adjacents[Direction.Left] = mn1;
            }
        }
    }

    // Connect two nodes together if adjacent
    private void ConnectNodes (int mn1, int mn2)
    {
        Vector2Int xy1 = PositionAt(mn1);
        Vector2Int xy2 = PositionAt(mn2);
        if ((xy2 - xy1).magnitude == 1.0f)
        {
            if (xy2.y - xy1.y == 1)
            {
                nodes[mn1].adjacents[Direction.Up] = nodes[mn2];
                nodes[mn2].adjacents[Direction.Down] = nodes[mn1];
            }
            else if (xy2.y - xy1.y == -1)
            {
                nodes[mn1].adjacents[Direction.Down] = nodes[mn2];
                nodes[mn2].adjacents[Direction.Up] = nodes[mn1];
            }
            else if (xy2.x - xy1.x == -1)
            {
                nodes[mn1].adjacents[Direction.Left] = nodes[mn2];
                nodes[mn2].adjacents[Direction.Right] = nodes[mn1];
            }
            else if (xy2.x - xy1.x == 1)
            {
                nodes[mn1].adjacents[Direction.Right] = nodes[mn2];
                nodes[mn2].adjacents[Direction.Left] = nodes[mn1];
            }
        }
    }

    // Find the general direction of an index given a start index
    private Direction DirectionTo (int start, int end)
    {
        Vector2Int xys = PositionAt(start);
        Vector2Int xye = PositionAt(end);
        if (Mathf.Abs(xys.x - xye.x) >= Mathf.Abs(xys.y - xye.y))
        {
            return xys.x - xye.x > 0 ? Direction.Left : Direction.Right;
        }
        else
        {
            return xys.y - xye.y > 0 ? Direction.Down : Direction.Up;
        }
    }

    // Find a path between two nodes, returning the closest one if possible
    public List<int> FindPath (int start, int end)
    {
        List<List<int>> searches = new List<List<int>>();
        foreach (MapNode mn in nodes[start].adjacents.Values)
        {
            if (mn != null)
            {
                searches.Add(FindPath(nodes.IndexOf(mn), end, new List<int> { start }));
            }
        }
        List<int> bestSearch = new List<int>();
        foreach (List<int> search in searches)
        {
            if (search[search.Count - 1] == end && (bestSearch.Count == 0 || search.Count < bestSearch.Count))
            {
                bestSearch = search;
            }
        }
        if (bestSearch.Count == 0)
        {
            foreach (List<int> search in searches)
            {
                if (bestSearch.Count == 0 || GetDistance(search[search.Count - 1], end) < GetDistance(bestSearch[bestSearch.Count - 1], end))
                {
                    bestSearch = search;
                }
            }
        }
        bestSearch.Insert(0, start);
        return bestSearch;
    }

    // Find a path between two nodes, returning the closest one if possible
    private List<int> FindPath (int start, int end, List<int> searched)
    {
        List<int> newSearched = new List<int>(searched);
        newSearched.Add(start);
        List<List<int>> searches = new List<List<int>>();
        foreach (MapNode mn in nodes[start].adjacents.Values)
        {
            if (mn != null && !searched.Contains(nodes.IndexOf(mn)))
            {
                searches.Add(FindPath(nodes.IndexOf(mn), end, newSearched));
            }
        }
        List<int> bestSearch = new List<int>();
        foreach (List<int> search in searches)
        {
            if (search[search.Count - 1] == end && (bestSearch.Count == 0 || search.Count < bestSearch.Count))
            {
                bestSearch = search;
            }
        }
        if (bestSearch.Count == 0)
        {
            foreach (List<int> search in searches)
            {
                if (bestSearch.Count == 0 || GetDistance(search[search.Count - 1], end) < GetDistance(bestSearch[bestSearch.Count - 1], end))
                {
                    bestSearch = search;
                }
            }
        }
        bestSearch.Insert(0, start);
        return bestSearch;
    }

    // Find a path between two nodes, returning the closest one if possible
    public List<int> FindPathFast (int start, int end)
    {
        if (start == end)
        {
            return new List<int> { start };
        }
        foreach (MapNode mn in GetSortedKeys(start, end))
        {
            if (mn != null && nodes.IndexOf(mn) == end)
            {
                return new List<int> { start, end };
            }
        }
        List<List<int>> searches = new List<List<int>>();
        foreach (MapNode mn in GetSortedKeys(start, end))
        {
            if (mn != null)
            {
                List<int> search = FindPathFast(nodes.IndexOf(mn), end, new List<int> { start });
                if (search[search.Count - 1] == end)
                {
                    search.Insert(0, start);
                    return search;
                }
                else
                {
                    searches.Add(search);
                }
            }
        }
        List<int> bestSearch = new List<int>();
        foreach (List<int> search in searches)
        {
            if (bestSearch.Count == 0 || GetDistance(search[search.Count - 1], end) < GetDistance(bestSearch[bestSearch.Count - 1], end))
            {
                bestSearch = search;
            }
        }
        bestSearch.Insert(0, start);
        return bestSearch;
    }

    // Find a path between two nodes, returning the closest one if possible
    private List<int> FindPathFast(int start, int end, List<int> searched)
    {
        if (start == end)
        {
            return new List<int> { start };
        }
        foreach (MapNode mn in GetSortedKeys(start, end))
        {
            if (mn != null && nodes.IndexOf(mn) == end)
            {
                return new List<int> { start, end };
            }
        }
        List<int> newSearched = new List<int>(searched);
        newSearched.Add(start);
        List<List<int>> searches = new List<List<int>>();
        foreach (MapNode mn in GetSortedKeys(start, end))
        {
            if (mn != null && !searched.Contains(nodes.IndexOf(mn)))
            {
                List<int> search = FindPathFast(nodes.IndexOf(mn), end, newSearched);
                if (search[search.Count - 1] == end)
                {
                    search.Insert(0, start);
                    return search;
                }
                else
                {
                    searches.Add(search);
                }
            }
        }
        List<int> bestSearch = new List<int>();
        foreach (List<int> search in searches)
        {
            if (bestSearch.Count == 0 || GetDistance(search[search.Count - 1], end) < GetDistance(bestSearch[bestSearch.Count - 1], end))
            {
                bestSearch = search;
            }
        }
        bestSearch.Insert(0, start);
        return bestSearch;
    }

    // Return a list of adjacent nodes organized by distance
    public List<MapNode> GetSortedKeys(int start, int end)
    {
        List<MapNode> result = new List<MapNode>();
        Dictionary<MapNode, float> keyDistances = new Dictionary<MapNode, float>();
        foreach (MapNode mn in nodes[start].adjacents.Values)
        {
            if (mn != null)
            {
                keyDistances.Add(mn, GetDistance(start, end));
            }
        }
        for (int i = 0; i < keyDistances.Keys.Count; ++i)
        {
            MapNode toAdd = new MapNode();
            float min = Mathf.Infinity;
            foreach (MapNode mn in keyDistances.Keys)
            {
                if (!result.Contains(mn) && keyDistances[mn] < min)
                {
                    toAdd = mn;
                    min = keyDistances[mn];
                }
            }
            result.Add(toAdd);
        }
        return result;
    }

    // Find all connected nodes to the current one
    private List<int> AllConnectedNodes (int start)
    {
        List<int> result = new List<int> { start };
        List<int> search = new List<int> { start };
        while (search.Count > 0)
        {
            int toSearch = search[0];
            search.RemoveAt(0);
            foreach (MapNode mn in nodes[toSearch].adjacents.Values)
            {
                if (mn != null && !result.Contains(nodes.IndexOf(mn)))
                {
                    result.Add(nodes.IndexOf(mn));
                    search.Add(nodes.IndexOf(mn));
                }
            }
        }
        return result;
    }

    // Get the distance between two nodes
    private float GetDistance (int start, int end)
    {
        Vector2Int xys = PositionAt(start);
        Vector2Int xye = PositionAt(end);
        return Mathf.Sqrt(Mathf.Pow(xys.x - xye.x, 2) + Mathf.Pow(xys.y - xye.y, 2));
    }

    /*// Find a path between two nodes, returning the closest one if possible
    public List<int> FindPathOld (int start, int end)
    {
        List<int> result = new List<int>();
        List<int> visitedNodes = new List<int>();
        List<int> TotalNodes = new List<int>();
        TotalNodes.Add(start);
        CountNodesInTree(start, end, TotalNodes);
        if (start != end)
        {
            result.Add(start);
            visitedNodes.Add(start);
            FindPathOverride(start, end, result, visitedNodes,TotalNodes.Count);
        }
        foundDest = false;
        return result;
    }

    public static void CountNodesInTree(int start, int end, List<int> c)
    {
        foreach(MapNode mn in nodes[start].adjacents.Values)
        {
            if(nodes.IndexOf(mn) != -1 && !c.Contains(nodes.IndexOf(mn)))
            {
                c.Add(start);
                CountNodesInTree(nodes.IndexOf(mn), end, c);
            }
        }
    }

    public static void FindPathOverride(int start, int end, List<int> o, List<int> visited,int max)
    {
        if (start != end)
        {
            bool isStuck = true;
            foreach (MapNode mn in nodes[start].adjacents.Values)
            {
                if (nodes.IndexOf(mn) != -1 && !visited.Contains(nodes.IndexOf(mn)) && !foundDest)
                {
                    isStuck = false;
                    int newStart = nodes.IndexOf(mn);
                    visited.Add(newStart);
                    o.Add(newStart);
                    FindPathOverride(newStart, end, o, visited, max);
                }
            }
            if (isStuck && visited.Count < max && !foundDest)
            {
                if (o.Count > 1)
                {
                    o.Remove(start);
                    FindPathOverride(o[o.Count - 1], end, o, visited, max);
                }
            }
        }
        else foundDest = true;
    }*/

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
            if (xy.y < height - 1 && Random.Range(0f, 1f) > chance)
            {
                MapNode up = NodeAbove(mn);
                mn.adjacents[Direction.Up] = up;
                up.adjacents[Direction.Down] = mn;
            }
            if (xy.x < width - 1 && Random.Range(0f, 1f) > chance)
            {
                MapNode right = NodeRight(mn);
                mn.adjacents[Direction.Right] = right;
                right.adjacents[Direction.Left] = mn;
            }
        }
        List<List<int>> groups = new List<List<int>>();
        for (int i = 0; i < nodes.Count; ++i)
        {
            if (!Utilities.InAnyList(groups, i))
            {
                groups.Add(AllConnectedNodes(i));
            }
        }
        for (int i = 1; i < groups.Count; ++i)
        {
            Vector2Int xy = PositionAt(groups[i][0]);
            if (xy.y < 1)
            {
                ConnectNodes(groups[i][0], NodeLeft(groups[i][0]));
            }
            else
            {
                ConnectNodes(groups[i][0], NodeBelow(groups[i][0]));
            }
        }
    }
}
