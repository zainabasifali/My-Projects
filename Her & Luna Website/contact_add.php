<?php
include('./dbConnection.php');
$name = $_POST['name'];
$review = $_POST['review'];
$email = $_POST['email'];
$message = $_POST['message'];


$sql = "insert into `contact` (`name`,`email`,`message`,`review`) values ('".$name."','".$email."','".$message."','".$review."')";

$result = mysqli_query($conn,$sql);

if($result){
    header('Location: index.php');
}


?>