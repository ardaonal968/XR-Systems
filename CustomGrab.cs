using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    public bool double_rotation = false;
    public InputActionReference input;
    
    CustomGrab other_hand;
    List<Transform> touching_objects = new List<Transform>();
    Transform current_object;

    Vector3 prev_pos;
    Quaternion prev_rot;

    void Start()
    {
        if(input != null) input.action.Enable();

        //find other hand script
        CustomGrab[] scripts = transform.parent.GetComponentsInChildren<CustomGrab>();
        
        foreach(CustomGrab s in scripts)
        {
            if (s != this) other_hand = s;
        }
    }

    void Update()
    {
        bool is_pressing = false;
        if (input != null) is_pressing = input.action.IsPressed();

        if (is_pressing)
        {
            //If isnt holding anythin grab
            if (current_object == null)
            {
                // check hand first
                if (touching_objects.Count > 0)
                {
                    current_object = touching_objects[0];
                }
                // check other hand second
                else if (other_hand != null && other_hand.current_object != null)
                {
                    current_object = other_hand.current_object;
                }

                // if just grabbed something, reset
                if (current_object != null)
                {
                    prev_pos = transform.position;
                    prev_rot = transform.rotation;
                }
            }
            // If holding something, do the math
            else
            {
                Vector3 pos_change = transform.position - prev_pos;

                Quaternion rot_change = transform.rotation * Quaternion.Inverse(prev_rot);

                if (double_rotation)
                {
                    float ang;
                    Vector3 ax;

                    rot_change.ToAngleAxis(out ang, out ax);

                    rot_change = Quaternion.AngleAxis(ang * 2.0f, ax);}

                // rotate object
                current_object.rotation = rot_change * current_object.rotation;

                // yorunge
                Vector3 radius = current_object.position - transform.position;

                Vector3 rotated_radius = rot_change * radius;

                Vector3 move_diff = rotated_radius - radius;



                current_object.position += pos_change + move_diff;

                prev_pos = transform.position;

                prev_rot = transform.rotation;
            }}
        else
        {
            current_object = null;}
}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            touching_objects.Add(other.transform);
        }
    }

    void OnTriggerExit(Collider other){

        if (other.CompareTag("Grabbable"))

        {touching_objects.Remove(other.transform);}}
}