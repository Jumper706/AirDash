using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float maxCharge = 3; // Time in seconds a dash can be charged.
    public float distancePerCharge = 1; // Relationship between time dashing and distance dashed.
    public int dashUpdate = 5; // Lenght of the dash in Update counts. about 1/60th of a second each. 
    int dashUpdateCount; // Where is the player in the dash.  0 is not dashing.
    Vector3 dashMovement; // The distance the player will attempt to move each frame.
    float chargeTime; // How much the player has already charged.

    // Use this for initialization
    void Start()
    {
        chargeTime = 0;
    }

    public void FixedUpdate()
    {
        HandleDash();
    }

    /// <summary>
    /// Function that handles the dash mechanic.
    /// </summary>
    void HandleDash()
    {
        // Checks if we are already dashing, if so, ignore all user input related to dash.
        if (dashUpdateCount != 0)
        {
            updateDash();
            return;
        }
        // Gets user input.
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        // Player is not holding in a direction. Or has been holding too long.
        if( input.sqrMagnitude == 0 || chargeTime > maxCharge)
        {
            if (chargeTime > 0)
            {
                // Setup and start dash.
                float perUpdateDash = (distancePerCharge * chargeTime) / dashUpdate;
                chargeTime = dashUpdateCount = 0;
                // The last registerd direction is the direction of the dash, we just set the distance.  
                dashMovement = perUpdateDash * dashMovement;
                updateDash();
            }

        }
        // Player is holding in a direction.
        else
        {
            chargeTime += Time.fixedDeltaTime;
            dashMovement = input;
        }

    }

    void updateDash()
    {
        // Move the position of the player.
        transform.position = transform.position + dashMovement;
        // See if we are at the end of the dash.
        dashUpdateCount++;
        if (dashUpdateCount == dashUpdate)
            dashUpdateCount = 0;
    }

}
