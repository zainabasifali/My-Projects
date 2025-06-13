Feature: DeleteProductFromCart

  Scenario: Add a product to the cart and remove it
    Given the User is on the homepage
    When the user clicks on the product 
    And the user clicks the Add to cart button
    And accepts the alert popup
    And the user navigates to the cart page
    And the user deletes the added product from the cart
    Then the product with same name no longer visible in the cart
