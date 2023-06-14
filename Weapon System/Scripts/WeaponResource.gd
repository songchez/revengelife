extends Resource
class_name WeaponData

@export var Projectile:PackedScene
@export var TimeTweenShots:float = 0.2

func GetProjectilePath() -> String:
	return Projectile.resource_path
