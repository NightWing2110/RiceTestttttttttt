using System.Collections;
using UnityEngine;


public class Dice : MonoBehaviour
{
    private Rigidbody rb;
    bool hasLanded;
    bool thrown;
    Vector3 initPosition;
    private Quaternion initRotation;
    public int diceValue;
    public DiceSide[] diceSides;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.gameObject.GetComponent<Renderer>().enabled = false;
        // Debug.Log(rb.gameObject);
        initPosition = transform.position;
        rb.useGravity = false;
        rb.maxAngularVelocity = 20;
        Quaternion initRotation = Quaternion.Euler(Random.Range(0, 6) * 90, 45, Random.Range(0, 6) * 90);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.gameObject.GetComponent<Renderer>().enabled = true;
            RollDice();
        }
        if (rb.IsSleeping() && !hasLanded && thrown)
        {
            hasLanded = true;
            rb.useGravity = false;
            rb.isKinematic = true;
            print(GetSideValue());
            GameObject.FindWithTag("DiceRoll").gameObject.GetComponentInChildren<DiceAnimation>().CanPlay = true;
        }
        else if (rb.IsSleeping() && hasLanded && diceValue == 0)
        {
            // Debug.Log("-------22222----");
            RollAgain();
        }
    }

    private void Roll()
    {
        thrown = true;
        rb.useGravity = true;
        // Tạo một hướng cho lực tác động
        Vector3 direction = new Vector3(300, 400, -300).normalized;
        // Tạo một mức độ cho mạnh mẽ của lực tác động
        float force = 500f;
        // Tác động 1 lực lên xúc xắc
        rb.AddForce(direction * force);
        // Tạo một vị trí cho tác động tại 1 điểm trên bề mặt xúc xắc
        Vector3 position = transform.position;
        position.y += position.y;
        // Tác động 1 lực tại vị trí trên bề mặt xúc xắc
        rb.AddForceAtPosition(direction * (force / 4f), position);
        GameObject.FindWithTag("DiceRoll").gameObject.GetComponentInChildren<DiceAnimation>().CanPlay = false;
    }

    private void RollDice()
    {
        if (!thrown && !hasLanded)
        {
            Roll();
        }
        else if (thrown && hasLanded)
        {
            ResetDice();
        }
    }

    private void ResetDice()
    {
        transform.position = initPosition;
        Quaternion initRotation = Quaternion.Euler(Random.Range(0, 6) * 90, (Random.Range(0, 6) * 90) + 45, Random.Range(0, 6) * 90);
        Vector3 eulerAngles = transform.localEulerAngles;
        eulerAngles.x = Random.Range(0, 6) * 90;
        eulerAngles.y = (Random.Range(0, 6) * 90) + 45;
        eulerAngles.z = Random.Range(0, 6) * 90;
        transform.localEulerAngles = eulerAngles;

        thrown = false;
        hasLanded = false;
        rb.useGravity = false;
        rb.isKinematic = false;

        rb.gameObject.GetComponent<Renderer>().enabled = false;
    }

    private void RollAgain()
    {
        ResetDice();
        Roll();
    }
    private int GetSideValue()
    {
        diceValue = 0;
        foreach (DiceSide side in diceSides)
        {
            if (side.OnGround())
            {
                return diceValue = side.sideValue;
            }
        }
        return diceValue;
    }
}
