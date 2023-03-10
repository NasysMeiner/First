using UnityEngine;

public class ArcherTower : Tower
{
    public int ShotsDone { get; private set; }

    public override void Shot(float damageBullet)
    {
        if(Time.timeScale > 0)
        {
            BulletArcherTower bullet = Instantiate(_bullet, transform.GetChild(1).position, Quaternion.identity);
            bullet.UpgradeDamage(damageBullet);
            bullet.InitializeEnemy(CalculetePosition(bullet), this);
            ShotsDone++;
        }
    }
}
