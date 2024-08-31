extends CanvasLayer

var time = 0.0
var score = 0
var time_passed = 0

func _physics_process(delta):
	time += delta
	time_passed += delta
	
	update_ui()
	update_score()

func update_ui():
	var minutes = int(time) / 60  # Calculate total minutes
	var seconds = int(time) % 60  # Calculate remaining seconds after minutes

	# Format the string as "MM:SS"
	var formatted_time = str(minutes).pad_zeros(2) + ":" + str(seconds).pad_zeros(2)
	
	Global.swtime = formatted_time
	
	$Label.text = formatted_time

func update_score():
	if time_passed >= 1:  # Check if 1 second has passed
		time_passed = 0  # Reset the time passed counter
		
		if time < 60:  # Less than 1 minute
			score += 1
		elif time < 120:  # Between 1 and 2 minutes
			score += 2
		else:  # More than 2 minutes
			score += 3
		
		$ScoreLabel.text = "Score: " + str(score)

func add_score_for_kill():
	score += 5
	$ScoreLabel.text = "Score: " + str(score)
