Feature: Login 3

@mytag
Scenario:  Login UnSuccessful
	Given that go to url www.demoblaze.com, navigate to the login page
	And enter correct username and wrong password
	When User clicks the Login Button
	Then user should see a alert box saying Wrong Password.