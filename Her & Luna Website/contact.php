<?php
include('dbConnection.php');
include('header.php');

// if user is login then take name from there
if (isset($_SESSION['username'])) {
    $user_name = $_SESSION['username'];
}
?>

<!-- Contact Form -->
<h2 class="text-center mb-3 mt-5" style="color:#338899">Contact US</h2>
<div class="container mt-3 justify-content-center w-sm-25 w-lg-50 p-3">
    <form action="./contact_add.php" method="POST" enctype="multipart/form-data">
        <div class="row g-3 align-items-center mb-3">
            <div class="col-sm-3 text-sm-end">
                <label for="firstname" class="col-form-label">Name</label>
            </div>
            <div class="col-sm-6">
                <div class="input-group">
                    <span class="input-group-text"><i class="fas fa-user"></i></span>
                    <input type="text" name="name" class="form-control my-input"
                        value="<?php echo isset($user_name) ? $user_name : ''; ?>" placeholder="John Doe" required>
                </div>
            </div>
        </div>

        <div class="row g-3 align-items-center mb-3">
            <div class="col-sm-3 text-sm-end">
                <label for="email" class="col-form-label">Email</label>
            </div>
            <div class="col-sm-6">
                <div class="input-group">
                    <span class="input-group-text"><i class="fas fa-envelope"></i></span>
                    <input type="email" name="email" class="form-control my-input" placeholder="jhondoe@gmail.com"
                        required>
                </div>
            </div>
        </div>

        <div class="row g-3 align-items-center mb-3">
            <div class="col-sm-3 text-sm-end">
                <label for="review" class="col-form-label">Experience</label>
            </div>
            <div class="col-sm-6">
                <div class="input-group">
                    <span class="input-group-text"><i class="fas fa-check-square" aria-hidden="true"></i></span>
                    <select class="form-select my-input" name="review" required>
                        <option selected>Select your preference</option>
                        <option value="Great">Great</option>
                        <option value="ok">ok</option>
                        <option value="Improvement Required">Improvement Required</option>
                    </select>
                </div>
            </div>
        </div>


        <div class="row g-3 align-items-center mb-3">
            <div class="col-sm-3 text-sm-end">
                <label for="message" class="col-form-label">Message</label>
            </div>
            <div class="col-sm-6">
                <div class="input-group">
                    <span class="input-group-text"><i class="fa-solid fa-message"></i></span>
                    <textarea name="message" class="form-control my-input" placeholder="Write message here"
                        required></textarea>
                </div>
            </div>
        </div>

        <div class="row justify-content-center">
            <div class="col-3 d-flex justify-content-center" style="background-color:black; border-radius:5px;">
                <button type="submit" class="btn text-white" style="background-color:black">Send</button>
                <div class="p-1">
                    <i class="fas fa-paper-plane" style="font-size: 15px; color: white; "></i>
                </div>
            </div>
        </div>

    </form>
</div>

<!-- Location Embed -->
<div class="container mt-5">
    <iframe
        src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d924236.0528563985!2d67.155462!3d25.193202!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3eb33e06651d4bbf%3A0x9cf92f44555a0c23!2sKarachi%2C%20Karachi%20City%2C%20Sindh%2C%20Pakistan!5e0!3m2!1sen!2sus!4v1733890747988!5m2!1sen!2sus"
        class="w-100" height="450" style="border:0;" allowfullscreen="" loading="lazy"
        referrerpolicy="no-referrer-when-downgrade"></iframe>
</div>

<!-- More Details -->
<div class="container mt-5 mb-5 text-center">
    <div class="row">
        <?php $sql = "select * from `company_info` where section_id = 5";
        $result = mysqli_query($conn, $sql);

        while ($row = mysqli_fetch_assoc($result)) {
            $heading = $row['heading'];
            $description = $row['description'];
            $image = $row['image'];
            echo
                '<div class="col-sm border">
                <span><i class="' . $image . '" aria-hidden="true"></i></span>
                 ' . $heading . '
                <p>' . $description . '</p>
                </div>
            ';
        }
        ?>
    </div>
</div>

<?php
include('footer.php');
?>
</body>

</html>