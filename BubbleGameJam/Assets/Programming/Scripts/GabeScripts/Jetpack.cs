using UnityEngine;

public class Jetpack : MonoBehaviour
{
    bool isAvailable = true;
    [SerializeField] Rigidbody playerBody;
    [SerializeField] float currentFuel = 0f;
    [SerializeField] float consumptionSpeed = 1f;
    [SerializeField] float maxFuel = 100f;
    [SerializeField] float jetSpeed = 0f;
    [SerializeField] float forceRate = 1f;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float maxYVelocity = 10f;
    void Start()
    {
        if(playerBody == null)
        {
            playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        }
    }


    void Update()
    {
        //if you activate the jetpack, it will build up speed until it reaches max.  If you don't press it, the jetpack will loose speed.
        if(Input.GetKey(KeyCode.LeftShift))
        {
            if (currentFuel >= 0)
            {
                jetSpeed += forceRate * Time.deltaTime;
                currentFuel -= consumptionSpeed * Time.deltaTime;
                if (jetSpeed > maxSpeed)
                    jetSpeed = maxSpeed;
            }
        }
        else
        {
            if(jetSpeed > 0)
            {
                jetSpeed -= forceRate * Time.deltaTime;
            }            
        }

        //if the speed value is positive, the jetpack will move the player upwards.  If its zero or netagive, it will stop adding force and allow the player to fall with their own gravity.
        //if(jetSpeed > 0)
        //{
            Vector3 rBodySpeed = Vector3.zero;
            rBodySpeed.y = jetSpeed;
            playerBody.AddForce(rBodySpeed,ForceMode.Impulse);

        if (playerBody.linearVelocity.y > maxYVelocity)
        {
            Vector3 temp = playerBody.linearVelocity;
            temp.y = maxYVelocity;
            playerBody.linearVelocity = temp;
            //   }
        }
        
        if(jetSpeed < 0)
        {
            jetSpeed = 0;
        }

    }

    public void AddFuel(float fuelValue)
    {
        currentFuel += fuelValue;
        if (currentFuel > maxFuel)
            currentFuel = maxFuel;
    }
}
