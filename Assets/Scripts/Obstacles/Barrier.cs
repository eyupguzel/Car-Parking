using System;
using UnityEngine;

public class Barrier : BaseObstacle
{
    public float duration = 1.2f; // 1.2 saniyede bir döngü tamamlanacak
    private float elapsedTime = 0f; // Geçen zamanı tutar

    private void Update()
    {
        ObstacleAnimation();
    }

    public override void ObstacleAnimation() 
    {
        elapsedTime += Time.deltaTime;

        // 0-1 arası değer hesapla (döngüyü tamamlarken zamanın % kaçı geçtiğini belirler)
        float t = Mathf.PingPong(elapsedTime / duration, 1f);

        // 0 ile 90 derece arasında lineer bir geçiş yap
        float zRotation = Mathf.Lerp(0f, 90f, t);

        // Z rotasyonunu ayarla
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.z = zRotation;
        transform.rotation = Quaternion.Euler(currentRotation);
    }
}

