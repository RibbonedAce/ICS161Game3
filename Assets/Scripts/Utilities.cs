using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {
    
    // Return if an element is contained in any of the lists provided
    public static bool InAnyList<T> (List<List<T>> lists, T item)
    {
        foreach (List<T> list in lists)
        {
            if (list.Contains(item))
            {
                return true;
            }
        }
        return false;
    }
}
