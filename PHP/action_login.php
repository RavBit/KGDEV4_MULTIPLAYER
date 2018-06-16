<?php
error_reporting(1);
include_once 'dbconnect.php';

//Posts from Unity Project with username and password
$username = $_POST['usernamePost'];
$upass = $_POST['passwordPost'];

// Strip username and password on weird characters
$email = strip_tags($email);
$upass = strip_tags($upass);
// password encrypt using SHA256();
$password = hash('sha256', $upass);


// Select username and password to check if they match with a row in the database
$query = "SELECT username, ID, nickname FROM users WHERE username='$username' AND password='$password'";
$result = mysqli_query($dbconnection, $query);

$row = mysqli_fetch_row($result);


if ($row)
{	
	// If there is a row it will init a session and return data to parse towards json (inside the Unity Project)
	session_start(); 
	$session_id = session_id();
	$dataArray = array('success' => true, 'error' => '', 'username' => "$username", 'ID' => $row[1], 'nickname' => $row[2], 'session' => $session_id);

	$_SESSION['ID'] = $row[1];
	$_SESSION['username'] = $username;
	$_SESSION['nickname'] = $row[2];
	
} else
{	
	//Return to the Unity Project the email or password are incorrect
	$dataArray = array('success' => false, 'error' => 'Invalid email or password.');
}


//Encode it to json and parse it back
header('Content-Type: application/json');

echo json_encode($dataArray);

?>
