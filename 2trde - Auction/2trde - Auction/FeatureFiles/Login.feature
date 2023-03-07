Feature: Login

Scenario: Login successful
	Given I launch the application
	And I enter 'correct' login details
	When I click on 'Login'
	Then I am on 'Dashboard' page

Scenario: Login Failure
	Given I launch the application
	And I enter 'incorrect' login details
	When I click on 'Login'
	Then I see Incorrect Error message

Scenario: Forgot Password
	Given I launch the application
	When I click on 'Forgot Password'
	Then I am on 'Reset Password' page
