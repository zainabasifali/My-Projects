<?php
ob_start();

include('header.php');
include('dbConnection.php');

if (isset($_SESSION['products']) && count($_SESSION['products']) > 0) {
    echo '<div class="row container mx-auto">';
    foreach ($_SESSION['products'] as $key => $cartItem) {
        $productId = $cartItem['productId'];
        $variantId = $cartItem['variantId'];
        $quantity = $cartItem['quantity'];

        $sql_show = "SELECT id,Image, Quantity FROM `variant` WHERE id = '$variantId'";
        $result = mysqli_query($conn, $sql_show);
        $row = mysqli_fetch_assoc($result);
        $productImage = $row['Image'];
        $variantQuantity = $row['Quantity'];
        $variantId = $row['id'];
        echo '<div class="col-12 col-md-6 col-lg-6 mt-5">
        <img src="' . $productImage . '" height="250px" width="250px"/>
        </div>';

        echo '<div class="col-12 col-md-6 col-lg-6 mt-5">';
        $sql_show = "SELECT p.id, p.name, p.description, p.price FROM products p WHERE p.id = '$productId'";
        $result = mysqli_query($conn, $sql_show);

        if ($result && $row = mysqli_fetch_assoc($result)) {
            $productName = $row['name'];
            $productPrice = $row['price'];
            $productDescription = $row['description'];

            echo '
            <h1>' . $productName . '</h1>
            <b>RS. ' . $productPrice . '.00</b>
            <p class="fs-6">' . $productDescription . '</p>';
            echo 'Quantity: ' . $quantity . '<br>';
            if (isset($_SESSION['username']) && !empty($_SESSION['username'])) {
                echo '<form method="POST"><button type="submit" name="checkout" class="btn cursor-pointer button-class mt-2">Checkout</button></form>';

                if (isset($_POST['checkout'])) {
                    $sql = "update variant set Quantity = Quantity - $quantity where id = $variantId";
                    $result_update = mysqli_query($conn, $sql);

                    if ($result_update) {
                        unset($_SESSION['products'][$key]);
                        $_SESSION['cartCount']--;
                        header("Location: " . $_SERVER['PHP_SELF']);
                        exit;
                    } else {
                        echo '<p class="text-danger">Error updating the quantity. Please try again later.</p>';
                    }
                }
            } else {
                echo '<a href="./login.php" class="btn cursor-pointer button-class mt-2">Login to Checkout</a>';
            }
        }

        echo '</div>';
    }
    echo '</div>';
} else {
    echo "<p class='text-center mt-5'>Your cart is empty.</p>";
}

ob_end_flush();

?>