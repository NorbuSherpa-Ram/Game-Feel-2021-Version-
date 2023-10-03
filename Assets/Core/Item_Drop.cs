using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Drop : MonoBehaviour
{
    [SerializeField] Pickable[] itemToDrop;

    public void DropItem()
    {
        foreach (Pickable  item in itemToDrop)
        {
            int chanceOfDrop = Random.Range(0, 100);
            if (item.dropChnage >= chanceOfDrop)
            {
                Instantiate(item, transform.position, Quaternion.identity);
            }

        }

    }
}
