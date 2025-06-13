Feature: SignUp1

@tag1
Scenario: SignUp Successful
	Given that go to url www.demoblaze.com, navigate to SignUp page
	And fill valid username and password
	When user clicks on signup button
	Then user should see signUp successful alert
