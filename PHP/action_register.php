<?php

include_once 'dbconnect.php';

// Get username and password from Unity Form
$username = $_POST['usernamePost'];
$upass = $_POST['passwordPost'];

// Strip username and password on weird characters
$email = strip_tags($email);
$upass = strip_tags($upass);

// password encrypt using SHA256();
$password = hash('sha256', $upass);

// check username exist or not
$query = "SELECT username FROM users WHERE username='$username'";
$result = mysqli_query($dbconnection ,$query);

// check if email exists or not
$query2 = "SELECT email FROM users WHERE email='$email'";
$result2 = mysqli_query($dbconnection ,$query2);


//Run these querries and check if they get a result
$row = mysqli_fetch_row($result);
$row2 = false;//mysqli_fetch_row($result2);
if ($row or $row2)
{
	//If they get a result the user exists
	$dataArray = array('success' => false, 'error' => 'username or email already exists.');
} else {
	//Run the query to insert it into the database
	$query2 = "INSERT INTO users(username,nickname,password) VALUES('$username','$username','$password')";
	if ($result2 = mysqli_query($dbconnection ,$query2))
	{
		//Inserted into database
	    $dataArray = array('success' => true, 'error' => '', 'nickname' => "$username", 'username' => "$username");
	} else {
		//User already exists
		$dataArray = array('success' => false, 'error' => 'could not create new user. Try again later.');
	}
}


header('Content-Type: application/json');
echo json_encode($dataArray);

?>