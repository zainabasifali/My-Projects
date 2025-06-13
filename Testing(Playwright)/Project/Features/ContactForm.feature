Feature: Contact

@tag1
Scenario: Contact Successful
	Given that go to url www.demoblaze.com, navigate to contact page
	And fill the email, name and message
	When user click on send message
	Then user should see Thanks for the message Alert
