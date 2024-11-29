using UnityEngine;

public class Trail : MonoBehaviour
{
    private Vector3 previousPos;
    private float totalDistance = 0f;
    TrailRenderer trailRenderer;
    Color trailColor;

    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.time = Mathf.Infinity;

        trailRenderer.material.color = Color.blue; //Change this to trailColor
        previousPos = transform.position;
    }

    void Update()
    {
        
        float distance = Vector3.Distance(transform.position, previousPos);
        
        if (distance > 0f) 
        {
            totalDistance += distance;
        }
        
        
        previousPos = transform.position;

        
        Debug.Log("Total Distance: " + totalDistance); //You can remove this line
    }
    public float getTotalDistance()
    {
        return totalDistance;
    }
    public void setColor(Color color)
    {
        trailColor = color;
    }
}
