
Feature: Login 4

@mytag
Scenario:  Login UnSuccessful
	Given that you go to url www.demoblaze.com, navigate to the login page
	And keep username empty and fill correct password
	When User clicks the login button
	Then user should see a alert box saying Please fill out Username and Password.