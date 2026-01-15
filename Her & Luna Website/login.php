
    <?php

require('dbConnection.php');
include('header.php');

if (isset($_SESSION['error'])) {
  unset($_SESSION['error']);
}


if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $username = $_POST['username'];
    $password = $_POST['password'];

    if (!empty($username) && !empty($password)) {
        $sql= "SELECT id, password,username FROM users WHERE username = '$username'";
        $res= mysqli_query($conn,$sql);
       
       
        if ($res) { 
          $user = mysqli_fetch_assoc($res);
            $hashedPassword= hash('md5',$_POST['password']);
            if ($hashedPassword == $user['password']) {
    
                $_SESSION['username'] = $username;

                header('Location: index.php');
                exit;
            } else {
                $_SESSION['error'] = 'Invalid username or password.';
            }
        } else {
            $_SESSION['error'] = 'Invalid username or password.';
        }

        
    } else {
        $_SESSION['error'] = 'Please fill in all fields.';
    }
}
?>

<div class='container min-vh-75 mb-5 d-flex mt-5 justify-content-center'>
  <div>
    <div class="card text-center">
      <div style="background-color: #ffe4f0;" class="card-header fw-bold fs-3">
        Login
      </div>

      <form method="POST" class="card-body p-5">
        <div class="input-group mb-3">
          <span class="input-group-text" style="background-color: #ffe4f0;" id="basic-addon2">Username</span>
          <input type="text" class="form-control" name="username" aria-label="Recipient's username" aria-describedby="basic-addon2"> 
        </div>

        <div class="input-group mb-3">
          <span class="input-group-text" style="background-color: #ffe4f0;" id="basic-addon2">Password</span>
          <input type="password" class="form-control" name="password" aria-label="Password" aria-describedby="basic-addon2"> 
        </div>

        <button type="submit" style="background-color: #ffe4f0; color:black;" class="btn mt-3">Login</button>
      </form>

      <p style="color: red;">
        <?php 
        if (isset($_SESSION['error'])) {
            echo $_SESSION['error'];
        }
        ?>
      </p>
    </div>
  </div>
</div>

<?php
require('footer.php');
?>
</body>
</html> 