<?php
include("header.php");
function query($id)
{
    include("dbConnection.php");
    $query = "SELECT heading, description, image FROM company_info WHERE section_id = $id";
    $result = mysqli_query($conn, $query);
    return $result;
}
$result = query(3);
$row = mysqli_fetch_assoc($result);

echo '
    <div class="container-fluid px-0">
    <div class="row mb-4 mx-0">
            <div class="col-12 position-relative px-0">
                <img src="' . htmlspecialchars($row['image']) . '" class="img-fluid w-100 "
                    style="height: 400px; object-fit: cover" alt="About Us Image">

                <div class="position-absolute top-50 start-50 translate-middle text-center text-white"> <h1 class="fw-bold">' . htmlspecialchars($row['heading']) . '</h1> </div>
            </div>
             <div class="col-6 mx-auto mt-5 mb-5 fs-4 fw-bold text-center">
             ' . htmlspecialchars($row['description']) . '
            </div>
        </div>
        </div>
    ';
$result = query(4);
if (mysqli_num_rows($result) > 0) {
    echo '<div class=container>';
    $counter = 0;
    while ($row = mysqli_fetch_assoc($result)) {
        $isImageLeft = $counter % 2 == 0;
        if ($isImageLeft) {
            ?>
            <div class="row mb-4 d-flex align-items-center">
                <div class="col-sm-5 col-md-12 col-lg-5">

                    <img src="<?php echo htmlspecialchars($row['image']); ?>" class="img-fluid w-100 custom-img-thumbnail"
                        style="height: 500px; object-fit: cover;" alt="About Us Image">

                </div>

                <div class="col-sm-12 col-md-12 col-lg-6 mt-3 text-center">
                    <h2>
                        <?php echo htmlspecialchars($row['heading']); ?>
                    </h2>
                    <p class="fs-5"><?php echo htmlspecialchars($row['description']); ?></p>
                    <a href="#" class="btn mt-3 btn-lg button-class">Learn More</a>
                </div>
            </div>


        <?php } else { ?>
            <div class="row mb-4 d-flex align-items-center">
                <div class="col-sm-12 col-md-12 col-lg-6 mt-3 text-center">
                    <h2>
                        <?php echo htmlspecialchars($row['heading']); ?>
                    </h2>
                    <p class="fs-5"><?php echo htmlspecialchars($row['description']); ?></p>
                    <a href="#" class="btn mt-3 btn-lg button-class">Learn More</a>
                </div>
                <div class="col-sm-5 col-md-12 col-lg-5 mt-3">
                    <img src="<?php echo htmlspecialchars($row['image']); ?>" class="img-fluid w-100 custom-img-thumbnail"
                        style="height: 500px; object-fit: cover;" alt="About Us Image">

                </div>
            </div>
            <?php

        }
        $counter++;
    }
}
echo '</div>';


require("footer.php");
?>