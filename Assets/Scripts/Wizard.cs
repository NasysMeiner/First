using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy
{
    [SerializeField] private BulletWizard _bulletWizard;

    public override void ApplyDamage()
    {
        if (_speed == 0)
        {
            if (_time >= _timeBetweenAttacks)
            {
                var bullet = Instantiate(_bulletWizard, transform.position, Quaternion.identity);
                bullet.Inst((_target - transform.position).normalized, _trash);
                _time = 0;
                _audioShot.Play();

                return;
            }

            _time += Time.deltaTime;
        }
    }
}
