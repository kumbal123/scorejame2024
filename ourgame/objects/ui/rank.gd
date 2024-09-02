extends Control

class_name Rank

var rank = "1"
var player = "Name"
var score = "10000"

var label_rank
var label_player
var label_score

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	label_rank = get_node("Rank")
	label_player = get_node("Name")
	label_score = get_node("Score")
	
	label_rank.text = rank
	label_player.text = player
	label_score.text = score


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

func setText(new_rank, new_player, new_score):
	label_rank.text = new_rank
	label_player.text = new_player
	label_score.text = new_score
