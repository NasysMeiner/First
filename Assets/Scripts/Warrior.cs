using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Enemy
{  
    public override void ApplyDamage()
    {
        if (_speed == 0)
        {
            if (_time >= _timeBetweenAttacks)
            {
                _objects[_currentAmount].TakeDamage(_damage);
                _time = 0;
                _audioShot.Play();

                return;
            }

            _time += Time.deltaTime;
        }
    }
}
