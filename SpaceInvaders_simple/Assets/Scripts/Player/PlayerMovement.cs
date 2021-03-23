using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    private int movementSpeed = 5;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;

    private bool canMove = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagsData.leftWall))
        {
            canMoveLeft = false;
        }
        else if (collision.gameObject.CompareTag(TagsData.rightWall))
        {
            canMoveRight = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagsData.leftWall))
        {
            canMoveLeft = true;
        }
        else if (collision.gameObject.CompareTag(TagsData.rightWall))
        {
            canMoveRight = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        canMoveLeft = true;
        canMoveRight = true;

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MoveShipByButtons();

            MovementByKeyboard();
        }
    }

    public void MoveShipByButtons()
    {
        _rigidbody2D.velocity = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal") * movementSpeed, 0f);
    }

    private void MovementByKeyboard()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (canMoveLeft == true)
                transform.position -= new Vector3(Time.deltaTime * movementSpeed, 0, 0);

        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (canMoveRight == true)
                transform.position += new Vector3(Time.deltaTime * movementSpeed, 0, 0);
        }
    }
        public void SetMovementActive(bool active)
    {
        canMove = active;
    }
}
