extends PhysicsBody2D

const MOVE_SPEED = 200
@onready var raycast = $RayCast2D
@export var health = 100
@export var playerNodePath = NodePath()

var player = null

func _ready():
	player = get_node(playerNodePath)

func _physics_process(delta):
	if player == null:
		return
	
	var vec_to_player = player.global_position - global_position
	vec_to_player = vec_to_player.normalized()
	global_rotation = atan2(vec_to_player.y, vec_to_player.x)
	move_and_collide(vec_to_player * MOVE_SPEED * delta)
	
	if raycast.is_colliding():
		var coll = raycast.get_collider()
		if coll.name == "Player":
			coll.takeDamage(0.2)

func takeDamage(amount:float):
	$healthbar.value = health
	$healthbar.visible = true
	health -= amount
	if(health <= 0):
		kill()

func kill():
	queue_free()

