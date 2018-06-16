<?php require_once("config.php");
error_reporting(1);

function GetScoreAbsolute()
{
	$connectionString = "mysql:dbname=".DATABASE_NAME.";host=localhost";
	$conn = new PDO($connectionString, DATABASE_USERNAME, DATABASE_PASSWORD);
	// Connect and show score all highscores
	$sql = "SELECT DISTINCT SCORE.score as score, SCORE.date as date, 
			USERS.username as username, GAME.Gamename as game 
			FROM score SCORE 
            LEFT JOIN users USERS ON SCORE.user_ID = USERS.ID 
			LEFT JOIN game GAME ON SCORE.game = GAME.ID WHERE GAME.ID = 1
            ORDER BY SCORE.score DESC";
	
	$sth = $conn->prepare($sql);
	$sth->execute();
	$result = $sth->fetchAll();
	return $result;
};

function GetScoreDay()
{
	$connectionString = "mysql:dbname=".DATABASE_NAME.";host=localhost";
	$conn = new PDO($connectionString, DATABASE_USERNAME, DATABASE_PASSWORD);
		// Connect and show score of the day
	$sql = "SELECT DISTINCT SCORE.score as score, SCORE.date as date, 
			USERS.username as username, GAME.Gamename as game 
            FROM score SCORE 
            LEFT JOIN users USERS on SCORE.user_ID = USERS.ID 
			LEFT JOIN game GAME on SCORE.game = GAME.ID
            WHERE GAME.ID = 1 AND DATE(date)=CURDATE()
            ORDER BY SCORE.score DESC ";
	
	$sth = $conn->prepare($sql);
	$sth->execute();
	$result = $sth->fetchAll();
	return $result;
};

function GetScoreMonth()
{
	$connectionString = "mysql:dbname=".DATABASE_NAME.";host=localhost";
	$conn = new PDO($connectionString, DATABASE_USERNAME, DATABASE_PASSWORD);
	//Connect and show score off the month
	$sql = "SELECT DISTINCT SCORE.score AS score, SCORE.date AS date, USERS.username AS username, GAME.Gamename AS game 
            FROM score SCORE 
            LEFT JOIN users USERS on SCORE.user_ID = USERS.ID 
			LEFT JOIN game GAME on SCORE.game = GAME.ID
            WHERE GAME.ID = 1 AND SCORE.date >= DATE_SUB(CURDATE(), INTERVAL DAYOFMONTH(CURDATE())-1 DAY)
            ORDER BY SCORE.score DESC ";
	
	$sth = $conn->prepare($sql);
	$sth->execute();
	$result = $sth->fetchAll();
	return $result;
};
?>