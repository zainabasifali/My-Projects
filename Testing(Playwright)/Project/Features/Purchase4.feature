Feature: Purchase4

A short summary of the feature

@tag1
Scenario: Purchase Failed
	Given user will go to the url demobalze.com
	And User will add product to cart
	And user will click on cart nav
	And user will place his order
	When user will fill not fill name field
	And user will click on purchase button
	Then an error message will appear
