using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    public float normalSpeed;
    public float multiplySpeed;
    public float mouseSpeed;

    private Vector3 positionVector;
    private Vector3 rotationVector;

    void Start()
    {
        normalSpeed = 0.05F;
        multiplySpeed = 3;
        mouseSpeed = 1;
    }

    void Update()
    {
        // Position
        positionVector.x = Input.GetAxis("Horizontal");
        positionVector.y = Input.GetAxis("Updown");
        positionVector.z = Input.GetAxis("Vertical");

        positionVector *= normalSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            positionVector *= multiplySpeed;
        }

        transform.Translate(positionVector.x, 0, positionVector.z, Space.Self);
        transform.Translate(0, positionVector.y, 0, Space.World);

        // Rotation
        rotationVector = transform.rotation.eulerAngles;

        rotationVector.x -= Input.GetAxis("Rotate X"); //Input.GetAxis("Mouse Y");
        rotationVector.y += Input.GetAxis("Rotate Y"); //Input.GetAxis("Mouse X");

        if ( rotationVector.x < 180 && rotationVector.x > 0 )
        {
            rotationVector.x = Mathf.Clamp(rotationVector.x, 0, 90);
        }

        else if ( rotationVector.x < 360 && rotationVector.x > 180 )
        {
            rotationVector.x = Mathf.Clamp(rotationVector.x, 270, 360);
        }

        transform.rotation = Quaternion.Euler(rotationVector.x, rotationVector.y, 0);

        if (Input.GetButtonDown("Fire1"))
        {
            // Raycast
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                //Debug.DrawRay(Input.mousePosition, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Mouse Did Hit");
                Debug.Log(hit);
                if (hit.collider != null)
                {
                    string RaycastReturn = hit.collider.gameObject.name;
                    Debug.Log("did hit");
                    Debug.Log(hit.collider.gameObject.transform.position);
                    Debug.Log(RaycastReturn);
                    Debug.Log(RaycastReturn);
                }
            }
            else
            {
                //Debug.DrawRay(Input.mousePosition, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Mouse not Hit");
            }

            // jesli objekt juz istnieje to nie


            // Vector3.angleBetween(cube.position, hit.position) <- to defect which face to hit
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z-1);
            cube.GetComponent<Renderer>().material.color = Color.blue;

            string ScriptName = "water";
            System.Type MyScriptType = System.Type.GetType (ScriptName + ",Assembly-CSharp");
            cube.AddComponent(MyScriptType);
        }

        /**RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }*/

    }

}
