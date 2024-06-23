using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
public class CharacterEvents
{
    //dmg and dmg value
    public static UnityAction<GameObject, int> characterDamaged;

    //health and health restored;
    public static UnityAction<GameObject, int> characterHealed;

}


