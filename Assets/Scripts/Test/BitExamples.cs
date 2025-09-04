using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerFlags : uint
{
    None = 0,
    OnGround = 1 << 0,  // 1
    Jumping = 1 << 1,   // 2
    Sprinting = 1 << 2, // 4
    Crouching = 1 << 3, // 8
    Invisible = 1 << 4  // 16
}

public class BitExamples
{
    public static bool HasFlag(PlayerFlags flags, PlayerFlags f)
    {
        // 해당 비트가 켜져 있으면 true를 반환.
        if((flags & f) != 0)
        {
            return true;
        }

        return false;
    }

    public static PlayerFlags SetFlag(PlayerFlags flags, PlayerFlags f)
    {
        flags = flags | f;
        return flags;
    }

    public static PlayerFlags ClearFlag(PlayerFlags flags, PlayerFlags f)
    {
        flags = (flags & ~f);
        return flags;
    }
}
