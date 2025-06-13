Feature: AddToCart

  Scenario: Add first product to cart and confirm popup
    Given the user is on the homepage
    When the user clicks on the first product
    And the user clicks the "Add to cart" button
    Then the alert message should match the one in data file
