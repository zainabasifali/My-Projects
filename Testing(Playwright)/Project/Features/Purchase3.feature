Feature: Purchase3

A short summary of the feature

@tag1
Scenario: Purchase Failed
	Given user will navigate on demoblaze website
	And user will add a product to cart
	And user will click on cart 
	And user will place order
	When user will not fill any fields in purchase form
	And user will click on purchase
	Then user will see an error message alert
