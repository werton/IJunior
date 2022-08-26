using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private int _minBuckshot = 10;
    [SerializeField] private int _maxBuckshot = 20;

    private int _minSpread = -20;
    private int _maxSpread = 20;
    private float _bulletColumnShiftX = 0.5f;

    public override void OnShot(Transform shootPoint)
    {
        int bulletsNumber = Random.Range(_minBuckshot, _maxBuckshot + 1);

        Vector3 bulletShift = new Vector3(_bulletColumnShiftX, 0, 0);
        bool isShifted = false;

        for (int i = 0; i < bulletsNumber; i++)
        {
            isShifted = !isShifted;
            Vector2 bulletPosition = isShifted == true ? shootPoint.position - bulletShift : shootPoint.position;
            Quaternion bulletRotation = Quaternion.Euler(0, 0, Random.Range(_minSpread, _maxSpread));
            Instantiate(Bullet, bulletPosition, bulletRotation);
        }
    }
}