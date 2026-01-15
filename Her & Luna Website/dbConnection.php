<?php
$servername = "localhost:3306";
$username = "root";
$password = "";
$dbname = "ecommerce";

$conn = mysqli_connect($servername, $username, $password, $dbname);
if (!$conn) {
    die("connection failed" . mysqli_connect_error());
}
?>