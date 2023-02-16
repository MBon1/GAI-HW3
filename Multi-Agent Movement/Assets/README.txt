================================================================

+------------+
| HOMEWORK 3 |
+------------+

================================================================

GROUP MEMBERS

  Matt Bonnecaze    (bonnem3)
  Justin Hung       (hungj2)
  Philip Stapleton  (staplp2)

================================================================

LINKS

WebGL Build

  https://bonnem.itch.io/multiagent-matthewbonnecaze-phillipstapleton-justinhung

GitHub Repository

  https://github.com/MBon1/GAI-HW3

================================================================

NOTES

  Our project is split into two separate scenes: one for scalable formations and another for two-level formations.

================================================================

OBSTACLE AVOIDANCE

  Our formations uses ray casting to avoid obstacles (specifically, one ray shooting directly outward and two at a slight leftward and rightward angle). For a scalable formation, only the leader performs raycasting, whereas for a two level formation, every agent casts rays to avoid obstacles individually.

  Defensive circle formations and wedge formations are used for the open areas. Line formations are used for squeezing through the tunnels, with the single file line being used for the narrow tunnel in the beginning half of the course and the three-wide line being used for the much wider tunnel at the end of the course.

================================================================

TUNNEL DETECTION AND FORMATION SWITCHING

  As was approved by Professor Si in class, our project uses triggers to detect tunnels and respond accordingly. As the group (or formation manager) approaches or exits a tunnel, it enters a trigger zone. These trigger zones then set the formation manager to one of four formations: defensive circle, line, wedge, and three-wide line.

  Defensive circle formations are built by taking the number of total agents and transposing them at a consistent distance away from an origin. The angle of the offset is determined as ((agent_index / total_agents) * 360) + 90, with the leader agent being at 12 o'clock by default. The direction and orientation of the defensive circle changes based on where the formation is headed.

  Line formations are built by taking each agent and transposing it away from an origin by a distance of (agent_index * spacing) + initial_spacing. As with defensive circle formations, the direction and orientation of the agents is determined based on where the formation is headed. For two-level formations, the agents' orientation is explicitly determined and is decided by the Arrive() behavior.

  Wedge and three-wide line formations are built like line formations: wedges are built with distance offset being the same for every two agents, and with even indexed agents being offset at a -60.0f degree angle, and odd indexed agents being offset at a +60.0f degree angle. Conversely, three-wide line formations are built like line formations where every three agents has the same offset backward, the first of every three is offset leftward, and the third of every three is offset rightward.

  With scalable formations, removing an agent from the list will cause all agents to restructure intelligently. Defensive circles will respace to be evenly distanced, lines will shuffle into an evenly spaced single file column, and wedges will reorganize the two wings to be evenly balanced.

================================================================

ADDITIONAL HEURISTICS

  No additional heuristics beyond those mentioned in class were used.

================================================================

DIFFERENCES BETWEEN FORMATION TYPES

Scalable

  Scalable formations rely on a leader agent to determine all movement. Individual agents do not move independently from one another; a leader agent will cast rays to determine where obstacles are, all agents will follow one specific direction, and all agents' positions are determined with respect to one "relative" position.

  With scalable formations, however, some of the agents have a tendency to clip into obstacles due to them not being able to independently evade on their own. This is particularly apparent when circle formations approach corners or when the outer ends of line and wedge formations try to navigate very small obstacles like the one in the center of the arena.

Two-Level

  Two level formations are similar but generally offer more freedom with agents following an invisible leader and otherwise acting independently. Each agent casts its own rays and has the ability to divert course come back into the formation, which fixes the obstacle avoidance issues that scalable formations present. For example, the group will now split and navigate around the aforementioned obstacle in the center of the arena.

================================================================

RELEVANT FUNCTIONS

NewFormationManager.UpdateAgentCircle();
NewFormationManager.UpdateAgentLine();
NewFormationManager.UpdateAgentWedge();
NewFormationManager.UpdateAgentThreeWideLine();

================================================================
