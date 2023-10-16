using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class conditionData {

    public static int condition;
    public static int flood;
    
    public static int Condition
    {

        get
        {
            return condition;
        }

        set
        {
            condition = value;
        }
    }
    public static int Flood
    {

        get
        {
            return flood;
        }
        set
        {
            flood = value;
        }
    }
}
