using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;


    class GameHelper
    {
        public static Vector3 getRandomPos(System.Random r)
        {
            return new Vector3(r.Next(GameConfig.Left,GameConfig.Right), r.Next(GameConfig.Bottom,GameConfig.Top), 0);

        }
    }
