Important values in the editor that have an effect on the player's jelly physics:

Jelly Player Prefab:

- Jelly Script:
	* radius: effects intial size of player

	* vertexNum: number of vertices of shape. Additionally, it also effects size of shape (not desired behaviour)

Vertex Prefab:

- Rigidbody2D:
	* constraints: mandatory that rotation on z axis is frozen

- HingeJoint2D is now handled in Jelly script

- CircleCollider2D
	* raidus: determines size of each vertex collider (needs to change depending on player size & vertexNum)

- SpringJoint2D:

	* Distance: determines the max length the player's rigidbody can move away from centre of player.
	  NB because it can effect how the player's movement feels (size dependent)
	
	* Frequency: effects jellyness of player. The higher it is the more rigid the player becomes.


