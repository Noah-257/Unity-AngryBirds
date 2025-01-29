using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragLine : MonoBehaviour
{
    LineRenderer _lineRenderer;
    BombBird _bird;
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _bird = FindObjectOfType<BombBird>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_bird.IsDragging){
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(1, _bird.transform.position);
        } else {
            _lineRenderer.enabled = false;
        }
        
    }
}
