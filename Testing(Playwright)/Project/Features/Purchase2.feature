Feature: Purchase2

A short summary of the feature

@tag1
Scenario: Purchase Failed
	Given User will go to demoblaze website
	When Add product to cart
	And Navigate to cart
	And Click place order
	And User will fill only name field of the purchase form
	Then User will see an error message to fill all fields
