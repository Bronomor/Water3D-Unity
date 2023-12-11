using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    private float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {  
        Vector3 currentPos = transform.position;

        Collider[] intersecting_right = Physics.OverlapSphere(new Vector3(currentPos.x+1, currentPos.y, currentPos.z), 0.01f);
        Collider[] intersecting_left = Physics.OverlapSphere(new Vector3(currentPos.x-1, currentPos.y, currentPos.z), 0.01f);
        Collider[] intersecting_back = Physics.OverlapSphere(new Vector3(currentPos.x, currentPos.y, currentPos.z+1), 0.01f);
        Collider[] intersecting_front = Physics.OverlapSphere(new Vector3(currentPos.x, currentPos.y, currentPos.z-1), 0.01f);
        Collider[] intersecting_down = Physics.OverlapSphere(new Vector3(currentPos.x, currentPos.y-1, currentPos.z), 0.01f);


        if (intersecting_down.Length == 0)
           transform.Translate(0, -1f * Time.deltaTime * speed, 0, Space.World);
        else if (intersecting_down.Length > 0)
        {
            //transform.Translate(-1f * Time.deltaTime * speed, 0, 0, Space.World);
            //transform.Translate( 1f * Time.deltaTime * speed, 0, 0, Space.World);
            //transform.Translate(0, 0, -1f * Time.deltaTime * speed, Space.World);
            //transform.Translate(0, 0, -1f * Time.deltaTime * speed, Space.World);

            //add_water(new Vector3(currentPos.x+1, currentPos.y, currentPos.z));
            //add_water(new Vector3(currentPos.x-1, currentPos.y, currentPos.z));
            //add_water(new Vector3(currentPos.x, currentPos.y, currentPos.z-1));
            //add_water(new Vector3(currentPos.x, currentPos.y, currentPos.z+1));
        }

        //Destroy(this);
        //Destroy(gameObject);
        //add_water(new Vector3(currentPos.x, currentPos.y-1, currentPos.z));
    }

}
