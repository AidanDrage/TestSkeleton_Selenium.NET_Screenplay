Feature: Wikipedia
    As A web user 
	I Want wikipedia to Work
	So that I am able to access information

@WikipediaHomePage
Scenario: Wikipedia Homepage Displays
	Given The User has Navigated to the wikipedia home page
	Then The Wikipedia Homepage will be displayed

@WikipediaHomePage
Scenario Outline: Wikipedia Homepage Lists Correct Languages
    Given The User has Navigated to the wikipedia home page
	Then Then The Wikipedia Homepage will list <Language> as one of the top 10 languages
	Examples: 
	| Language  |
	| English   |
	| 日本語    |
	| Español   |
	| Deutsch   |
	| Русский   |
	| Français  |
	| Italiano  |
	| 中文      |
	| Português |
	| Polski    |

@WikipediaSearch
Scenario: Wikipedia Search
    Given The User has Navigated to the wikipedia home page
	When The User Searches for "Earth"
	Then The Wikipedia article that the user search for will be displayed