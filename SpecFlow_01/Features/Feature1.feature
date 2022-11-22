Feature: Feature1

A short summary of the feature

@tag1
Scenario: backend test reqres.io
	Given reqres get test api
	When I request the get from api
	Then response status should be OK