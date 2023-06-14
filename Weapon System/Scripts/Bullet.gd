extends PhysicsBody2D

var direction := Vector2.RIGHT
@export var speed:float = 400
@export var damage:float = 10

func _ready():
	top_level
	direction = direction.normalized()
	look_at(direction + global_position)

func _physics_process(delta):
	var vel = direction * speed * delta
	var col = move_and_collide(vel, false)
	
	if(col and col.collider):
		if(col.collider.has_method("takeDamage")):
			col.collider.takeDamage(damage)
		
		if(col.collider.has_method("apply_impulse")):
			col.collider.apply_impulse(col.collider.global_position, direction)
		
		queue_free()
