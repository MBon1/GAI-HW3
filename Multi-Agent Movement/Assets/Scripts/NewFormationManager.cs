using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFormationManager : MonoBehaviour
{
    public static NewFormationManager FM;
    public GameObject agentPrefab;
    public int numAgents = 12;
    public List<GameObject> allAgents = new List<GameObject>();
    public Vector3 goalPos = Vector3.zero;

    [Header("Target Positions")]
    public GameObject agentTargetPrefab;
    public List<GameObject> agentTargetPositions = new List<GameObject>();

    [Header("Formation Pattern")]
    public Formation formation = Formation.Line;
    Formation lastFormation = Formation.None;
    public float agentWidth = 1;
    public float agentAttatchmentDistance = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = this.transform.position;
        for (int i = 0; i < numAgents; i++)
        {
            allAgents.Add(Instantiate(agentPrefab, pos, Quaternion.identity));
            allAgents[i].name = "Agent " + i;
            agentTargetPositions.Add(Instantiate(agentTargetPrefab, pos, Quaternion.identity));
            agentTargetPositions[i].name = "Target " + i;

            Movement_3 movement = allAgents[i].GetComponent<Movement_3>();
            movement.targets.Add(agentTargetPositions[i]);
        }

        FM = this;

        UpdateFormation(true);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFormation();
    }

    public void RemoveAgent(GameObject agent)
    {
        int index = allAgents.IndexOf(agent);
        if (allAgents.Remove(agent))
        {
            Destroy(agent);
            Destroy(agentTargetPositions[index]);
        }
    }

    void UpdateFormation(bool ignoreDetatchment = false)
    {
        SetGoalPos();
        if (formation == Formation.Circle)
        {
            UpdateAgentsScalableCircle(ignoreDetatchment);
        }
        if (formation == Formation.Line)
        {
            UpdateAgentsScalableLine(ignoreDetatchment);
        }
        if (formation == Formation.Wedge)
        {
            UpdateAgentsScalableWedge(ignoreDetatchment);
        }

        if (formation != lastFormation)
        {
            lastFormation = formation;
        }
    }

    void SetGoalPos()
    {
        SetGoalPos(this.transform.position);
    }

    void SetGoalPos(Vector3 pos)
    {
        goalPos = pos;
    }


    void UpdateAgentsScalableCircle(bool ignoreDetatchment = false)
    {
        if (allAgents.Count == 1)
        {
            allAgents[0].transform.position = this.transform.position;
            allAgents[0].transform.rotation = this.transform.rotation;
            return;
        }

        float numSlots = allAgents.Count;
        float radius = agentWidth / Mathf.Sin(Mathf.PI / numSlots);
        float angleStep = 360.0f / allAgents.Count;
        float currentAngle = 90.0f + this.transform.rotation.eulerAngles.z;

        Vector3 pos = transform.position;
        
        for (int i = 0; i < allAgents.Count; i++)
        {
            float x = pos.x + radius * Mathf.Cos(Mathf.Deg2Rad * currentAngle);
            float y = pos.y + radius * Mathf.Sin(Mathf.Deg2Rad * currentAngle);
            Vector2 newPos = new Vector2(x, y);

            agentTargetPositions[i].transform.position = newPos;

            // Check distance between agent's position and its taret's position
            Movement_3 movement = allAgents[i].GetComponent<Movement_3>();
            bool notDetatched = !movement.Detatch(agentAttatchmentDistance);
            if (ignoreDetatchment || notDetatched)
            {
                allAgents[i].transform.position = newPos;
                allAgents[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle - 90));
            }

            currentAngle += angleStep;
        }
    }

    void UpdateAgentsScalableLine(bool ignoreDetatchment = false)
    {
        if (allAgents.Count == 0)
        {
            return;
        }

        float spacing = 2.0f;
        float relativePos = ((float)(allAgents.Count - 1) * spacing) / 2.0f;
        float forwardAngle = this.transform.rotation.eulerAngles.z + 90.0f;

        Vector3 pos = transform.position;

        for (int i = 0; i < allAgents.Count; i++)
        {
            float x = pos.x + (Mathf.Cos(Mathf.Deg2Rad * forwardAngle) * relativePos);
            float y = pos.y + (Mathf.Sin(Mathf.Deg2Rad * forwardAngle) * relativePos);
            Vector2 newPos = new Vector2(x, y);

            agentTargetPositions[i].transform.position = newPos;

            // Check distance between agent's position and its taret's position
            Movement_3 movement = allAgents[i].GetComponent<Movement_3>();
            bool notDetatched = !movement.Detatch(agentAttatchmentDistance);
            if (ignoreDetatchment || notDetatched)
            {
                allAgents[i].transform.position = newPos;
                allAgents[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, forwardAngle - 90));
            }

            relativePos -= 2.0f;
        }
    }

    void UpdateAgentsScalableWedge(bool ignoreDetatchment = false)
    {
        if (allAgents.Count == 0)
        {
            return;
        }

        float spacing = 1.0f;
        float relativePos = -((float)(allAgents.Count - 1) * spacing);
        float forwardAngle = this.transform.rotation.eulerAngles.z + 90.0f;

        Vector3 pos = transform.position;

        for (int i = 0; i < allAgents.Count; i++)
        {
            float x;
            float y;
            /*if (i == 0)
            {
                x = pos.x;
                y = pos.y;
            }*/
            if (i % 2 == 0)
            {
                x = pos.x - (Mathf.Sin(Mathf.Deg2Rad * forwardAngle) * relativePos);
                y = pos.y + (Mathf.Sin(Mathf.Deg2Rad * forwardAngle) * relativePos);
            }
            else
            {
                x = pos.x + (Mathf.Sin(Mathf.Deg2Rad * forwardAngle) * relativePos);
                y = pos.y + (Mathf.Sin(Mathf.Deg2Rad * forwardAngle) * relativePos) - 1.0f;

            }

            Vector2 newPos = new Vector2(x, y);

            agentTargetPositions[i].transform.position = newPos;

            // Check distance between agent's position and its taret's position
            Movement_3 movement = allAgents[i].GetComponent<Movement_3>();
            bool notDetatched = !movement.Detatch(agentAttatchmentDistance);
            if (ignoreDetatchment || notDetatched)
            {
                allAgents[i].transform.position = newPos;
            allAgents[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, forwardAngle - 90));
            }

            relativePos += 1.0f;
        }
    }
}




public enum Formation
{
    None, 
    Circle,
    Line,
    Wedge
}
