using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    BulletPool bullet;
    BulletHitPool hit;
    ExplosionPool explosion;
    PowerUpPool powerUp;

    OldEnemyPool enemy;
    OldAsteroidPool asteroid;

    EnemyWavePool enemyWave;
    EnemyBonusPool enemyBonus;
    EnemyCurvePool enemyCurve;

    EnemyAsteroidBigPool enemyAsteroidBig;
    EnemyAsteroidSmallPool enemyAsteroidSmall;

    EnemyBossPool enemyBoss;
    BossBulletPool bossBullet;
    BossMissilePool bossMissile;

    protected override void OnInitialize()
    {
        bullet = GetComponentInChildren<BulletPool>();
        if (bullet != null)
        {
            bullet.Initialized();
        }

        hit = GetComponentInChildren<BulletHitPool>();
        if (hit != null)
        {
            hit.Initialized();
        }

        explosion = GetComponentInChildren<ExplosionPool>();
        if (explosion != null)
        {
            explosion.Initialized();
        }

        powerUp = GetComponentInChildren<PowerUpPool>();
        if (powerUp != null)
        {
            powerUp.Initialized();
        }

        enemy = GetComponentInChildren<OldEnemyPool>();
        if (enemy != null)
        {
            enemy.Initialized();
        }

        asteroid = GetComponentInChildren<OldAsteroidPool>();
        if (asteroid != null)
        {
            asteroid.Initialized();
        }

        enemyWave = GetComponentInChildren<EnemyWavePool>();
        if (enemyWave != null)
        {
            enemyWave.Initialized();
        }

        enemyBonus = GetComponentInChildren<EnemyBonusPool>();
        if (enemyBonus != null)
        {
            enemyBonus.Initialized();
        }

        enemyCurve = GetComponentInChildren<EnemyCurvePool>();
        if (enemyCurve != null)
        {
            enemyCurve.Initialized();
        }

        enemyAsteroidBig = GetComponentInChildren<EnemyAsteroidBigPool>();
        if (enemyAsteroidBig != null)
        {
            enemyAsteroidBig.Initialized();
        }

        enemyAsteroidSmall = GetComponentInChildren<EnemyAsteroidSmallPool>();
        if (enemyAsteroidSmall != null)
        {
            enemyAsteroidSmall.Initialized();
        }

        bossBullet = GetComponentInChildren<BossBulletPool>();
        if(bossBullet != null)
        {
            bossBullet.Initialized();
        }
        bossMissile = GetComponentInChildren<BossMissilePool>();
        if (bossMissile != null)
        {
            bossMissile.Initialized();
        }
        enemyBoss = GetComponentInChildren<EnemyBossPool>();
        if (enemyBoss != null)
        {
            enemyBoss.Initialized();
        }
    }

    // 풀에서 오브젝트 가져오는 함수들 =========================================================================
    public Bullet GetBullet(Vector3? position, float angle = 0.0f)
    {
        //Vector3.forward * angle
        return bullet.GetObject(position, new Vector3(0, 0, angle));
    }
    public Explosion GetBulletHit(Vector3? position)
    {
        return hit.GetObject(position);
    }
    public Explosion GetExplosion(Vector3? position)
    {
        return explosion.GetObject(position);
    }
    public PowerUp GetPowerUp(Vector3? position)
    {
        return powerUp.GetObject(position);
    }

    public OldEnemy GetEnemy(Vector3? position, float angle = 0.0f)
    {
        return enemy.GetObject(position, new Vector3(0, 0, angle));
    }
    public OldAsteroid GetAsteroid(Vector3? position)
    {
        return asteroid.GetObject(position);
    }

    public EnemyWave GetEnemyWave(Vector3? position)
    {
        return enemyWave.GetObject(position);
    }

    /// <summary>
    /// 보너스 적 하나를 돌려주는 함수
    /// </summary>
    /// <param name="position">생성 위치</param>
    /// <returns></returns>
    public EnemyBonus GetEnemyBonus(Vector3? position)
    {
        return enemyBonus.GetObject(position);
    }

    /// <summary>
    /// 커브도는 적 하나를 돌려주는 함수
    /// </summary>
    /// <param name="position">생성 위치</param>
    /// <returns></returns>
    public EnemyCurve GetEnemyCurve(Vector3? position)
    {
        EnemyCurve curve = enemyCurve.GetObject(position);
        curve.UpdateRotateDirection();

        return curve;
    }

    /// <summary>
    /// 큰 운석 하나를 돌려주는 함수
    /// </summary>
    /// <param name="position">생성위치</param>
    /// <param name="targetPosition">이동할 목적지</param>
    /// <param name="angle">초기 각도(디폴트값을 사용하면 0~360도 사이의 랜덤한 각도)</param>
    /// <returns>큰 운석 하나</returns>
    public EnemyAsteroidBig GetAsteroidBig(Vector3? position, Vector3? targetPosition = null, float? angle = null)
    {
        // direction이 null이면 Vector3.left 값을 사용, null이 아니면 direction이 들어있는 값을 사용
        Vector3 target = targetPosition ?? (position.GetValueOrDefault() + Vector3.left);            // 이동방향 지정
        Vector3 euler = Vector3.zero;
        euler.z = angle ?? Random.Range(0.0f, 360.0f);      // 초기 회전 정도 지정

        
        EnemyAsteroidBig big = enemyAsteroidBig.GetObject(position, euler);
        big.SetDestination(target);

        return big;
    }

    /// <summary>
    /// 작은 운석 하나를 돌려주는 함수
    /// </summary>
    /// <param name="position">생성 위치</param>
    /// <param name="direction">이동할 방향</param>
    /// <param name="angle">초기각도(디폴트값을 사용하면 0~360도 사이의 랜덤한 각도)</param>
    /// <returns></returns>
    public EnemyAsteroidSmall GetAsteroidSmall(Vector3? position, Vector3? direction, float? angle = null)
    {
        Vector3 euler = Vector3.zero;
        euler.z = angle ?? Random.Range(0.0f, 360.0f);      // 초기 회전 정도 지정


        EnemyAsteroidSmall small = enemyAsteroidSmall.GetObject(position, euler);
        small.Direction = direction ?? Vector3.left;        // 이동 방향 지정

        return small;
    }

    public BossBullet GetBossBullet(Vector3? position)
    {
        return bossBullet.GetObject(position);
    }

    /// <summary>
    /// 보스용 미사일 하나를 리턴하는 함수
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public BossMissile GetBossMissile(Vector3? position)
    {
        return bossMissile.GetObject(position);
    }

    /// <summary>
    /// 보스 하나를 리턴하는 함수
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public EnemyBoss GetBoss(Vector3? position)
    {
        return enemyBoss.GetObject(position);
    }
}
