<?php
error_reporting(1);
include_once 'dbconnect.php';

//Posts from Unity Project with details
$session_id = $_POST['session_id'];
$nickname = $_POST['nickname'];


//Start session id we got from the unity project
session_id($session_id);
session_start();


if(isset($_SESSION['username']))
{

	//If the session exists. Update the user it's nickname with the new nickname we got from the POST
	$query = "
	UPDATE users SET nickname='$nickname' WHERE ID=" . $_SESSION['ID'] . "";
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
