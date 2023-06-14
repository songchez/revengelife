extends Node2D
class_name ShootingBehavior

@onready var timer = $Timer
@onready var audio = $Audio
@export var WeaponResource:Resource

signal shotFired

var canShoot:bool = true

func Shoot():
	if not canShoot:
		return
	
	audio.play()
	
	#create instance of projectile
	var projectile = WeaponResource.Projectile.instantiate()
	
	#Set varibles on projectile
	projectile.direction = Vector2.RIGHT.rotated(global_rotation)
	projectile.position = Vector2(1,1)
	#부모노드와 충돌하지 않게
	projectile.add_collision_exception_with(get_parent())
	
	#Add to scene
	self.add_child(projectile)
	
	canShoot = false
	emit_signal("shotFired")
	timer.start(WeaponResource.TimeTweenShots)

func _on_Timer_timeout():
	canShoot = true
