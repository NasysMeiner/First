using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Warrior : Enemy
{  
    public override IEnumerator ApplyDamage()
    {
        yield return new WaitForSeconds(_timeBetweenAttacks);

        _objects[_currentAmount].TakeDamage(_damage);
        _time = 0;
        _audioShot.Play();

        if (_speed > 0)
            StopCoroutine(ApplyDamage());
        else
            StartCoroutine(ApplyDamage());
    }
}
