Feature: User logout 

  Background:
    Given the user is logged in

  Scenario: User logs out successfully
    When the user clicks the button
    Then the login username should not be visible
   
