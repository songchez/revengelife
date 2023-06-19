extends CharacterBody2D
class_name Player

@export var speed = 300
@export var health = 100
@export var HPbar = Node.new()

@onready var shootingBehavior := $ShootingBehavior

var isDead:bool = false
var inputVelocity:Vector2 = Vector2.ZERO
var Velocity:Vector2 = Vector2.ZERO
var rotation_direction = 0

signal tookDamage(amount)
signal hasDied

func get_input():
	var input_direction = Input.get_vector("left", "right", "up", "down")
	velocity = input_direction * speed

func _input(event):
	if event is InputEventMouseButton and event.button_index == MOUSE_BUTTON_LEFT and event.pressed and shootingBehavior:
		shootingBehavior.Shoot()

func _physics_process(delta):
	get_input()
	move_and_slide()

func enable(b:bool):
	set_process(b)
	set_process_input(b)
	set_physics_process(b)

func takeDamage(amount:float):
	health -= amount
	if health <= 0:
		kill()
	emit_signal("tookDamage", amount)
	HPbar.value = health

func kill():
	if !isDead:
		health = 0
		enable(false)
		emit_signal("hasDied")
