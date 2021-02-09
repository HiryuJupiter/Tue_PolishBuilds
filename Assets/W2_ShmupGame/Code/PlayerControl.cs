using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] GameObject pf_Bullet;
    [SerializeField] Transform shootPoint;

    [SerializeField] float moveBound_X;
    [SerializeField] float moveBound_Y;

    [SerializeField] float steering = 5f;
    [SerializeField] float moveSpeed = 20f;

    InputManager input;
    Vector2 velocity;
    Rigidbody2D rb;

    void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        input = InputManager.Instance;
    }

    void Update()
    {
        MoveControl();
        ShootControl();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 200, 20), "MovingLeft " + MovingLeft);
        GUI.Label(new Rect(20, 40, 200, 20), "MovingRight " + MovingRight);
        GUI.Label(new Rect(20, 60, 200, 20), "MovingUp " + MovingUp);
        GUI.Label(new Rect(20, 80, 200, 20), "MovingDown " + MovingDown);
        GUI.Label(new Rect(20, 100, 200, 20), "CanMoveLeft " + CanMoveLeft);
        GUI.Label(new Rect(20, 120, 200, 20), "CanMoveRight " + CanMoveRight);
        GUI.Label(new Rect(20, 140, 200, 20), "CanMoveUp " + CanMoveUp);
        GUI.Label(new Rect(20, 160, 200, 20), "CanMoveDown " + CanMoveDown);
    }

    void MoveControl ()
    {
        //targetVel.x = MovingWithinXBounds ? input.moveX : 0f;
        //targetVel.y = MovingWithinYBounds ? input.moveY : 0f;
        //raw velocity

        velocity = Vector2.Lerp(velocity, new Vector2(input.moveX, input.moveY) * Time.deltaTime, steering * Time.deltaTime);

        if (!MovingWithinXBounds)
            velocity.x = 0f;
        if (!MovingWithinYBounds)
            velocity.y = 0f;

        transform.Translate(velocity * moveSpeed * Time.deltaTime);
        //rb.velocity = velocity * moveSpeed * Time.deltaTime;
    }

    void ShootControl ()
    {
        if (input.shootDown)
        {
            Instantiate(pf_Bullet, shootPoint.position, Quaternion.identity);
        }
    }

    #region Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("player ontrigger enters somthing: ");
    }
    #endregion


    #region Helpers
    //Helpers
    bool MovingLeft => velocity.x < 0f;
    bool MovingRight => velocity.x > 0f;
    bool MovingUp => velocity.y < 0f;
    bool MovingDown => velocity.y > 0f;
    //bool MovingLeft => input.moveX < 0f;
    //bool MovingRight => input.moveX > 0f;
    //bool MovingUp => input.moveY < 0f;
    //bool MovingDown => input.moveY > 0f;
    bool CanMoveLeft => transform.position.x > -moveBound_X;
    bool CanMoveRight => transform.position.x < moveBound_X;
    bool CanMoveUp => transform.position.y > -moveBound_Y;
    bool CanMoveDown => transform.position.y < moveBound_Y;
    bool MovingWithinXBounds => (MovingLeft && CanMoveLeft) || (MovingRight && CanMoveRight);
    bool MovingWithinYBounds => (MovingUp && CanMoveUp) || (MovingDown && CanMoveDown);
    #endregion

}
