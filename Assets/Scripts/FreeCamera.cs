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

    [SerializeField]
    private GameObject cobblestonePrefab;
    [SerializeField]
    private GameObject waterPrefab;

    void Start()
    {
        normalSpeed = 0.05F;
        multiplySpeed = 3;
        mouseSpeed = 1;
    }

    void Update()
    {
        // Position
        // positionVector.x = Input.GetAxis("Horizontal");
        // positionVector.y = Input.GetAxis("Updown");
        // positionVector.z = Input.GetAxis("Vertical");

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
                if (hit.collider != null)
                {
                    string RaycastReturn = hit.collider.gameObject.name;
                }
            }
            else
            {
                Debug.Log("Mouse not Hit");
            }

            Vector3 hitDirection = hit.point - hit.transform.position;

            float upWeight = Vector3.Dot(hitDirection, hit.transform.up);
            float forwardWeight = Vector3.Dot(hitDirection, hit.transform.forward);
            float rightWeight = Vector3.Dot(hitDirection, hit.transform.right);

            
            //We care about the absolute value only for now
            float upMag = Mathf.Abs(upWeight);
            float forwardMag = Mathf.Abs(forwardWeight);
            float rightMag = Mathf.Abs(rightWeight);

            Vector3 positionNew;

            if(upMag >= forwardMag && upMag >= rightMag)
            {
                if(upWeight > 0) 
                {
                    positionNew = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y+1, hit.collider.gameObject.transform.position.z);
                    // Debug.Log("Gora"); //Up
                }
                else 
                {
                    positionNew = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y-1, hit.collider.gameObject.transform.position.z);
                    //Debug.Log("Dol"); //Down
                }
            }
            else if(forwardMag >= upMag && forwardMag >= rightMag)
            {
                if(forwardWeight > 0) 
                {
                    positionNew = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z+1);
                    //Debug.Log("Przod"); //Forward
                }
                else  
                {
                    positionNew = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z-1);
                    //Debug.Log("Tyl"); //Back
                }
            }
            else
            {
                if(rightWeight > 0) 
                {
                    positionNew = new Vector3(hit.collider.gameObject.transform.position.x+1, hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z);
                    //Debug.Log("Prawo"); //Right
                }
                else 
                {
                    positionNew = new Vector3(hit.collider.gameObject.transform.position.x-1, hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z);
                    //Debug.Log("Lewo"); //Left
                }
            }


            float angle = Vector3.SignedAngle(hit.point, hit.collider.gameObject.transform.position, Vector3.up);
            Instantiate(waterPrefab, positionNew, Quaternion.identity);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            // Raycast
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider != null)
                {
                    string RaycastReturn = hit.collider.gameObject.name;
                }
            }
            else
            {
                Debug.Log("Mouse not Hit");
            }

            Vector3 hitDirection = hit.point - hit.transform.position;

            float upWeight = Vector3.Dot(hitDirection, hit.transform.up);
            float forwardWeight = Vector3.Dot(hitDirection, hit.transform.forward);
            float rightWeight = Vector3.Dot(hitDirection, hit.transform.right);

            
            //We care about the absolute value only for now
            float upMag = Mathf.Abs(upWeight);
            float forwardMag = Mathf.Abs(forwardWeight);
            float rightMag = Mathf.Abs(rightWeight);

            Vector3 positionNew;

            if(upMag >= forwardMag && upMag >= rightMag)
            {
                if(upWeight > 0) 
                {
                    positionNew = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y+1, hit.collider.gameObject.transform.position.z);
                    // Debug.Log("Gora"); //Up
                }
                else 
                {
                    positionNew = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y-1, hit.collider.gameObject.transform.position.z);
                    //Debug.Log("Dol"); //Down
                }
            }
            else if(forwardMag >= upMag && forwardMag >= rightMag)
            {
                if(forwardWeight > 0) 
                {
                    positionNew = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z+1);
                    //Debug.Log("Przod"); //Forward
                }
                else  
                {
                    positionNew = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z-1);
                    //Debug.Log("Tyl"); //Back
                }
            }
            else
            {
                if(rightWeight > 0) 
                {
                    positionNew = new Vector3(hit.collider.gameObject.transform.position.x+1, hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z);
                    //Debug.Log("Prawo"); //Right
                }
                else 
                {
                    positionNew = new Vector3(hit.collider.gameObject.transform.position.x-1, hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z);
                    //Debug.Log("Lewo"); //Left
                }
            }
            Instantiate(cobblestonePrefab, positionNew, Quaternion.identity);
            
        }

    }

}
