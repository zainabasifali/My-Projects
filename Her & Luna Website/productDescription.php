<?php
include('header.php');
include('dbConnection.php');

if (isset($_GET['productId'])) {
    $id = $_GET['productId'];
}

// For Add to cart 
if ($_SERVER['REQUEST_METHOD'] === 'POST' && isset($_POST['action']) && $_POST['action'] === 'addToCart') {
    $_SESSION['cartCount']++;
    $quantity = $_POST['countInput'];
    $variantId = $_POST['variantid'];
    $productId = $_POST['productId'];

    $_SESSION['products'][] = [
        'productId' => $productId,
        'variantId' => $variantId,
        'quantity' => $quantity
    ];
    header("Location: " . $_SERVER['PHP_SELF'] . "?productId=" . $id);
}

?>

<!-- Images From Variant Table -->
<div class="row container mt-5 mx-auto">
    <div class="col-sm-12 col-lg-2">
        <?php
        $sql_show = "SELECT id,Image, Quantity FROM `variant` WHERE productId = '$id'";
        $result = mysqli_query($conn, $sql_show);
        $count = 0;

        if ($result) {
            while ($row = mysqli_fetch_assoc($result)) {
                $productImage = $row['Image'];
                $variantQuantity = $row['Quantity'];
                $variantId = $row['id'];

                if ($count == 0) {
                    $defaultImage = $productImage;
                    $defaultQuantity = $variantQuantity;
                    $defaultVariantId = $variantId;
                    echo "
                        <script>
                            var currentQuantity = $defaultQuantity;
                            window.onload = function() {
                                DisplayImage('$defaultImage', $defaultQuantity, $defaultVariantId);
                            };
                        </script>";
                    $count++;
                }

                echo '
                     <div class="border rounded text-center p-2">
                        <a href="javascript:void(0);" onclick="DisplayImage(\'' . $productImage . '\', ' . $variantQuantity . ',' . $variantId . ')">
                            <img src="' . $productImage . '" class="custom-img-thumbnail" height="114px" alt="Variant Image">
                        </a>
                    </div>';
            }
        } else {
            echo "No variants found!";
        }
        ?>
    </div>

    <!-- Centralized Image -->
    <div class="col-sm-12 col-lg-4 text-center p-4 border rounded">
        <img id="mainImage" src="<?php echo $defaultImage; ?>" height="300px" alt="Main Product Image"
            class="custom-img-thumbnail">
    </div>

    <!-- Product Details Display and Review Form on Modal -->
    <div class="col-sm-12 col-lg-5 ms-lg-5">
        <?php
        $sql_show = "SELECT p.id,p.name, p.description, p.price FROM products p WHERE p.id = '$id'";
        $result = mysqli_query($conn, $sql_show);

        if ($result) {
            $row = mysqli_fetch_assoc($result);
            $productName = $row['name'];
            $productPrice = $row['price'];
            $productDescription = $row['description'];
            $productId = $row['id'];
            echo '
            <div class="d-flex justify-content-between">
            <h1>' . $productName . '</h1>
        <button type="button" class="btn review-button" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
        <i class="fas fa-comments fs-5 mt-3" style="color:black;"></i>
        </button>

        <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog">
        <div class="modal-content">
        <div class="modal-header">
            <h1 class="modal-title fs-5" id="staticBackdropLabel">Review Here</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body container mt-3 d-flex justify-content-center p-3">
            <form action="./review_add.php" method="POST" >

             <div class="row g-3 align-items-center mb-3">
                    <div class="col-sm-3 text-sm-end">
                        <label for="name" class="col-form-label">Name</label>
                    </div>
                    <div class="col-sm-9">
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-user"></i></span>
                            <input type="text" name="name" class="form-control my-input" placeholder="Jhon Doe" required>
                        </div>
                    </div>
                </div>
                        <div class="input-group">
                            <input type="int" hidden name="productid" class="form-control my-input" value="' . $id . '" required>
                        </div>

                <div class="row g-3 align-items-center mb-3">
                    <div class="col-sm-3 text-sm-end">
                        <label for="message" class="col-form-label">Message</label>
                    </div>
                    <div class="col-sm-9">
                        <div class="input-group">
                            <span class="input-group-text"><i class="fa-solid fa-message"></i></span>
                            <textarea name="message" class="form-control my-input" placeholder="write here" required></textarea>
                        </div>
                    </div>
                </div>

                <div class="row g-3 align-items-center mb-3">
                    <div class="col-sm-3 text-sm-end">
                        <label for="rating" class="col-form-label">Rating</label>
                    </div>
                    <div class="col-sm-9">
                        <div class="input-group">
                            <span class="input-group-text"><i class="fa-solid fa-ranking-star"></i></span>
                            <input type="number" name="rating" class="form-control my-input" placeholder="out of 10" min="1" max="10" required>
                        </div>
                    </div>
                </div>
            
        <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="submit" class="btn button-class">Done</button>
            </form>
        </div>
        </div>
        </div>
    </div>
    </div>
        </div>
            <b>RS. ' . $productPrice . '.00</b>
            <p class="fs-6">' . $productDescription . '</p>

            <div class="input-group mb-3">
                <button class="btn btn-outline-secondary" type="button" onclick="changeCount(-1)">-</button>
                <input type="text" id="visibleCountInput" class="text-center" value="0" readonly>
                <button class="btn btn-outline-secondary" type="button" onclick="changeCount(1)">+</button>
            </div>';
        }
        ?>

        <form method="POST" action="">
            <input type="hidden" name="action" value="addToCart">
            <input type="hidden" id="countInput" name="countInput" value="0">
            <input type="hidden" name="productId" value="<?php echo $productId?>">
            <input type="hidden" id="variantId" name="variantid" value="0">

            <button id="addToCartButton" class="btn cursor-pointer button-class col-12 border text-black" type="submit" disabled>Add to
                Cart</button>
        </form>
    </div>
