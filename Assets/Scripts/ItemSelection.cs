using System.Drawing;
using UnityEngine;

public class ItemSelection : MonoBehaviour
{
    [SerializeField] private GameObject Text;
    [SerializeField] private LayerMask Item;

    // Update is called once per frame
    void Update()
    {
        Collider2D collision = Physics2D.OverlapBox(transform.position, new Vector2(4, 4), 0, Item);
        if (collision != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject destroyalble = collision.gameObject;
                Destroy(destroyalble);
            }
            Text.SetActive(true);
        }
        else
        {
            Text.SetActive(false);
        }
        
    }
}
