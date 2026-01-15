<?php
include("dbConnection.php");
include("header.php");

$productsQuery = "SELECT 
    p.name, 
    p.description, 
    p.price, 
    v.Quantity,
    p.id as productId,
    MIN(v.Image) AS Image
FROM 
    products p
JOIN 
    variant v 
    ON p.id = v.productId
WHERE 
    v.Quantity > 0
GROUP BY 
    p.id, p.name, p.description, p.price;
";

$productsQueryResult = mysqli_query($conn,$productsQuery);

$whyChooseQuery = "SELECT image, heading, description FROM company_info WHERE section_id = 2";
$whyChooseResult = mysqli_query($conn,$whyChooseQuery);

$subscribeQuery = "SELECT image, heading, description FROM company_info WHERE section_id = 6";
$subscribeResult = mysqli_query($conn,$subscribeQuery);



$carouselQuery = "SELECT image, heading, description FROM company_info WHERE section_id = 1";
$carouselResult = mysqli_query($conn,$carouselQuery);

?>


<div id="carouselExample" class="carousel slide " data-bs-ride="carousel">
    <div class="carousel-inner">
        <?php
        if (mysqli_num_rows($carouselResult) > 0) {
            $active = true;
            while ($row = mysqli_fetch_assoc($carouselResult)) {
                $imageData = $row['image'];
                $heading = $row['heading'];
                $description = $row['description'];
                ?>
                <div class="carousel-item <?php echo $active ? 'active' : ''; ?>">
                    <img src="<?php echo $imageData; ?>" class="d-block w-100" alt="Image">
                    <div class="carousel-caption text-black text-start mb-5 col-6 col-lg-3">
                        <h1><?php echo htmlspecialchars($heading); ?></h1>
                        <p><?php echo htmlspecialchars($description); ?></p>
                        <div class="d-grid gap col-9">
                            <a href="./product.php" type="button" class="btn btn-light btn-lg">Shop Now</a>
                        </div>
                    </div>
                </div>
                <?php
                $active = false;
            }
        } else {
            echo "<p>No images found for carousel.</p>";
        }
        ?>
    </div>
    <button class="carousel-control-prev" type="button" data-bs-target="#carouselExample" data-bs-slide="prev">
        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
        <span class="visually-hidden">Previous</span>
    </button>
    <button class="carousel-control-next" type="button" data-bs-target="#carouselExample" data-bs-slide="next">
        <span class="carousel-control-next-icon" aria-hidden="true"></span>
        <span class="visually-hidden">Next</span>
    </button>
</div>


<div class="container-fluid">
    <div class="p-5">
        <div class="d-flex justify-content-between">
            <h2 class="fw-bold">Best Sellers</h2>
            <a href="product.php?" class="d-none d-md-block btn cursor-pointer button-class btn-md">View all Products</a>
        </div>
        <div class="d-flex justify-content-center">
            <div class="row justify-content-between g-4 mt-3">
                <?php
                if (mysqli_num_rows($productsQueryResult) > 0) {
                    $c = 0;
                    while ($row = mysqli_fetch_assoc($productsQueryResult)) {
                        if ($c >= 6) {
                            break;
                        }

                        ?>
                        <div class="col col-12 col-md-6 col-lg-4">
                            <div class="card mx-auto" style="width: 18rem; ">
                                <img style="height: 250px;  object-fit: cover;" src="<?php echo $row['Image']; ?>"
                                    class="card-img-top " alt="...">
                                <div class="p-3">
                                    <h5 class="card-title fw-bold fs-6"><?php echo $row['name'] ?></h5>
                                    <p style="font-size: 14px;" class=""><?php echo "Hurry up Only " . $row['Quantity'] . " left in stock" ?>
                                    </p>
                                </div>
                                <div class="p-3 d-flex justify-content-between" style="background-color:#ffe4f0;">
                                    <p class=" fw-bold fs-6 "><?php echo "Price " . $row['price'] ?></p>
                                    <a href="productDescription.php?productId=<?php echo trim($row['productId']) ?>"
                                        style="color: black;font-weight: 200;text-decoration: none;cursor:pointer"><?php echo " >" ?></a>
                                </div>
                            </div>
                        </div> <?php
                        $c++;

                    }
                } else {
                    echo '<p>No data found for Why Choose Us.</p>';
                }
                ?>
            </div>
        </div>
    </div>
</div>

<div class="container-fluid">
    <div class="why-choose-us  mt-5 p-5 mb-5">
        <h2 class="text-justify">Why Choose Us?</h2>
        <div class="container">
            <div class="row">
                <?php
                if (mysqli_num_rows($whyChooseResult) > 0) {
                    $counter = 0; 
                    while ($row = mysqli_fetch_assoc($whyChooseResult)) {
                        if ($counter >= 3) {
                            break; 
                        }
                        echo '<div class="col-md-4">';
                        echo (!empty($row['image']) ? '<img src="' . htmlspecialchars($row['image']) . '" alt="Icon">' : '');
                        echo '<h4>' . htmlspecialchars($row['heading']) . '</h4>';
                        echo '<p>' . htmlspecialchars($row['description']) . '</p>';
                        echo '</div>';
                        $counter++;
                    }
                } else {
                    echo '<p>No data found for Why Choose Us.</p>';
                }
                ?>
            </div>
        </div>
    </div>
</div>

<div class="container-fluid">
    <div style="background-color:#98b9e7;text-align: center;color:white;padding: 50px 20px;" class="sub fw-bolder mt-5 mb-5">
        <?php
        if($subscribeResult)
        {
            $row=mysqli_fetch_assoc($subscribeResult);
        }
        ?>
        <h2 class="text-justify"><?php echo $row['heading']; ?></h2>
        <h6><?php echo $row['description']; ?></h6>
        <div class="container  p-1 mt-3 ">
        <div class="row">
    <div class="col-md-6 mx-auto mb-3">
        <div class="input-group">
            <input type="text" class="form-control" placeholder="Enter your email" aria-label="Recipient's username" aria-describedby="basic-addon2">
            <button class="btn input-group-text button-class" id="basic-addon2">Subscribe</button>
        </div>
    </div>
</div>

        </div>
    </div>
</div>


<?php
mysqli_close($conn);
include("footer.php");
?>

</body>

</html>