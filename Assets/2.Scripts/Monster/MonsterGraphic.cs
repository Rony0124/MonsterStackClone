using UnityEngine;

public class MonsterGraphic : MonoBehaviour
{
    private SpriteRenderer[] spriteRenderers;

    private void OnEnable()
    {
        if(spriteRenderers.Length > 0)
            return;
        
        spriteRenderers = transform.GetComponentsInChildren<SpriteRenderer>();
    }

    public void AddOrderInLayer(int orderInLayer)
    {
        foreach (var renderer in spriteRenderers)
        {
            renderer.sortingOrder += orderInLayer;
        }
    }
}
