using UnityEngine;

public class GOAnimatorTrigger : MonoBehaviour
{
    public Animator animator;

    public void OnButtonPressed()
    {
        animator.SetTrigger("Pressed");
    }
}
