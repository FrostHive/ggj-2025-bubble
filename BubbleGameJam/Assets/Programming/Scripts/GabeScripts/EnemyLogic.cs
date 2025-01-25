using UnityEngine;
using System;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer; // For ground detection

    private Rigidbody rBody;
    public enum EnemyType {FLOAT, JUMP, CHARGE};
    public EnemyType type;

    [SerializeField] float moveSpeed = 5;
    [SerializeField] bool active = true;
    [SerializeField] Animator animator;
    GameObject player;
    float timer = 0;
    bool reachedDestination = false;
    [SerializeField] float waitTime = 1.5f;// used for the delays between jumping and charging

    [Header("Float Variables")]
    bool isFloating = false;

    [Header("Jump Variables")]
    Vector3 endPos;    //the ending position of the jump, helps reverse the jump to go back and fourth
    ParabolaController paraB;

    [Header("Charge Variables")]
    [SerializeField] float detectDist = 10f; //the distance it takes for the enemy to detect the player
    bool charging = false;
    Vector3 chargeTarget;

    void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (!animator) animator = GetComponent<Animator>();
        
        switch (type)
        {
            case EnemyType.FLOAT:
                rBody.useGravity = false;
                //PlayAnimation("Idle");
                break;
            case EnemyType.JUMP:
                rBody.useGravity = false;
                paraB = GetComponent<ParabolaController>();
                paraB.Speed = moveSpeed;
                paraB.FollowParabola();
                Transform[] points = paraB.getPoints();
                endPos = points[points.Length - 1].position;
                break;
            case EnemyType.CHARGE:
                if(paraB)
                    paraB.enabled = false;
                break;
        }
    }


        void FixedUpdate()
    {
        if (!active)
            return;
        MovementLogic();
    }

    void MovementLogic()
    {
        switch (type)
        {
            case EnemyType.FLOAT:
                break;
            case EnemyType.JUMP:

                if (!reachedDestination)
                {
                    if (Vector3.Distance(transform.position, endPos) <= 0.5f)
                    {
                        reachedDestination = true;
                        Transform[] points = paraB.getPoints();
                        Array.Reverse(points);
                        endPos = points[points.Length - 1].position;
                        paraB.SetPoints(points);
                    }
                }
                else
                {
                    timer += Time.deltaTime;
                    if (timer >= waitTime)
                    {
                        reachedDestination = false;
                        timer = 0;
                        paraB.FollowParabola();
                    }
                }
               
                break;
            case EnemyType.CHARGE:
                if (charging)//if charging at the player, move towards their detected position
                {
                    transform.position = Vector3.MoveTowards(transform.position, chargeTarget, moveSpeed*0.01f);
                    if (Vector3.Distance(transform.position, chargeTarget) <= 0.5f)
                    { 
                        charging = false;
                        reachedDestination = true;
                    }             
                }
                else//after charging towards the player, have a delay until the next charge. After waiting, will detect if the player is in range
                {
                    if(reachedDestination)
                    {
                        timer += Time.deltaTime;
                        if (timer >= waitTime)
                        {
                            reachedDestination = false;
                        }
                    }
                    else 
                        if (Vector3.Distance(transform.position, player.transform.position) <= detectDist)
                        {
                            charging = true;
                            chargeTarget = player.transform.position;
                        }
                }
                break;

        }
    }

    void PlayAnimation(string animationName)
    {
        animator.SetBool(animationName, true);
    }
}
