using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 offset = new Vector3(x: 0, y: 6, z: -8);
    [SerializeField] float delayFollowSpeed;


    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, delayFollowSpeed * Time.deltaTime);       
    }
}
