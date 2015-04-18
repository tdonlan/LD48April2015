using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;


    class GameHelper
    {
        public static Vector3 getRandomPos(System.Random r)
        {
            return new Vector3(r.Next(-500, 500), r.Next(-500, 500), 0);

        }
    }
