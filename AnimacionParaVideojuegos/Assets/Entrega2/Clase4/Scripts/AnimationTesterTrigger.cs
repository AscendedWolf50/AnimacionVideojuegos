using UnityEngine;

public class AnimationTesterTrigger : MonoBehaviour
{
    public Animator Animator;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            Animator.SetTrigger(name:"Fire");
    }
}
