using Data;

public class Item
{
    private ItemData _data;
    
    public ItemData Data => _data;
    
    public Item (ItemData data)
    {
        this._data = data;
    }
}
