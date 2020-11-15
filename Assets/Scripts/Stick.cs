using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour {

    private Animator animator;
    void Start() {
        animator = GetComponent<Animator>();
    }





    public void PerformAttack() {
        Debug.Log(this.name + " attack");

        animator.SetTrigger("Base_Attack");





        //attack timer: if the player has attacked in the last .5 seconds don't allow them to attack again
        /*if (Input.GetMouseButtonDown(0) && timer >= 1) {
            attacking = true;
            hasAttacked = true;
            //lastDirection = -1 * lastDirection;
            timer = 0;
        }
        else if (timer >= .5) {
            attacking = false;
        }//rotate the weapon 90 degrees (180 degrees per second over half a second) 
        if (attacking && timer <= .5) {
            weapon.transform.RotateAround(gameObject.transform.position, Vector3.down,
                    180 * Time.deltaTime);
            weapon.transform.RotateAround(gameObject.transform.position, Vector3.right,
                    180 * Time.deltaTime);
            attacking = true;

        }
        if (timer >= .5 && timer <= 1 && hasAttacked == true) {
            weapon.transform.RotateAround(gameObject.transform.position, Vector3.up,
                    180 * Time.deltaTime);
            weapon.transform.RotateAround(gameObject.transform.position, Vector3.left,
                    180 * Time.deltaTime);
        }
        if (timer >= 1) {
            hasAttacked = false;
        }
        timer += Time.deltaTime;*/
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Enemy") {
            other.GetComponent<EnemyController>().TakeDamage();
        }
    }

}
