Feature: Purchase5

  Scenario: User completes a purchase without add to cart
    Given the user is on the Demoblaze homepage
    When the user clicks on the "Cart" link in the navbar
    And the user click the "Place Order" button
    Then a modal form should appear for entering purchase details
    When the user fills in the form with data from "data.json"
    And the user clicks the  button
    Then a confirmation modal should appear with the message 
