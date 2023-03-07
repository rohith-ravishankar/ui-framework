Feature: Auction

Background: Car Details Upload
	Given I launch the application
	And I enter 'correct' login details
	When I click on 'Login'
	Then I am on 'Dashboard' page
	When I click on 'Auctions'
	And I click on 'Add New Car'
	And I upload car details pdf
	
Scenario: Validate all the fields are filled
	Then I validate all fields are filled

Scenario: Validate correct format
	When I validate formats of fields

#Bug
Scenario: Incomplete fields error message
	#Then I should see error message

Scenario: Validate pre filled values
    When I wait for enrichment completion
	And I click on 'My Auction'
	Then I validate the filled values

#incomplete - would need some work on filling data
Scenario: E2E journey
	When I have 'Yes' 'Roadworthy'
	And I have 'No' 'Engine damage'
    And I have 'Leatherette fabric' 'Upholster type'
	And I click on 'auction details'
	And I enter 'Auction end time'
	And I enter 'Vehicle location'