</div>

<!-- Customer Reviews -->
<h2 class="text-center mt-5 fw-bold">Customer Reviews</h2>
<?php
$sql = "select * from `reviews` where productId = $id";
$result = mysqli_query($conn, $sql);
if (mysqli_num_rows($result) > 0) {

    while ($row = mysqli_fetch_assoc($result)) {
        $reviewid = $row['id'];
        $name = $row['name'];
        $message = $row['message'];

        echo
            '<div class="container">
            <img src="./Images/star.png" class="d-block mx-auto" style="width:110px" alt="">
            <div class="d-flex ">
                <span><i class="fas fa-user fa-lg me-3" style="color:#338899"></i></span>
                <h4 class="mx-auto">' . $name . ' - <span class="badge text-white"
                        style="background-color:#338899">Verified</span></h4>
            </div>
            <p>' . $message . '</p>
            <hr>
        </div>';
    }
} else {
    echo '<p class="text-center">No reviews yet. Be the first to leave a review!</p>';
}
?>

<!-- Randomized Products -->
<?php
$sql = "SELECT p.id,p.name,v.Image FROM products p INNER JOIN variant v ON p.id = v.productId ORDER BY RAND() LIMIT 4";
$result = mysqli_query($conn, $sql);
if ($result) {
    echo ' <div class="row container-fluid mt-5 mb-5 mx-auto">
    <h2 class="text-center mb-5 fw-bold">You may also like</h2>';

    while ($row = mysqli_fetch_assoc($result)) {
        echo '
              <div class="col-sm-12 col-md-6 col-lg-3 text-center border rounded p-2 ">
            <figure>
                    <a href="./productDescription.php?productId=' . $row['id'] . '" ><img src="' . $row['Image'] . '" width="250px" height=250px class="custom-img-thumbnail mb-3"/></a>
                <figcaption>' . $row['name'] . '</figcaption>
                </figure>
                </div>
            ';
    }
}
echo '</div>';
include('footer.php');
?>

<!-- Javascript for Runtime Changes -->
<script>
    let count = 0;

    function DisplayImage(path, quantity, variantId) {
        document.getElementById('mainImage').src = path;
        currentQuantity = quantity;
        count = 0;
        document.getElementById('variantId').value = variantId;
        document.getElementById('countInput').value = count;
        toggleAddToCartButton();
    }

    function changeCount(change) {
        console.log('Before:', { count, currentQuantity, change });
        if (change === 1 && count < currentQuantity) {
            count++;
        } else if (change === -1 && count > 0) {
            count--;
        } else if (change === 1 && count >= currentQuantity) {
            alert('Out of stock');
        }
        console.log('After:', { count });

        document.getElementById('visibleCountInput').value = count;
        document.getElementById('countInput').value = count;
        toggleAddToCartButton();
    }

    function toggleAddToCartButton() {
        const addToCartButton = document.getElementById('addToCartButton');
        addToCartButton.disabled = count === 0;
    }

</script>