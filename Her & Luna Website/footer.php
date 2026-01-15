<?php
function f_query($id)
{
  include('dbConnection.php');
    $query = "SELECT heading, description, image FROM company_info WHERE section_id = $id";
    $result = mysqli_query($conn,$query);
    return $result;
}
?>

<footer class="text-center text-lg-start text-muted">
 
  <section class="d-flex justify-content-center justify-content-lg-between p-4 border-bottom">
  <div class="me-5 d-none d-lg-block">
      <span>Get connected with us on social networks:</span>
    </div>
    <div>
      <a href="https://www.facebook.com/" target="_blank" class="me-4 text-reset">
        <i class="fab fa-facebook-f"></i>
      </a>
      <a href="https://twitter.com/" target="_blank" class="me-4 text-reset">
        <i class="fab fa-twitter"></i>
      </a>
      <a href="https://www.google.com/webhp?authuser=1" target="_blank" class="me-4 text-reset">
        <i class="fab fa-google"></i>
      </a>
      <a href="https://www.instagram.com/" target="_blank" class="me-4 text-reset">
        <i class="fab fa-instagram"></i>
      </a>
      <a href="https://www.linkedin.com/" target="_blank" class="me-4 text-reset">
        <i class="fab fa-linkedin"></i>
      </a>
      <a href="https://github.com/"  target="_blank" class="me-4 text-reset">
        <i class="fab fa-github"></i>
      </a>
    </div>
    
  </section>
  
  <section class="">
    <div class="container text-center text-md-start mt-5">
     
      <div class="row mt-3">
        
        <div class="col-md-3 col-lg-4 col-xl-3 mx-auto mb-4">
          
          <h6 class="text-uppercase fw-bold mb-4">
            <i class="fas fa-gem me-3"></i>Her & Luna
          </h6>
          <?php
          $result = f_query(3);
          $row = mysqli_fetch_assoc($result);
          echo '<p>'.$row['description'].'</p>'
          ?>
          
        </div>
        
        <div class="col-md-2 col-lg-2 col-xl-2 mx-auto mb-4">
         
          <h6 class="text-uppercase fw-bold mb-4">
            Useful Links
          </h6>
          <p>
            <a href="./index.php" class="text-reset">Her and Luna</a>
          </p>
          <p>
            <a href="./about.php" class="text-reset">About</a>
          </p>
          <p>
            <a href="./contact.php" class="text-reset">Contact</a>
          </p>
          <p>
            <a href="./product.php" class="text-reset">Products</a>
          </p>
        </div>
        

        
        <?php
           $result = f_query(5);
           echo '<div class="col-md-4 col-lg-3 col-xl-3 mx-auto mb-md-0 mb-4">
            <h6 class="text-uppercase fw-bold mb-4">Contact</h6>';
          while( $row = mysqli_fetch_assoc($result)){
            echo '
          <p><i class="'.$row['image'].' me-3"></i>'.$row['description'].'</p>
        ';
          }
           echo '</div>';
        ?>
       
        
      </div>
     
    </div>
  </section>
  

  
  <div class="text-center p-4" style="background-color:#FFE4F0;">
    © 2024 Copyright:
    <a class="text-reset fw-bold" href="https://mdbootstrap.com/">her_Luna.com</a>
  </div>
  
</footer>
