using System;
using System.Collections.Generic;

[Serializable]
public class SaveObject
{
    public bool isGreeceUnlocked = false;

    public bool isSpaceUnlocked = false;

    public List<int> unlockedEgyptLevels = new List<int>
    {
        1
    };

    public List<int> unlockedGreeceLevels = new List<int>
    {
        1
    };

    public List<int> unlockedSpaceLevels = new List<int>
    {
        1
    };
}
