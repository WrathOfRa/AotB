using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    public Animator anim;
    public Player player;

    public string nextAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        player = GetComponentInParent<Player>();
        nextAnim = "None";
    }

    // Update is called once per frame
    void Update()
    {
       

        ChooseAnimation();
        
    }

    void ChooseAnimation()
    {

        MovementAnimations();
        AttackAnimations();

        if (!CurrentlyPlaying(nextAnim))
            PlayAnimation(nextAnim);
    }

    void AttackAnimations()
    {
        if (player.isAttacking)
        {
            nextAnim = Attacking();
        }
    }

    void MovementAnimations()
    {
        if (player.movement.magnitude > 0.0f)
        {
            if (player.isSprinting)
                nextAnim = Sprinting();
            else
                nextAnim = Walking();
        }
        else
            nextAnim = Standing();
    }

    bool CurrentlyPlaying(string s)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(s);
    }

    void PlayAnimation(string s)
    {
        anim.Play(s);
    }

    string Walking()
    {
        switch (player.direction)
        {
            case 'U':
                return "Player_Walk_Up";
            case 'D':
                return "Player_Walk_Down";
            case 'L':
                return "Player_Walk_Left";
            case 'R':
                return "Player_Walk_Right";
        }
        return "None";
    }

    string Standing()
    {
        switch (player.direction)
        {
            case 'U':
                return "Player_Stand_Up";
            case 'D':
                return "Player_Stand_Down";
            case 'L':
                return "Player_Stand_Left";
            case 'R':
                return "Player_Stand_Right";
        }
        return "None";
    }

    string Sprinting()
    {
        switch (player.direction)
        {
            case 'U':
                return "Player_Run_Up";
            case 'D':
                return "Player_Run_Down";
            case 'L':
                return "Player_Run_Left";
            case 'R':
                return "Player_Run_Right";
        }
        return "None";
    }

    string Attacking()
    {
        switch (player.direction)
        {
            case 'U':
                return "Player_Attack_Up";
            case 'D':
                return "Player_Attack_Down";
            case 'L':
                return "Player_Attack_Left";
            case 'R':
                return "Player_Attack_Right";
        }
        return "None";
    }
}
