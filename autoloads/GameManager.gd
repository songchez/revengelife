extends Node

@export var gameover = Node.new()
@export var restartbutton = Node.new()

func _ready():
	restartbutton.pressed.connect(self.reStart)
	
func GameOver():
	gameover.visible = true

func reStart():
	gameover.visible = false
	get_tree().reload_current_scene()

func _on_player_has_died():
	GameOver()
