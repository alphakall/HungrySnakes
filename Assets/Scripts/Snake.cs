using UnityEngine;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();  // intialising list whilst creating and starting game

    public Transform segmentPrefab;                  //usually type is gameobject but in this case its Transfrom (This creates a property in editor where we can add prefab refrence)
    public int initialSize = 4; 

    private void Start()
    {
        ResetState();
        //_segments = new List<Transform>();                                                                // using list to create snake body
       // _segments.Add(this.transform);                                                                   // head of the snake (object that this script is attached to )
    }


    private void Update()
    {
        HandleInput();

        
    }
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (_direction != Vector2.down)           // to prevent direct backward movement
            {
                _direction = Vector2.up;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (_direction != Vector2.up)
            {
                _direction = Vector2.down;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (_direction != Vector2.right)
            {
                _direction = Vector2.left;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (_direction != Vector2.left)
            {
                _direction = Vector2.right;
            }
        }

    }
    private void FixedUpdate()
    {
        for(int i = _segments.Count -1; i>0; i--) // snake tail assigns it's postion here in loop to make the body move forward
        {
            _segments[i].position = _segments[i - 1].position;   
        }

        // After executing code for body the head postion gets calculated here below

        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x)
                                  + _direction.x, Mathf.Round(this.transform.position.y)
                                  + _direction.y, 0.0f);
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);  // creates a copy of prefab

        segment.position = _segments[_segments.Count - 1].position; // setting postion of prefab(Tail/body) with last count

        _segments.Add(segment); // adding to list
    }

    private void ResetState()
    {
        for(int i = 1; i < _segments.Count; i++)       // Deletes body 
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);          // Adds back head

        for(int i=1; i< this.initialSize; i++)      // intial body size of snake on rst (includes head)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }
        else if(other.tag =="Obstacle")
        {
            ResetState();
        }
    }



}