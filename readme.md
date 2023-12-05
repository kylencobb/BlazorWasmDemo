# Blazor Wasm Demo
This project was created in a few hours during Thanksgiving to demonstrate proficiency in Blazor WebAssembly. This is not intended to be a robust demonstration. I am excited to be working with Blazor.

## Local Setup
To run this application locally, use Multiple Startup Projects in Visual Studio (starting both the Api and Client projects).

## Authentication
You can login using any credentials. The Api provisions a Json Web Token with an 'Administrator' role which can be used to view a protected table on the homepage and an Admin page which allows you to manage a list of people.

## Home
Once logged in, the homepage will display a TableSearchSort component filled with dummy data. You can search for any text within the table to filter the list of people. You can also sort the table by selecting the table header for the column you wish to sort.

## Admin Page
Contains a small form for adding a new person to an existing list of people.
