using System.Collections.Generic;
using UnityEngine;

public class ItemsPoolHandler : MonoBehaviour
{
    public List<GameObject> GetItemsPool(GameObject item, Transform creationTransform, int itemsCount)
    {
        List<GameObject> itemsPool = new List<GameObject>();

        for (int i = 0; i < itemsCount; i++)
        {
            itemsPool.Add(Instantiate(item, creationTransform));
            itemsPool[i].gameObject.SetActive(false);
        }

        return itemsPool;
    }

    public void DestroyItemsPool(List<GameObject> itemsPool)
    {
        foreach (var item in itemsPool)
            Destroy(item);
        
        itemsPool.Clear();
    }
}