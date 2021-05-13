//Première version du web service utilisée sur le serveur. Modifiée le 13/05/2021

//PHP :

/*
<? php
  header('Content-Type: application/json');
header('Content-Type: text/html; charset=ISO-8859-1');
 

  $functionToCall = $_GET['functionToCall'];

switch ($functionToCall){
    case 'Send':
      AddLevel();
    break;
    case 'Json':
      GetJSON();
    break;
    case 'Traps':
      GetTraps();
    break;
    case 'FindName':
      $string = $_GET['string'];
    FindLevelNameWith($string);
    break;
}


Connection();


function Connection()
{
  $db_host = "fdb30.awardspace.net";
  $db_username = "3720561_leveleditor";
  $db_password = "mNRvCN4w88@Zw}.X";
  $db_name = "3720561_leveleditor";

  $conn = mysqli_connect($db_host, $db_username, $db_password, $db_name);


    if (!$conn) echo 'Connection error :'.mysqli_connect_error();

return $conn;
}

function AddLevel()
{
  $conn = Connection();

  $sql = "INSERT INTO `Level` (`ID_user`, `level_name`, `creation_date`, `level_data`, `max_turns`, `traps`, `nightLevel`) 
  VALUES('666', '$_POST[levelName]', '$_POST[creationDate]', '$_POST[levelDataJson]', '$_POST[maxTurns]', '$_POST[traps]', '$_POST[nightLevel]'); ";
  //
  $conn->query($sql);
    echo $sql;
    echo mysqli_error($conn);

  $conn->close();
}

function GetTraps()
{
  $conn = Connection();

  $sql = "SELECT * FROM Level ORDER BY `Level`.`level_name` ASC";
  $result = $conn->query($sql);

    if ($result->num_rows > 0) {
    $index = 0;
    while ($row = $result->fetch_assoc()) 
      echo $row['traps'];
}
  else
{
    echo "0 results";
}
}

function GetJSON()
{
  $conn = Connection();
  
  $sql = "SELECT * FROM Level ORDER BY `Level`.`level_name` ASC";
  $result = $conn->query($sql);

    if ($result->num_rows > 0) {
    $index = 0;

    while ($row = $result->fetch_assoc()) {
        if ($index != 0) echo "newLevel";

        echo $row['level_data']; 
      $index++;
    }

}
  else
{
    echo "0 results";
}

  $conn->close();
}


function FindLevelNameWith($string)
{

  $conn = Connection();

  $sql = "SELECT * FROM `Level` WHERE level_name LIKE '%$string%';";


 $result = $conn->query($sql);

    if ($result->num_rows > 0) {
    $index = 0;
    while ($row = $result->fetch_assoc()) {
        if ($index != 0) echo "newLevel";
        echo $row['level_data']; 
      $index++;
    }

}
  else
{
    echo "0 results";
}

}
?>
*/