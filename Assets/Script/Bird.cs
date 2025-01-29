using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce = 500;
    [SerializeField] float _maxDragDistance = 5;

    [SerializeField] bool OutOfBoundsDetection = false;
    public GameObject OutOfBoundsArea;

    Vector2 _startPosition;
    Rigidbody2D _rigidbody2D;
    SpriteRenderer _spriteRenderer;

    bool isBirdOutOfBounds = false;

    public bool IsDragging { get; private set; }

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        _startPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true;
    }

    void OnMouseDown()
    {
        _spriteRenderer.color = Color.red;
        IsDragging = true;
    }

    void OnMouseUp()
    {
        Vector2 currentPosition = _rigidbody2D.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();

        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(direction * _launchForce); // Multiplying the direction changes the rate of which the bid moves 

        _spriteRenderer.color = Color.white;
        IsDragging = false;
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;

        float distance= Vector2.Distance(desiredPosition, _startPosition);
        if(distance > _maxDragDistance)
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * _maxDragDistance);
        }

        if (desiredPosition.x > _startPosition.x)
        {
            desiredPosition.x = _startPosition.x;
        }

        _rigidbody2D.position = desiredPosition;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == OutOfBoundsArea){
            
            ResetBird();
        } else {
            Debug.Log("Collision");
            StartCoroutine(ResetAfterDelay());
        }
        
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        _rigidbody2D.position = _startPosition;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
    }


    public void ResetBird(){
        Debug.Log("Out of bounds");
        _rigidbody2D.position = _startPosition;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
    }
}
