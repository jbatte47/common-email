Feature: Line Length Limits
	In order to maintain conformance to RFC 5322 Section 2.1.1, "Line Length Limits"
	As a common-email API consumer
	I want to be able to validate incoming message lines as needed to ensure valid length

Scenario Outline: Valid
	Given I have an instance of the message validator
	When I validate a line with <count> characters
	Then no exception should be thrown during the validation call
	And no calls should be made to the logger
	Examples: 
	| count |
	| 1     |
	| 78    |

Scenario Outline: Warning
	Given I have an instance of the message validator
	When I validate a line with <count> characters
	Then the logger assigned to the validator should receive a warning
	And no exception should be thrown during the validation call
	Examples: 
	| count |
	| 79    |
	| 100   |
	| 998   |

Scenario Outline: Error
	Given I have an instance of the message validator
	When I validate a line with <count> characters
	Then a format exception should be thrown during the validation call
	Examples: 
	| count |
	| 999   |
	| 9980  |
	| 99800 |