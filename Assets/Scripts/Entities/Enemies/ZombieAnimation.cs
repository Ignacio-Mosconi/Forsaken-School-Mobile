﻿using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ZombieAI))]
[RequireComponent(typeof(Life))]

public class ZombieAnimation : MonoBehaviour
{
    [SerializeField] AnimatorOverrideController animatorOverrideController;
    
    Animator animator;
    NavMeshAgent agent;
    ZombieAI zombieAI;
    Life zombieLife;

	void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        zombieAI = GetComponent<ZombieAI>();
        zombieLife = GetComponent<Life>();
	}

	void Start()
    {
        zombieLife.OnHit.AddListener(HitAnimation);
        zombieLife.OnDeath.AddListener(DeathAnimation);
        zombieAI.OnAttack.AddListener(AttackAnimation);

        animator.runtimeAnimatorController = animatorOverrideController;
    }

	void Update()
    {
        Vector3 horizontalVelocity = new Vector3(agent.velocity.x, 0, agent.velocity.z);
        float normalizedVelocity = horizontalVelocity.magnitude / zombieAI.MaxSpeed;

        animator.SetFloat("Horizontal Velocity", normalizedVelocity, 0.2f, Time.deltaTime);
	}

    void HitAnimation()
    {
        animator.SetTrigger("Was Hit");
    }

    void DeathAnimation()
    {
        animator.SetTrigger("Death");
    }

    void AttackAnimation()
    {
        animator.SetTrigger("Has Attacked");
    }
}
