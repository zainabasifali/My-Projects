<?php
include('./dbConnection.php');
$name = $_POST['name'];
$productId = $_POST['productid'];
$message = $_POST['message'];
$rating = $_POST['rating'];

$sql = "insert into `reviews` (`name`,`productId`,`message`,`rating`) values ('".$name."','".$productId."','".$message."','".$rating."')";

$result = mysqli_query($conn,$sql);

if($result){
    header('Location: productDescription.php?productId='.$productId.'');
}


?>