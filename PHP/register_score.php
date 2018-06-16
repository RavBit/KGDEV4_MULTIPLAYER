<?php
error_reporting(1);
include_once 'dbconnect.php';

//Check for session and get score from Unity
$session_id = $_POST['session_id'];
$score = $_POST['score'];
$game_id = 1;

session_id($session_id);
session_start();


if(isset($_SESSION['username']))
{

	//If session exists set a new score for the user. 
	$query = "
	INSERT INTO score(user_ID, game, score) VALUES('" . $_SESSION['ID'] . "',$game_id,'$score')";
	
	//Insert the score
	$result = mysqli_query($dbconnection ,$query);
	if ($result)
	{
		$dataArray = array('success' => true, 'error' => '' );
	} else 
	{
		$dataArray = array('success' => false, 'error' => 'could not save score' );
	}

header('Content-Type: application/json');
echo json_encode($dataArray);
}
else
{
	echo $session_id;
}


?>
