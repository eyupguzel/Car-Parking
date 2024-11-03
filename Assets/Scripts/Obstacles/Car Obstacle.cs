using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

class CarObstacle : BaseObstacle
{
    private Vector3 startPosition;
    private enum Direction
    {
        right,
        left
    }

    [SerializeField] private Direction direction;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (transform.position.x <= -13)
        {
            transform.position = startPosition;
        }
        
        switch (direction)
        {
            case Direction.right: ObstacleAnimation();
                break;
            case Direction.left: ObstacleAnimation();
                break;
        }
    }

    public override void ObstacleAnimation()
    {
        transform.position += transform.forward * (3f * Time.deltaTime);
    }
    
}
