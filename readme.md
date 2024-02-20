# Blazor Wasm Demo

## Local Setup
To run this application locally, set WasmDemo.Api as the startup project and run using the https debug profile.

## Authentication
You can login using any credentials. The Api provisions a Json Web Token with an 'Administrator' role which can be used to view a protected table on the homepage, an Admin page which allows you to manage a list of people, and a page on which you can play [Conway's Game of Life](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life).

## Home
Once logged in, the homepage will display a TableSearchSort component filled with dummy data. You can search for any text within the table to filter the list of people. You can also sort the table by selecting the table header for the column you wish to sort.

## Admin Page
Contains a small form for adding a new person to an existing list of people.

## Conway's Game of Life
An interactive page where users can play [Conway's Game of Life](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life). Select cells in the grid to set the initial population. Then select 'Play' to watch the game play out.