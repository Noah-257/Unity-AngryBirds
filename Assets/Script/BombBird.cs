using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBird : MonoBehaviour
{
 [SerializeField] float _launchForce = 500;
    [SerializeField] float _maxDragDistance = 5;

    [SerializeField] bool OutOfBoundsDetection = false;
    public GameObject OutOfBoundsArea;

    Vector2 _startPosition;
    Rigidbody2D _rigidbody2D;
    SpriteRenderer _spriteRenderer;

    bool isBirdOutOfBounds = false;

    public float impactField;
    public float force;
    public LayerMask lmToHit;
    public GameObject explosionPrefab;

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
        if(Input.GetKeyDown("space")){
            Debug.Log("Explosion");
            Explosion();
        }
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

    void Explosion(){
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, impactField, lmToHit);
        foreach(Collider2D obj in objects){
            Vector2 dir = obj.transform.position - transform.position;

            obj.GetComponent<Rigidbody2D>().AddForce(dir * force);
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, impactField);
    }
}
