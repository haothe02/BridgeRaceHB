using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayRun()
    {
        _animator.Play("Run");
    }
    public void PlayIdle() 
    {
        _animator.Play("Idle");
    }
    public void PlayVictory()
    {
        _animator.Play("Victory");
    }
}
