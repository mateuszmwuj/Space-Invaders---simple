using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicShip : MonoBehaviour
{
    protected EnemyMovementTriggerEvents enemyColliderTriggerEvent;

    [SerializeField]
    protected GameObject explosionObject;

    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    [SerializeField]
    protected BoxCollider2D boxCollider2D;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagsData.playerLaserShot))
        {
            explosionObject.SetActive(true);

            SetComponentsStateTo(false);

            Invoke("DisableShip", 0.5f);
        }
        else if (collision.CompareTag(TagsData.leftWall))
        {
            EnemyMovementTriggerEvents.LeftWallEnter?.Invoke();
        }
        else if (collision.CompareTag(TagsData.rightWall))
        {
            EnemyMovementTriggerEvents.RightWallEnter?.Invoke();
        }
        else if (collision.CompareTag(TagsData.downWall))
        {
            EnemyCollisionTriggerEvents.DownWallEnter?.Invoke();
        }
        else if (collision.CompareTag(TagsData.player))
        {
            EnemyCollisionTriggerEvents.PlayerContact?.Invoke();

            explosionObject.SetActive(true);

            SetComponentsStateTo(false);

            Invoke("DisableShip", 0.5f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyColliderTriggerEvent = gameObject.GetComponent<EnemyMovementTriggerEvents>();
    }

    protected void DisableShip()
    {
        EnemyDiesEvents.EnemyDies();

        gameObject.SetActive(false);
    }
    
    protected void SetComponentsStateTo(bool enable)
    {
        spriteRenderer.enabled = enable;
        boxCollider2D.enabled = enable;
    }
}
