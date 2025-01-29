using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float timeOffset;
    [SerializeField] Vector2 posOffset;

    [SerializeField] float leftLimit;
    [SerializeField] float rightLimit;
    [SerializeField] float bottomLimit;
    [SerializeField] float topLimit;


    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Cameras current position
        Vector3 startPos = transform.position;

        //Players current position
        Vector3 endPos = player.transform.position;

        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -10;

        //Smoothly move the camera towards the players posiion
        transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);

        transform.position = new Vector3 (
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit), 
            Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
            transform.position.z
        );
    }

    private void OnDrawGizmos(){
        //draw a box around the camera boundary
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));
        
            
    }
}
