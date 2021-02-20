using UnityEngine;

public class MoveTransition : PlayerTransition
{
    public override void Enable()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NeedTransit = true;
        }
    }
}
