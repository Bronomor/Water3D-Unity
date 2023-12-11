using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaterCell : MonoBehaviour
{
    public float waterVolume = 1.0f; // Current volume of water in this cell
    public float maxVolume = 1.0f; // Maximum volume this cell can hold without pressure

    public float flowSpeed = 1.0f; // Speed at which water flows between cells

    public GameObject waterPrefab; // Assign the water prefab

    private float blockHeight = 1f;
    private float originalYPosition;

    private void Update()
    {
        CheckFlow();
    }
    private void Start()
    {
        originalYPosition = transform.localPosition.y;
    }

    private void CheckFlow()
    {
        // Rule #1: Flow Downwards
        var flowedDown = FlowDown();
        if (flowedDown) return;
        // // Rule #2: Flow Sideways


        FlowToAdjacentCell(Vector3.left);
        // FlowToAdjacentCell(Vector3.right);
        // FlowToAdjacentCell(Vector3.forward);
        // FlowToAdjacentCell(Vector3.back);

        // // Rule #3: Flow Upwards if under pressure
        // if (waterVolume > maxVolume)
        // {
        //     FlowToAdjacentCell(Vector3.up);
        // }
    }
    private Collider[] CheckCollisions(Vector3 direction)
    {
        // Calculate the center of the face of the water block in the given direction
        // assuming the water block is a cube with its pivot at the center.
        var faceCenter = transform.position + (direction.normalized * (blockHeight / 2));

        // Calculate the distance to extend the check just beyond the face of the block.
        // We want the check to be just beyond the face of the block, not from the center.
        var checkStartPoint = faceCenter + (direction.normalized * 0.002f); // Small offset in the given direction

        // Now we use OverlapSphere at the checkStartPoint with a small radius.
        Collider[] intersecting = Physics.OverlapSphere(checkStartPoint, 0.001f);

        // Optionally, draw a debug sphere in the Scene view
        Debug.DrawRay(transform.position, checkStartPoint - transform.position, Color.green, 1.0f);
        Debug.DrawRay(checkStartPoint, direction.normalized * 0.1f, Color.red, 1.0f);

        return intersecting;
    }
    private void FlowToAdjacentCell(Vector3 direction)
    {
        Collider[] intersecting = CheckCollisions(direction);
        Debug.Log(gameObject.name + " " + intersecting.Length);
        if (intersecting.Length == 0)
        {
            SplitWater(direction);
        }
        else if (intersecting[0].gameObject.CompareTag("Water"))
        {
            // There's water in this direction, let's attempt to transfer some volume
            WaterCell adjacentWaterCell = intersecting[0].GetComponent<WaterCell>();
            if (adjacentWaterCell != null && waterVolume > adjacentWaterCell.waterVolume)
            {
                TransferWaterVolume(adjacentWaterCell);
            }
        }
        // If there's a stone, we do nothing as the rule states
    }
    private void SplitWater(Vector3 direction)
    {
        var transferVolume = waterVolume / 2;
        if (transferVolume < 0.1f)
        {
            InstantiateWaterCell(transform.position + direction, transferVolume * 2);
            Destroy(gameObject);
        }
        else
        {
            InstantiateWaterCell(transform.position + direction, transferVolume);
            RemoveWater(transferVolume);
        }


    }

    private bool FlowDown()
    {

        Collider[] intersecting = CheckCollisions(Vector3.down);
        if (intersecting.Length == 0)
        {
            MoveWater(Vector3.down);
            return true;
        }
        else if (intersecting[0].gameObject.CompareTag("Water"))
        {
            WaterCell adjacentWaterCell = intersecting[0].GetComponent<WaterCell>();
            if (adjacentWaterCell != null && waterVolume > adjacentWaterCell.waterVolume)
            {
                TransferWaterVolume(adjacentWaterCell);
                return true;
            }
        }
        return false;
    }
    private void MoveWater(Vector3 distance)
    {
        // Move the water in the specified direction
        transform.position += distance * Time.deltaTime * this.flowSpeed;
    }



    // private void FlowToAdjacentCell(Vector3 direction)
    // {
    //     Collider[] intersecting = Physics.OverlapSphere(transform.position + direction, 0.01f);
    //     if (intersecting.Length == 0)
    //     {
    //         // No collider, can potentially flow in this direction
    //         CreateOrMoveWater(direction);
    //     }
    //     else if (intersecting[0].gameObject.CompareTag("Water"))
    //     {
    //         // There's water in this direction, let's attempt to transfer some volume
    //         WaterCell adjacentWaterCell = intersecting[0].GetComponent<WaterCell>();
    //         if (adjacentWaterCell != null && waterVolume > adjacentWaterCell.waterVolume)
    //         {
    //             TransferWaterVolume(adjacentWaterCell);
    //         }
    //     }
    //     // If there's a stone, we do nothing as the rule states
    // }


    private WaterCell InstantiateWaterCell(Vector3 position, float volume)
    {
        // Instantiate a new water cell with a specified volume at the given position
        GameObject newWaterCellObject = Instantiate(waterPrefab, position, Quaternion.identity);
        WaterCell newWaterCell = newWaterCellObject.GetComponent<WaterCell>();
        if (newWaterCell != null)
        {
            newWaterCell.AddWater(volume);
        }
        return newWaterCell;
    }

    private void TransferWaterVolume(WaterCell targetCell)
    {
        float transferVolume = Mathf.Min((waterVolume - targetCell.waterVolume) / 2, waterVolume);
        targetCell.AddWater(transferVolume);
        RemoveWater(transferVolume);
    }

    public void AddWater(float amount)
    {
        waterVolume = Mathf.Min(waterVolume + amount, maxVolume);
        UpdateVisuals();
    }

    public void RemoveWater(float amount)
    {
        waterVolume = Mathf.Max(waterVolume - amount, 0.0f);
        Debug.Log(waterVolume);
        if (waterVolume == 0)
        {
            Destroy(gameObject); // Destroy this cell if it's empty
            Debug.Log("Destroy");
        }
        else
        {
            UpdateVisuals();
        }
    }

    private void UpdateVisuals()
    {
        // Calculate the new scale, assuming the full volume corresponds to a scale of 1
        float newHeightScale = waterVolume / maxVolume;
        transform.localScale = new Vector3(1, newHeightScale, 1);

        // // Adjust the position so that the bottom of the water block stays in place
        // float heightAdjustment = (maxVolume - waterVolume) * blockHeight * 0.5f;
        // transform.localPosition = new Vector3(transform.localPosition.x, originalYPosition - heightAdjustment, transform.localPosition.z);
    }

}