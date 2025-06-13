Feature: Login 2

@mytag
Scenario:  Login UnSuccessful
	Given that go to url demoblaze.com, navigate to the login page
	And enter wrong username and correct password
	When user click the login button
	Then user should see a alert box saying User does not exist.