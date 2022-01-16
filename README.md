# QA Automation Tests
This repository contains various types of automated tests for the Wiggle website, reporting for the tests as well as functionallity to keep the frontend tests in sync with our test management tool [Testrail](https://wigglecrcqa.testrail.net/index.php?/dashboard)

All tests are writen in [Gherkin syntax](https://cucumber.io/docs/gherkin/) in [Testrail](https://wigglecrcqa.testrail.net/) and are pulled down via the [TestRailSync](https://github.com/WiggleCRC/qa-testrailsync) tool to be implemented in C# with the help of [Specflow](https://specflow.org/)

![UI Tests](https://github.com/WiggleCRC/qa-automation-tests/actions/workflows/RunUITests.yml/badge.svg) ![API Tests](https://github.com/WiggleCRC/qa-automation-tests/actions/workflows/RunAPITests.yml/badge.svg)

---

## Project Structure
In the repository there are 3 test projects, Accessibility Tests, UI Tests and API Tests as well as 2 "Provider" projects Selenium Provider and API provider 

### Accessibility Tests
The Equality Act 2010 specifies that a site owner is required to make ‘reasonable adjustments’ to make their site accessible to people with disabillities. This project will output a report of the accessibility definitions as defined by [AxE](https://www.deque.com/axe/) which can be used to analyze the accessibility of the site and decide if any action needs to be taken. 

### UI Tests
Tests that test the front end of the website live here, they are executed using Selenium Webdriver (which is insantiated via the SeleniumProvider project) to control a browser.

Tests are implemented using the [screenplay development pattern](https://serenity-js.org/handbook/design/screenplay-pattern.html) allowing test code to be written as a series of tasks that can be preformed and questions that can be asked about the current state of the system. This makes code far more modular and reusable by sticking more tightly to the single responsibility principle than the traditionally used page object model. 

It also allows very simple syntax in the test steps that should be easy to read regardless of the technical background of the reader, for example
> Customer.TriesTo(Navigate.ToHomepage())
> Customer.TriesTo(Add.ProductToBasket("Vitus Necleus Youth 24 Hardtail Bike"))
> Customer.TriesTo(Navigate.ToBasket())
> Customer.Asks(WhatItemsAreInTheBasket())

A report will be output by [Extent Reports](http://www.extentreports.com/) as well as being reported back into Testrail if needed.

### API tests
Tests performed entirely via api are in here, these can be tests that are testing a service directly or front end tests that have been decided to be run via api calls rather than via the UI for speed reasons.

#### SeleniumProvider
The SeleniumProvider project contains a factory that is able to provide a selenium webdriver to any test on demand for various diffrent browsers and screen size configurations  

#### APIprovider
The API provider contains several generic API methods all written using .NET Core's system.net.http as well as any third party API bindings that are needed in the other projects

---

## Running tests
The test can be run through Github actions by going to the actions tab on this repo, selecting "RunUITests" or "RunApiTests" and using the workflow dispatch option to kick off a test run. Instructions for downloading and running the tests locally can be found [Here](https://wigglecrc.atlassian.net/wiki/spaces/TEST/pages/30769163/How+to+run+locally)

