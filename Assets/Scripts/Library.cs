using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Library<T> : ScriptableObject where T : ScriptableObject
{
    [SerializeField] protected List<T> items;
    
    public T FindByHash(int hashCode)
    {
        var result = items.FirstOrDefault(item => item.GetHashCode() == hashCode);
        return result;
    }

    public List<T> GetAll()
    {
        return items;
    }
}
