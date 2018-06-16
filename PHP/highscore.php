<?php
error_reporting(1);
include_once 'dbconnect.php';
require_once("config.php");
require_once("functions.php");

$users = GetScoreAbsolute();
$usersday = GetScoreDay();
$usersmonth = GetScoreMonth();


//How many times the game has been played this week. 
$query =
            "SELECT COUNT(s.score) FROM score s LEFT JOIN game g on s.game = g.ID
            WHERE g.id AND s.date >= DATE_SUB(CURDATE(), INTERVAL DAYOFWEEK(CURDATE())-1 DAY)";
			
$result = mysqli_query($dbconnection, $query);

$row = mysqli_fetch_row($result);

//Show row with data how many times it has been played this week
if ($row)
{
	echo "Het spel is deze week zoveel keer gespeeld: " .$row[0]. " keer";
	
} else
{
	echo "Iets gaat helemaal fout";
}
?>

<P>Absolute Score</p>
<table class="rwd-table">
  <tr>
    <th>Player</th>
    <th>Score</th>
  </tr>
  <tr>
				<?php foreach ($users as $user){
				echo "<tr><td data-th='Player'>"; echo $user["username"]; echo "</td><td data-th='Wellbeing'>"; echo $user["score"]; echo "</td></tr>"; 
				};?>
				  </tr>
</table>
<P> Daily Score</p>
<table class="rwd-table">
  <tr>
    <th>Player</th>
    <th>Score</th>
  </tr>
  <tr>
				<?php foreach ($usersday as $user){
				echo "<tr><td data-th='Player'>"; echo $user["username"]; echo "</td><td data-th='Wellbeing'>"; echo $user["score"]; echo "</td></tr>"; 
				};?>
				  </tr>
</table>

<P> Month Score</p>
<table class="rwd-table">
  <tr>
    <th>Player</th>
    <th>Score</th>
  </tr>
  <tr>
				<?php foreach ($usersmonth as $user){
				echo "<tr><td data-th='Player'>"; echo $user["username"]; echo "</td><td data-th='Wellbeing'>"; echo $user["score"]; echo "</td></tr>"; 
				};?>
				  </tr>
</table>

