Feature: CategoryNavigation

@tag1
Scenario: Navigate to a specific category
	Given that the user is on the homepage
	When the user clicks on the Laptops category
	Then the user should see a list of products in the Laptops category

