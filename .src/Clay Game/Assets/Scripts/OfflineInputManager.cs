using UnityEngine;
using System.Collections;

public static class OfflineInputManager{


    static bool leftDown;
    public static bool LeftDown
    {
        get
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Q))
                return true;
            if (leftDown)
            {
                leftDown = false;
                return true;
            }
            else
            {
                return false;
            }

        }
        set { leftDown = value; }
    }

    static bool rightDown;
    public static bool RightDown
    {
        get
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                return true;
            if (rightDown)
            {
                rightDown = false;
                return true;
            }
            else
            {
                return false;
            }

        }
        set { rightDown = value; }
    }

    static bool downDown;
    public static bool DownDown
    {
        get
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                return true;
            if (downDown)
            {
                downDown = false;
                return true;
            }
            else
            {
                return false;
            }

        }
        set { downDown = value; }
    }

    static bool upDown;
    public static bool UpDown
    {
        get
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z))
                return true;
            if (upDown)
            {
                upDown = false;
                return true;
            }
            else
            {
                return false;
            }

        }
        set { upDown = value; }
    }

    static bool aDown;
    public static bool Adown
    {
        get
        {
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                return true;
            if (aDown)
            {
                aDown = false;
                return true;
            }
            else
            {
                return false;
            }

        }
        set { aDown = value; }
    }

    static bool bDown;
    public static bool Bdown
    {
        get
        {
            if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Backspace))
                return true;
            if (bDown)
            {
                bDown = false;
                return true;
            }
            else
            {
                return false;
            }
          

        }
        set { bDown = value; }
    }

    static bool xDown;
    public static bool Xdown
    {
        get
        {
            if (Input.GetKeyDown(KeyCode.V))
                return true;
            if (xDown)
            {
                xDown = false;
                return true;
            }
            else
            {
                return false;
            }

        }
        set { xDown = value; }
    }
}
