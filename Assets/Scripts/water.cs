using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    private float speed = 2f;
    public string Type;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {  
        Debug.Log("water1");
        Vector3 currentPos = transform.position;

        Collider[] intersecting_right = Physics.OverlapSphere(new Vector3(currentPos.x+1, currentPos.y, currentPos.z), 0.01f);
        Collider[] intersecting_left = Physics.OverlapSphere(new Vector3(currentPos.x-1, currentPos.y, currentPos.z), 0.01f);
        Collider[] intersecting_back = Physics.OverlapSphere(new Vector3(currentPos.x, currentPos.y, currentPos.z+1), 0.01f);
        Collider[] intersecting_front = Physics.OverlapSphere(new Vector3(currentPos.x, currentPos.y, currentPos.z-1), 0.01f);
        Collider[] intersecting_down = Physics.OverlapSphere(new Vector3(currentPos.x, currentPos.y-1, currentPos.z), 0.01f);


        //if (intersecting_down.Length == 0)
        //   add_water(new Vector3(currentPos.x, currentPos.y-1, currentPos.z));

        //Destroy(this);
        //Destroy(gameObject);
        transform.Translate(0, -1f * Time.deltaTime * speed, 0, Space.World);
    }

    void add_water(Vector3 pos)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = pos;
        cube.GetComponent<Renderer>().material.color = Color.blue;

        string ScriptName = "water";
        System.Type MyScriptType = System.Type.GetType (ScriptName + ",Assembly-CSharp");
        cube.AddComponent(MyScriptType);
    }
}
