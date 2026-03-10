using System;

public interface IUpdateable
{
    event Action OnUpdated;
    
    void CustomUpdate();
    void RemoveAllListeners();
}
