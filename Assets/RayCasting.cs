using UnityEngine;

public class RayCasting : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        var player = GameObject.FindGameObjectWithTag("Player");
        var rayPlayer = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        //
        // LayerMask.GetMask(params string[]) gives directly maksed integer. Othewise we need to shift the bit. Example: 1 << 8 for enemy in our case
        // 100f is distance to cast ray
        //
        if (Physics.Raycast(rayPlayer, out hitInfo, 100f, LayerMask.GetMask("Enemy")))
        {
            // Destroy enemy on click on enemy
            if (Input.GetMouseButtonDown(0))
            {
                if (hitInfo.rigidbody != null)
                {
                    Debug.Log("Ray casted object has rigid body");
                    hitInfo.collider.GetComponent<Renderer>().material.color = Color.red;
                    Destroy(hitInfo.collider);
                }
            }
        }
        // Change color to friendly if mouse is over friend
        var hit = Physics.Raycast(rayPlayer, out hitInfo, 100f, LayerMask.GetMask("Friend"));
        if (hit)
        {
            hitInfo.collider.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            foreach (var friend in GameObject.FindGameObjectsWithTag("Friend"))
                friend.GetComponent<Renderer>().material.color = Color.gray;
        }
    }
}
