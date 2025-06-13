Feature: Purchase1

A short summary of the feature

@tag1
Scenario: Purchase Successfull
	Given user will navigate to demoblaze website
	And user will add product to cart and navigate to cart
	And user will click on place order
	When user will fill the purchase form
	Then user will see the confirm message
