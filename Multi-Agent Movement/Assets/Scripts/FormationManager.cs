using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationManager : MonoBehaviour
{
    // Holds the assignment of a single character to a slot
    /*[System.Serializable]
    public struct SlotAssignment
    {
        GameObject character;
        int slotNumber;
    }*/

    [SerializeField] int numSlots = 12;

    // Holds a list of slot assignments
    //[SerializeField] List<SlotAssignment> slotAssignments;
    [SerializeField] List<GameObject> slotAssignments;

    // Hold position and orientation represeting
    // the drift offset for the currently filled slots.
    [SerializeField] Vector2 driftOffsetPosition;
    [SerializeField] float driftOffsetOrientation;

    // Holds the formation pattern
    [SerializeField] FormationPattern pattern;

    // Updates the assignment of characters to slots
    void UpdateSlotAssignments()
    {
        // A very simple assignment algorithm:
        // we simply go through each assignment in
        // the list and assign sequential slot numbers
        /*for (int i = 0; i < slotAssignments.Count; i++)
        {
            slotAssignments[i]
        }*/

        // Update the drift offset
        //pattern.getDriftOffset(slot assignment)
        pattern.GetDriftOffset(slotAssignments, ref driftOffsetPosition, ref driftOffsetOrientation);
    }


    // Add a new character to the first available slot. 
    // Returns false if no more slots are available.



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
