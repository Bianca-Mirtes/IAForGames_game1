using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum State
{
    IDLE,
    STALKING,
    ATTACKING1,
    ATTACKING2,
    DEAD
}

public class StateMachineController : MonoBehaviour
{
    public State state = State.IDLE;
    private State currentState;

    private Transform player;
    [SerializeField] private float speed=10;
    private Animator ani;
    private float distanceForPlayer;
    private bool areaIsActive = false;

    private void Start()
    {
        ani = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        distanceForPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceForPlayer < 10f)
        {
            state = State.STALKING;
            if (distanceForPlayer < 2f)
            {
                state = State.ATTACKING1;
            }
        }
        else
        {
            state = State.IDLE;
        }
    }

    private void FixedUpdate()
    {
        if(currentState != state)
        {
            currentState = state;
            switch (state)
            {
                case State.IDLE:
                    Idle();
                    break;
                case State.STALKING:
                    Stalking();
                    break;
                case State.ATTACKING1:
                    Attack();
                    break;
                case State.ATTACKING2:
                    Attack();
                    break;
                case State.DEAD:
                    Dead();
                    break;
            }
        }
    }
    private void Stalking()
    {
        Vector3 direcao = player.position - transform.position;

        // Normaliza o vetor direção para obter um vetor unitário
        direcao.Normalize();

        // Move o objeto na direção do alvo
        transform.position += direcao * speed * Time.deltaTime;
    }

    private void Idle()
    {
        //ani.SetBool("idle", true);
    }

    private void Attack()
    {
        transform.GetComponent<Animator>().SetBool("isAttacking", true);
        transform.GetChild(0).GetComponent<Animator>().SetBool("isAttacking", true);
    }

    private void Dead()
    {
        Destroy(gameObject, 1.5f);
    }
}
