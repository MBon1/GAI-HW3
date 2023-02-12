using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFormationManager : MonoBehaviour
{
    public static NewFormationManager FM;
    public GameObject agentPrefab;
    public int numAgents = 12;
    public List<GameObject> allAgents;
    public Vector3 goalPos = Vector3.zero;

    [Header("Formation Pattern")]
    public Formation formation = Formation.Line;
    public float agentWidth = 1;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = this.transform.position;
        allAgents = new List<GameObject>();
        for (int i = 0; i < numAgents; i++)
        {
            allAgents.Add(Instantiate(agentPrefab, pos, Quaternion.identity));
        }

        FM = this;

        UpdateFormation();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFormation();
    }

    public void RemoveAgent(GameObject agent)
    {
        if (allAgents.Remove(agent))
        {
            Destroy(agent);
        }

    }

    void UpdateFormation()
    {
        SetGoalPos();
        if (formation == Formation.Circle)
        {
            UpdateAgentsScalableCircle();
        }
        if (formation == Formation.Line)
        {
            UpdateAgentsScalableLine();
        }
        if (formation == Formation.Wedge)
        {
            UpdateAgentsScalableWedge();
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


    void UpdateAgentsScalableCircle()
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

            allAgents[i].transform.position = new Vector2(x, y);
            allAgents[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle - 90));

            currentAngle += angleStep;
        }
    }

    void UpdateAgentsScalableLine()
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

            allAgents[i].transform.position = new Vector2(x, y);
            allAgents[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, forwardAngle - 90));

            relativePos -= 2.0f;
        }
    }

    void UpdateAgentsScalableWedge()
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

            allAgents[i].transform.position = new Vector2(x, y);
            allAgents[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, forwardAngle - 90));
            relativePos += 1.0f;

        }
    }
}




public enum Formation
{
    Circle,
    Line,
    Wedge
}
