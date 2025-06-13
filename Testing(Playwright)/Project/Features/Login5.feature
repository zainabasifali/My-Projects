Feature: Login 5

@mytag
Scenario:  Login UnSuccessful
	Given that you go to Url www.demoblaze.com, navigate to the login page
	And fill correct username and keep password empty
	When User clicks the Login button
	Then User should see a alert box saying Please fill out Username and Password.