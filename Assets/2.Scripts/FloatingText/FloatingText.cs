using System;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TextMesh targetTextMesh;
    [SerializeField] private Transform movingContainer;
    [SerializeField] private bool faceCamera;
    [SerializeField] private float lifeTime;
    
    [SerializeField] private float remapYZero;
    [SerializeField] private float remapYOne;
    
    [SerializeField] private AnimationCurve animateYCurve;

    private FloatingTextSpawner spawner;
    
    private float startedAt;
    private float elapsedTime;
    private float remappedTime;

    private void OnEnable()
    {
        spawner = GameManager.Instance.TextSpawner;
        startedAt = Time.time;
        targetTextMesh.GetComponent<MeshRenderer>().sortingOrder = 10;
    }

    private void Update()
    {
        if (!isActiveAndEnabled)
            return;
            
        elapsedTime = Time.time - startedAt;
        remappedTime = Remap(elapsedTime, 0f, lifeTime, 0f, 1f);
        
        HandleMovement();
        FaceCamera();

        if (elapsedTime >= lifeTime)
        {
            Release();
        }
    }
    
    public void SetText(string newValue)
    {
        targetTextMesh.text = newValue;
    }

    private void HandleMovement()
    {
        transform.up = Vector2.up;
        
        var newPosition = transform.localPosition;
        newPosition.y += Remap(animateYCurve.Evaluate(remappedTime), 0f, 1, remapYZero, remapYOne);
            
        transform.localPosition = newPosition;
    }
    
    private void FaceCamera()
    {
        if (faceCamera)
        {
            if (Camera.main != null)
            {
                var targetCameraRotation = Camera.main.transform.rotation;
                movingContainer.transform.LookAt(movingContainer.transform.position + targetCameraRotation 
                    * Vector3.forward, targetCameraRotation * movingContainer.up);
            }
        }
    }

    public void Release()
    {
        spawner.RetrieveFloatingText(this);
    }
    
    public static float Remap(float x, float A, float B, float C, float D)
    {
        float remappedValue = C + (x-A)/(B-A) * (D - C);
        return remappedValue;
    }
}
