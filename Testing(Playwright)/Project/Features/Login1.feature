Feature: Login 1

@mytag
Scenario:  Login successful
	Given that go to url abc.com, navigate to the login page
	And enter username and password
	When user clicks the login button
	Then user should see a message "Welcome Hur"