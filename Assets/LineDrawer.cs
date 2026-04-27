using System.Collections;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private float drawDuration = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 0;
        }
    }

    public void RestartLine()
    {
        lineRenderer.positionCount = 0;
    }

    public IEnumerator AnimateLine(Vector3 start, Vector3 end)
    {
        lineRenderer.positionCount=2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, start); 
        
        float elapsedTime = 0f;

        while (elapsedTime < drawDuration)
        {
            elapsedTime += Time.deltaTime;
            float deltaMovement = elapsedTime / drawDuration;
            
            Vector3 currentPosition = Vector3.Lerp(start, end, deltaMovement);
            lineRenderer.SetPosition(1, currentPosition);
            
            yield return null;
        }
        
        lineRenderer.SetPosition(1, end);
    }
    
}
