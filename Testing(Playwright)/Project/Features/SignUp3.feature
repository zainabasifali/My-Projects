Feature: SignUp3

@tag1
Scenario: SignUp UnSuccessful
	Given that go to url www.Demoblaze.com, navigate to SignUp page
	And keep username and password Empty
	When user click on Signup button
	Then user should see Fill in all the fields alert
