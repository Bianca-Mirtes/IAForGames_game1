using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum State
{
    IDLE,
    STALKING,
    ATTACKING,
    DEAD
}

public class StateMachineController : MonoBehaviour
{
    public State state = State.IDLE;
    private State currentState;

    private Transform player;
    [SerializeField] private float speed=8;
    private float distanceForPlayer;
    private bool areaIsActive = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (GetComponent<EnemyController>().health <= 0)
            state = State.DEAD;

        if (GetComponent<EnemyController>().health < 50)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            GetComponent<EnemyController>().damage = 10;
        }
        
        if(state != State.DEAD)
        {
            distanceForPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceForPlayer < 10f)
            {
                state = State.STALKING;
                if (distanceForPlayer < 4f)
                    state = State.ATTACKING;
            }
            else
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
                case State.ATTACKING:
                    Attack();
                    break;
                case State.DEAD:
                    Dead();
                    break;
            }
        }
    }
    public void Stalking()
    {
        Vector3 direcao = player.position - transform.position;

        // Normaliza o vetor direção para obter um vetor unitário
        direcao.Normalize();

        // Move o objeto na direção do alvo
        transform.position += direcao * speed * Time.deltaTime;
    }

    public void Idle()
    {
        
    }

    public void Attack()
    {
        GetComponent<EnemyController>().Shoot();
    }

    private void Dead()
    {
        Destroy(gameObject, 1f);
    }
}
