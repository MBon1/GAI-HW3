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
    bool AddCharacter(GameObject character)
    {
        // Find how many slots we have occupied
        int occupiedSlots = numSlots - slotAssignments.Count;

        // Check if the pattern supports more slots
        // if pattern.SupportsSlots(occupiedSlots+1);
        if (occupiedSlots + 1 < numSlots)
        {
            // Add a new slot assignment
            slotAssignments.Add(character);

            // Update the slot assignments and return success
            UpdateSlotAssignments();
            return true;
        }
        else
        {
            return false;
        }
    }

    // Removes a character from its slot
    void RemoveCharacter(GameObject character)
    {
        // Find Character's slot
        int slot = slotAssignments.IndexOf(character);

        // Made sure we've found a valid result
        if (slot >= 0 && slot < numSlots)
        {
            slotAssignments.RemoveAt(slot);

            // Update the assignments
            UpdateSlotAssignments();
        }
    }

    // Write new slot locations to each character

}
