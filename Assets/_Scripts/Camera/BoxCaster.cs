using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCaster : MonoBehaviour
{
    List<ChangeVisibility> visibilities = new List<ChangeVisibility>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ChangeVisibility>())
        {
            visibilities.Add(other.GetComponent<ChangeVisibility>());
            other.GetComponent<ChangeVisibility>().FadeAway();
            //if (Vector3.Distance(other.transform.position, transform.position) < (Vector3.Distance(player.position, transform.position)))
            //{
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (visibilities.Contains(other.GetComponent<ChangeVisibility>()))
        {
            other.GetComponent<ChangeVisibility>().MakeVisible();
            visibilities.Remove(other.GetComponent<ChangeVisibility>());
        }
    }
}
