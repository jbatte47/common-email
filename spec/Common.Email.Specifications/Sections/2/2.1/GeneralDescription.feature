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
	| this                                                                                          |
	| !"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{}~ |
	| ~}{zyxwvutsrqponmlkjihgfedcba`_^]\[ZYXWVUTSRQPONMLKJIHGFEDCBA@?>=<;:9876543210/.-,+*)('&%$#"! |
# unfortunately we have to stop with these two examples because the full ASCII range includes
# non-printable characters, which obviously aren't easy to represent in text-based specs

@staticInput
Scenario Outline: Error
	Given I have an instance of the message validator
	When I validate <this>
	Then a format exception should be thrown during the validation call
	Examples: 
	| this              |
	| Прывітанне свет   |
	| 你好世界              |
	| Xin chào thế giới |
	| नमस्ते विश्व      |
	| Hej Världen       |
# this is "Hello World" in Belarussian, Chinese, Vietnamese, Hindi, and Swedish.
# they all contain at least one non-ASCII character.
# they also represent the first languages of developers who are actually associated with this project.
# if you contribute and your first language is not English, and the phrase "Hello World" contains
# non-ASCII characters when written in your language, add it to the list!