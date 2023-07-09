using System;
using System.Runtime.Serialization;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class DeadEnemy : LDBrick
{
    private bool bCanInteract = false;

    [Serializable]
    public enum EnemyType
    {
        TOMB,
        GLOOPY,
        SHOOTER
    }

    [SerializeField]
    private GameObject tombObject;
    [SerializeField]
    private GameObject gloopyObject;
    [SerializeField]
    private GameObject shooterObject;

    [SerializeField]
    private EnemyType wantedEnemyType = EnemyType.GLOOPY;

    private EnemyType enemyType = EnemyType.TOMB;

    private PlayerController2D pc;


    private void Awake()
    {
        pc = FindObjectOfType<PlayerController2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == pc.gameObject)
            bCanInteract = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == pc.gameObject)
            bCanInteract = false;
    }

    private void Update()
    {
        if (!bCanInteract)
            return;

        if (Input.GetKeyDown(KeyCode.E) && !pc.GetIsCarrying())
        {
            enemyType += 1;
            enemyType = (EnemyType)((int)enemyType % 3);

            switch (enemyType)
            {
                case EnemyType.TOMB:
                    tombObject.SetActive(true);
                    gloopyObject.SetActive(false);
                    shooterObject.SetActive(false);
                    break;
                case EnemyType.GLOOPY:
                    tombObject.SetActive(false);
                    gloopyObject.SetActive(true);
                    shooterObject.SetActive(false);
                    break;
                case EnemyType.SHOOTER:
                    tombObject.SetActive(false);
                    gloopyObject.SetActive(false);
                    shooterObject.SetActive(true);
                    break;
                default:
                    break;
            }

            bFinished = enemyType == wantedEnemyType;
        }
    }
}