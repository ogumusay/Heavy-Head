using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTouchInput : MonoBehaviour
{
    private static float horizontalInput = 0;
    private static bool jumpDown = false;
    private static bool clickedJumpBefore = false;

    private static bool attack = false;
    private static bool clickedAttackBefore = false;

    private static float accelerationSpeed = 2.6f;
    private static float slowingSpeed = 3f;

    private static bool isLeftUp = true;
    private static bool isRightUp = true;

    public void LeftButtonDown()
    {
        isLeftUp = false;
    }
        
    public void LeftButtonUp()
    {
        isLeftUp = true;
    }


    public void RightButtonDown()
    {
        isRightUp = false;
    }

    public void RightButtonUp()
    {
        isRightUp = true;
    }

    public void JumpButtonDown()
    {
        jumpDown = true;
    }

    public void JumpButtonUp()
    {
        clickedJumpBefore = false;
        jumpDown = false;
    }

    public static bool GetJumpButtonKey()
    {
        if (!clickedJumpBefore && jumpDown)
        {
            clickedJumpBefore = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AttackButtonDown()
    {
        attack = true;
    }

    public void AttackButtonUp()
    {
        clickedAttackBefore = false;
        attack = false;
    }

    public static bool GetAttackButtonKey()
    {
        if (!clickedAttackBefore && attack)
        {
            clickedAttackBefore = true;
            horizontalInput = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static float GetHorizontalAxis()
    {
        if (!isLeftUp)
        {
            if(horizontalInput > -1f)
            {
                if (horizontalInput > 0f)
                {
                    horizontalInput = 0f;
                }

                horizontalInput -= Time.deltaTime * accelerationSpeed;
            }
            else
            {
                horizontalInput = -1f;
            }
        }
        else if (!isRightUp)
        {
            if (horizontalInput < 1f)
            {
                if (horizontalInput < 0f)
                {
                    horizontalInput = 0f;
                }

                horizontalInput += Time.deltaTime * accelerationSpeed;
            }
            else
            {
                horizontalInput = 1f;
            }
        }
        else
        {
            if (horizontalInput > 0)
            {
                horizontalInput -= Time.deltaTime * slowingSpeed;

                if (horizontalInput <= 0)
                {
                    horizontalInput = 0f;
                }
            }
            else if (horizontalInput < 0)
            {
                horizontalInput += Time.deltaTime * slowingSpeed;

                if (horizontalInput >= 0)
                {
                    horizontalInput = 0f;
                }
            }
        }

        return horizontalInput;
    }

    private void OnDisable()
    {
        horizontalInput = 0;
        isLeftUp = true;
        isRightUp = true;
    }
}
