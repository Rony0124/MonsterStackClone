public class FloatingTextSpawner : Spawner<FloatingText>
{
    public FloatingText GetFloatingText()
    {
        var floatingText = pool.GetObject();
        if (floatingText != null)
        {
            poolList.Add(floatingText);
        }
        return floatingText;
    }

    public void RetrieveFloatingText(FloatingText text)
    {
        poolList.Remove(text);
        pool.ReturnObject(text);
    }

    public void ClearAllFloatingText()
    {
        foreach (var item in poolList)
        {
            Destroy(item.gameObject);
        }
        
        poolList.Clear();
    }
}
