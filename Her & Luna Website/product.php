<?php
include('header.php');
include('dbConnection.php');
?>

<div style="margin-top: 50px;" class="container min-vh-100">
    <form class="input-group rounded" method="GET">
        <input type="search" class="form-control rounded" name="search" placeholder="Search" aria-label="Search"
            aria-describedby="search-addon" />
            <button class="input-group-text border-0 button-class" id="search-addon" type="submit"> <i class="fas fa-search"></i> </button>
           
    </form>

    <div class="row row-cols-1 mt-3 row-cols-md-3 row-cols-lg-4 g-6 d-flex justify-content-center" >
   
        <?php

        if (isset($_GET['search'])) {
            $search = $_GET['search'];
            $sql = "SELECT p.id,p.name,p.description,p.price, MIN(v.Image) AS Image  FROM products p 
           JOIN variant v ON p.id = v.productId  where name LIKE '%$search%'  or name LIKE '$search%' or name LIKE '%$search' GROUP BY p.id";

        } else if (isset($_GET['categoryId'])) {
                $categoryId = (int) $_GET['categoryId'];
                $sql = "SELECT p.id,p.name,p.description,p.price, MIN(v.Image) AS Image 
                FROM products p 
                JOIN variant v ON p.id = v.productId 
                WHERE p.category_id = $categoryId GROUP BY p.id;";       
             }
        else{
            $sql = "SELECT p.id,p.name,p.description,p.price, MIN(v.Image) AS Image 
            FROM products p 
            JOIN variant v ON p.id = v.productId GROUP BY p.id;"; 
        }
        

        $res = mysqli_query($conn, $sql);
        if ($res && mysqli_num_rows($res) > 0) {
            while ($row = mysqli_fetch_assoc($res)) {
                ?>
                <div class="col px-4 p-md-1 mb-4 mb-md-5">
                    <div class="card h-100 custom-img-thumbnail">
                        <img style="object-fit: cover; "class="card-img-top" src="<?php echo $row['Image']; ?>" alt="Product image" height="250px">
                        <div class="card-body">
                            <h5 class="card-title"><?php echo $row['name']; ?></h5>
                        </div>
                        <div class="card-footer">
                            <div class="d-flex justify-content-between align-items-center ">
                                <b class="text-muted">Rs <?php echo $row['price']; ?></b>
                                <a href="./productDescription.php?productId=<?php echo $row['id'] ?>"
                                    class="btn cursor-pointer button-class">View
                                    Product</a>
                            </div>
                        </div>
                    </div>
                </div>
                <?php
            }
        } else {
            echo '<p>No products found </p>';
        }
        ?>
    </div>
</div>
<?php
include('footer.php');
?>
</body>

</html>