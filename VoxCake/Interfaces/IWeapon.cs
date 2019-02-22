using UnityEngine;
public interface IWeapon
{
    int Team { get; set; }
    int Damage { get; set; }
    int BlockDamage { get; set; }
    int FireRate { get; set; }
    Vector3[] SprayPattern { get; set; }

    string WeaponModel { get; set; }
    string BulletModel { get; set; }
}