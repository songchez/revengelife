extends Node

func GameOver():
	get_tree().reload_current_scene()


func _on_has_died():
	GameOver()
