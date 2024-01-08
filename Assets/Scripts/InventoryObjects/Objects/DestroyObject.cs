using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private LayerMask SolidObject;
    [SerializeField] private GameObject Text;
    [SerializeField] private float power = 40;

    void Update()
    {
        Collider2D collision = Physics2D.OverlapBox(transform.position, new Vector2(2, 2), 0, SolidObject); //add massive
        if (collision != null)
        {
            //Debug.Log(collision.gameObject.name);
            if (Input.GetKeyDown(KeyCode.Mouse0) && collision.gameObject.GetComponent < DestroyableObject > () != null) //Change key
            {
                string itemInRightHand = "";
                string itemInLeftHand = "";
                if (transform.parent.GetChild(4).childCount != 0)
                {
                    itemInRightHand = transform.parent.GetChild(4).GetChild(0).name;
                }
                if (transform.parent.GetChild(5).childCount != 0)
                {
                    itemInLeftHand = transform.parent.GetChild(5).GetChild(0).name;
                }
                if (itemInRightHand.Contains(collision.gameObject.GetComponent<DestroyableObject>().destroyObject.itemToDestroy) || itemInLeftHand.Contains(collision.gameObject.GetComponent<DestroyableObject>().destroyObject.itemToDestroy))
                {
                    collision.gameObject.GetComponent<DestroyableObject>().stateAmount -= power;
                    collision.gameObject.transform.localScale = new Vector3(collision.gameObject.transform.localScale.x - 0.1f, collision.gameObject.transform.localScale.y - 0.1f, collision.gameObject.transform.localScale.z - 0.1f);
                    if (collision.gameObject.GetComponent<DestroyableObject>().stateAmount <= 0)
                    {
                        Instantiate(collision.gameObject.GetComponent<DestroyableObject>().destroyObject.DropedItemPrefab, collision.transform.position, collision.transform.rotation);
                        Destroy(collision.gameObject);
                    }
                }
            }
            //Text.SetActive(true);
        }
        else
        {
            //Text.SetActive(false);
        }
    }

}
