using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    public virtual float GetAxisRaw(string axis)
    {
        return 0.0f;
    }

    public virtual bool GetSprintButton()
    {
        return false;
    }

    public virtual bool GetAttackButton()
    {
        return false;
    }

    public virtual int GetNumberRow()
    {
        return -1;
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

}


public class PlayerInput : GameInput
{
    public override float GetAxisRaw(string axis)
    {
        return Input.GetAxisRaw(axis);
    }

    public override bool GetSprintButton()
    {
        return Input.GetKeyDown(KeyCode.LeftShift);
    }

    public override bool GetAttackButton()
    {
        return Input.GetMouseButtonDown(0);
    }

    public override int GetNumberRow()
    {
        KeyCode[] keyCodes =
            {
                //KeyCode.Alpha0,
                KeyCode.Alpha1,
                KeyCode.Alpha2,
                //KeyCode.Alpha3,
                //KeyCode.Alpha4,
                //KeyCode.Alpha5,
                //KeyCode.Alpha6,
                //KeyCode.Alpha7,
                //KeyCode.Alpha8,
                //KeyCode.Alpha9,
            };
        for (int i = 0; i < keyCodes.Length;  i++)
        {
            if (Input.GetKeyDown(keyCodes[i])) return i;
        }
        return -1;
    }
}



public class ComputerInput : GameInput
{
    public override float GetAxisRaw(string axis)
    {
        return 0.0f;
    }

    public override bool GetSprintButton()
    {
        return false;
    }

    public override bool GetAttackButton()
    {
        return true;
    }

    public override int GetNumberRow()
    {
        return -1;
    }
}
