using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    private int movementSpeed = 5;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;

    private bool canMove = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LeftWall"))
        {
            canMoveLeft = false;
        }
        else if (collision.gameObject.CompareTag("RightWall"))
        {
            canMoveRight = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LeftWall"))
        {
            canMoveLeft = true;
        }
        else if (collision.gameObject.CompareTag("RightWall"))
        {
            canMoveRight = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        canMoveLeft = true;
        canMoveRight = true;

        canMove = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                MoveShipLeft(true);
            else if (Input.GetKey(KeyCode.RightArrow))
                MoveShipRight(true);

            MoveShipLeft();
            MoveShipRight();
        }
    }

    public void MoveShipLeft(bool byKeyboard = false)
    {
        rigidbody2D.velocity = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal") * movementSpeed, 0f);
        if (canMoveLeft && byKeyboard)
            transform.position -= new Vector3(CrossPlatformInputManager.GetAxis("Horizontal") * movementSpeed, 0, 0);
    }
    public void MoveShipRight(bool byKeyboard = false)
    {
        rigidbody2D.velocity = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal") * movementSpeed, 0f);

        if (canMoveRight && byKeyboard)
            transform.position += new Vector3(CrossPlatformInputManager.GetAxis("Horizontal") * movementSpeed, 0, 0);
    }

    public void SetMovementActive(bool active)
    {
        canMove = active;
    }
}
