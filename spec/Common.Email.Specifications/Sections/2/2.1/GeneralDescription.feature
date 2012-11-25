Feature: General Description
	In order to maintain conformance to RFC 5322 Section 2.1, "General Description"
	As a common-email API consumer
	I want to be able to validate incoming message strings as needed to ensure valid character range

@staticInput
Scenario Outline: Valid
	Given I have an instance of the message validator
	When I validate <this>
	Then no exception should be thrown during the validation call
	And no calls should be made to the logger
	Examples: 
	| this                                         |
	| the quick brown fox jumped over the lazy dog |
	| !!!### THIS IS A MESSAGE ###!!!              |

@staticInput
Scenario Outline: Error
	Given I have an instance of the message validator
	When I validate <this>
	Then a format exception should be thrown during the validation call
	Examples: 
	| this        |
	| 片仮名         |
	| स्वतन्त्रता |
	| ราชาศัพท์   |