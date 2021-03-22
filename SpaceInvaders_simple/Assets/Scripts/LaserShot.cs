using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShot : MonoBehaviour
{
    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    [SerializeField]
    protected CapsuleCollider2D capsuleCollider2D;

    protected int direction = 1;

    protected int movementSpeed = 10;
    protected int movementBasicSpeed = 10;

    protected Vector3 startPosition;

    protected string ignoreTagShip;
    protected string ignoreTagLaser;

    protected string targetTagShip;
    protected string targetTagLaser;

    public int indexOfShooter = -1;

    [SerializeField]
    protected GameObject explosionObject;

    private void OnEnable()
    {
        EnableLaser(true);

        movementSpeed = movementBasicSpeed;

        explosionObject.SetActive(false);
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ignoreTagShip) && collision.gameObject.CompareTag(ignoreTagLaser))
        {
            EnableLaser(false);

            DisableGameObject();
        }
        else if (collision.gameObject.CompareTag(targetTagShip) || collision.gameObject.CompareTag(targetTagLaser))
        {
            EnableLaser(false);
            movementSpeed = 0;

            explosionObject.SetActive(true);

            SoundManager.Instance.PlayExplosionSound();

            Invoke("DisableGameObject", 0.5f);
        }
        else if (collision.gameObject.CompareTag(TagsData.upWall) || collision.gameObject.CompareTag(TagsData.downWall))
        {
            EnableLaser(false);
            movementSpeed = 0;

            explosionObject.SetActive(true);
            
            SoundManager.Instance.PlayExplosionSound();

            Invoke("DisableGameObject", 0.5f);
        }
    }

    private void Start()
    {
        movementSpeed = movementBasicSpeed;
    }
    // Update is called once per frame
    protected void Update()
    {
        transform.localPosition += new Vector3(0, Time.deltaTime * movementSpeed * direction, 0);
    }

    protected void EnableLaser(bool enable)
    {
        spriteRenderer.enabled = enable;
        capsuleCollider2D.enabled = enable;
    }

    protected void DisableGameObject()
    {
        gameObject.SetActive(false);
    }
}
