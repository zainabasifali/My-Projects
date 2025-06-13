Feature: Login 6

@mytag
Scenario:  Login UnSuccessful
	Given that you Go to Url www.demoblaze.com, navigate to the login page
	And keep username and password empty
	When User click the Login button
	Then User should see a Alert box saying Please fill out Username and Password.