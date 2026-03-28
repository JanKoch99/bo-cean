using UnityEngine;

public class SinkingObject : FloatingObject
{

    public float sinkOffset = 0f;

    protected override void Update()
    {
        base.Update();
        
        float waterLevel = WaterManager.Instance.waterHeight;

        Vector3 pos = transform.position;

        pos.y = -waterLevel + sinkOffset;

        transform.position = pos;
    }
}
