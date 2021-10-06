using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public int playerspeed = 0;
    public Camera main_camera;
    Animator animator;
    Vector3 position_maincharacter;
    Rigidbody2D rigid2d;
    Vector2 movement;
    string animationState = "MoveState";
    enum States
    {
        right = 4,
        left = 3,
        up = 1,
        down = 2,
        idle = 5
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid2d = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Playermove();

    }
    void Update()
    {
        UpdateState();
        position_maincharacter = new Vector3(transform.position.x, transform.position.y, -10);
        main_camera.transform.position = position_maincharacter;
    }

    private void Playermove()
    {
        movement = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.D))
        {
            movement = new Vector2(playerspeed * Time.deltaTime, 0);
            rigid2d.MovePosition(rigid2d.position + movement);
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement = new Vector2(-playerspeed * Time.deltaTime, 0);
            rigid2d.MovePosition(rigid2d.position + movement);
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement = new Vector2(0, -playerspeed * Time.deltaTime);
            rigid2d.MovePosition(rigid2d.position + movement);
        }
        if (Input.GetKey(KeyCode.W))
        {
            movement = new Vector2(0, playerspeed * Time.deltaTime);
            rigid2d.MovePosition(rigid2d.position + movement);
        }
        Moving_event();
    }

    void Moving_event(){
        
    }
    private void UpdateState()
    {
        if (movement.x > 0)
        {
            animator.SetInteger(animationState, (int)States.right);
        }
        else if (movement.x < 0)
        {
            animator.SetInteger(animationState, (int)States.left);
        }
        else if (movement.y > 0)
        {
            animator.SetInteger(animationState, (int)States.up);
        }
        else if (movement.y < 0)
        {
            animator.SetInteger(animationState, (int)States.down);
        }
        else
        {
            animator.SetInteger(animationState, (int)States.idle);
        }
    }
}
