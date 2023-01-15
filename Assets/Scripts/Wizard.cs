using System.Collections;
using UnityEngine;

public class Wizard : Enemy
{
    [SerializeField] private BulletWizard _bulletWizard;

    public override IEnumerator ApplyDamage()
    {
        bool isWork = true;

        while (isWork)
        {
            yield return new WaitForSeconds(_timeBetweenAttacks);

            var bullet = Instantiate(_bulletWizard, transform.position, Quaternion.identity);
            bullet.Inst((_target - transform.position).normalized, _trash);
            _time = 0;
            _audioShot.Play();

            if (_speed > 0)
            {
                isWork = false;
                StopCoroutine(ApplyDamage());
            }
        }
    }
}
