using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;
    [SerializeField] Vector3 offset = new Vector3(x: 0, y: 6, z: -8);

    private void Update()
    {
        this.transform.position = player.transform.position + offset;
    }
}
