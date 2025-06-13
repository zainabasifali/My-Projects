Feature: TotalAmountCalculation

A short summary of the feature

@tag1
Scenario: Correct Total amount calculated after order confirmation
	Given the user goes to demoblaze website
	And adds multiple products to cart
	And clicks on cart nav
	And clics on place order
	When Fills the purchase form correctly
	Then the user should see the corect amount on the order confirmation card
