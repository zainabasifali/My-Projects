Feature: SignUp2

@tag1
Scenario: SignUp UnSuccessful
	Given that go to Url www.demoblaze.com, navigate to SignUp page
	And fill existing username & valid password
	When user clicks on Signup button
	Then user should see user already exist alert
