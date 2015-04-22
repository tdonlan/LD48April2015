using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum HarpoonState
{
    Waiting,
    Shooting,
    Retrieving
}
    public enum EnemyState
    {
        Default,
        Captured,
        Turned,
        Shot,
    }

    public enum EnemyType
    {
    Seeker, //kamakazi
    Shooter, //moves near to the player, shoots bullets
    Tank // spawns seekers, can't be harpooned
    }

public enum PlayerState
{
    Harpoon,
    Captive
}
