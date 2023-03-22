using UnityEngine;

public class DiceAnimation : MonoBehaviour
{
    private Animator animator;
    private Dice dicRoll;


    public bool CanPlay { get; set; }
    // private bool isVisible = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("DiceRollAlone");
        dicRoll = GameObject.FindGameObjectWithTag("DiceRoll").GetComponent<Dice>();
        CanPlay = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<Renderer>().enabled = CanPlay;
            CanPlay = false;
        }
    }

}
