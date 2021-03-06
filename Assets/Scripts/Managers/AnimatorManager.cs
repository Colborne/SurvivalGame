using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{    
    SoundManager soundManager;
    public Animator animator;
    int horizontal;
    int vertical;

    void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("H");
        vertical = Animator.StringToHash("V");   
        soundManager = GetComponent<SoundManager>();
    }

    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnimation, 0.2f);
    }
    
    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting, bool isSneaking, bool isSwimming)
    {
        float snappedHorizontal;
        float snappedVertical;
/*
        #region Snapped Horizontal
        if(horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }
        else if(horizontalMovement > 0.55f)
        {
            snappedHorizontal = 1;
        }
        else if(horizontalMovement < 0 && horizontalMovement > -.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if(horizontalMovement < -0.55f)
        {
            snappedHorizontal = -1;
        }
        else
        {
            snappedHorizontal = 0;
        }
        #endregion
*/

        snappedHorizontal = horizontalMovement;

        #region Snapped Vertical
        if(verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if(verticalMovement > 0.55f)
        {
            snappedVertical = 1;
        }
        else if(verticalMovement < 0 && verticalMovement > -.55f)
        {
            snappedVertical = -0.5f;
        }
        else if(verticalMovement < -0.55f)
        {
            snappedVertical = -1;
            
        }
        else
        {
            snappedVertical = 0;
        }
        #endregion

        if(isSprinting)
        {
            snappedHorizontal = horizontalMovement;
            snappedVertical = 2;
        }
        else if(isSneaking)
        {
            if(verticalMovement > 0)
            {
                snappedHorizontal = horizontalMovement;
                snappedVertical = 3;
            }
        }
        
        if(isSwimming)
        {            
            snappedHorizontal = horizontalMovement;
            if(verticalMovement > 0)
                snappedVertical = 5;
            else
                snappedVertical = 4;
        }

        animator.SetFloat(horizontal, snappedHorizontal);//, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }
}
