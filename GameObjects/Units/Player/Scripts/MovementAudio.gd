extends Node2D

@onready var audio:AudioStreamPlayer2D = $Audio
@onready var timer:Timer = $Timer
@export var timeTweenSteps:float = 0.2
@export var controlNode:Node2D = null

func _process(delta):
	#Footstep Audio
	if(controlNode.Velocity != Vector2.ZERO):
		if(timer.time_left <= 0):
			audio.pitch_scale = randf_range(1, 1.1)
			audio.play()
			timer.start(timeTweenSteps)
