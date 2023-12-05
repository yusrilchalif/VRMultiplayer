using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AvatarController : MonoBehaviour
{
    public Transform[] spawnPoint;

    public void SetSpawnPosition(int spawnIndex)
    {
        if (spawnIndex >= 0 && spawnIndex < spawnPoint.Length)
        {
            Vector3 spawnPos = spawnPoint[spawnIndex].position;
            Quaternion spawnRot = spawnPoint[spawnIndex].rotation;

            transform.position = new Vector3(spawnPos.x, spawnPos.y, spawnPos.z);
            transform.rotation = spawnRot;
        }
        else
        {
            Debug.LogError("Spawn index out of range. ");
        }
    }
}
