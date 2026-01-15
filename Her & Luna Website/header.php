<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Her & Luna</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css" rel="stylesheet">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="style.css">
    </head>
    
<body>
<?php
session_start();
if (!isset($_SESSION['cartCount'])) {
    $_SESSION['cartCount'] = 0;
}

require("dbConnection.php");
$sql="Select * from categories";
$result = mysqli_query($conn,$sql);

?>

<marquee behavior="scroll" direction="left" style="background-color:#FFE4F0;" class="p-3">
  New Year Sale is Live!!
</marquee>
<nav style="background-color:white;border:#FFE4F0" class="navbar navbar-expand-lg">
  <div class="container-fluid">
    <a class="navbar-brand fw-bold " href="index.php">Her & Luna</a>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarNavDropdown">
      <ul class="navbar-nav gap-4">

      <li class="nav-item ">
          <a class="nav-link hidden " href="index.php"></a>
        </li>

        <li class="nav-item">
          <a class="nav-link" href="product.php?">All Products</a>
        </li>

      <li class="nav-item dropdown">
          <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
            Shop by Product
          </a>
          <ul class="dropdown-menu">
            <?php
            if($result){
                while($row = mysqli_fetch_assoc($result)){
                    ?>
                    <li><a class="dropdown-item" href="product.php?categoryId=<?php echo trim($row['id']) ?>&name=<?php echo trim($row['name'])?>">
                        <?php echo $row['name'] ?>
                    </a></li>
                    <?php
                }
            }
            ?>
          </ul>
        </li>
        <li class="nav-item ">
          <a class="nav-link " href="contact.php">Contact Us</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="about.php">About Us</a>
        </li>
      </ul>
      <ul class="navbar-nav ms-auto">
      <li class="nav-item me-3">
       <a type="button" class="btn position-relative" href="./addToCart.php">
       <i class="fa-solid fa-cart-shopping fs-3"></i>
          <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">
            <?php echo $_SESSION['cartCount'];?>
          </span>
          </a>
       </li>
       
        <?php
        if(isset($_SESSION['username'])){
          ?>
          <li class="nav-item">
          <a class="nav-link fw-bolder" href="logout.php">Logout</a>
        </li>
        <?php
        }else{
          ?>
              <li class="nav-item">
              <a class="nav-link" href="login.php"><i class="fa fa-user-circle fs-3" style="color:black" aria-hidden="true"></i></a>
              </li>

          <?php
           
        }
        ?>

      </ul>
    </div>
  </div>
</nav>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js"></script>