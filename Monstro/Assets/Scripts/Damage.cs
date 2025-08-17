using UnityEngine;
using static GameSettings;

public class Damage : MonoBehaviour
{
    public int damageAmount = 1;
    public enum MethodOfDamage { Touch, Interact };
    public MethodOfDamage damageMethod;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("Antes if");
        print(other);
        if (other.gameObject == player && damageMethod == MethodOfDamage.Touch)
        {
            print("Barreira morte");
            player.SendMessage("TakeDamage", damageAmount);
        }
    }



}
