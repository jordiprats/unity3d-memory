using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    private static int cards = 7;

    public static int Cards
    {
        get 
        {
            return cards;
        }
        set 
        {
            cards = value;
        }
    }

	private static int level = 0;

    public static int Level
    {
        get 
        {
            return level;
        }
        set 
        {
            level = value;
        }
    }

}
