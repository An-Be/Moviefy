# Moviefy

This is my first project using ASP.NET Core, MVC, and CRUD Operations. It is a webapplication in which authenticated users can watch movies, add movies, edit existing movies, and
see a list of available movies. A specific user has the ability to also delete movies. I gave the specific user the ability to do that by using Policy Based Authorization. 
Anonymous users are able to see a list of movies available but they can not complete any of the editing features until they register. This project has two seperate databases, one 
for movies and one for the users using sql server.

I wanted to add a little bit of customization to .Nets original layout so I made it look a bit like Netflix by changing font colors and the background.

I had a bit of trouble building this at first because I was confused on how to give just one specific user the ability to delete, I first used some if statements in the controller
but that was not secure so I looked into roles and later decided to speak to my instructor WOZ-U for some further assistance on best practices. After a bit of research he let me
know about Policy-based authorization instead of Role-Based authorization, so we gave that a try and it turned out great.

I'd like to expand on this project and add additional views in which an authenticated user can keep track of what movies they have watched and also keep track of how many times
they have watched certain movies.

Here is an image of the List of movies anonymous users can see
![Moviefy](Moviefy/ReadMeImage/ReadMeImage.png)

More movies can be added to the database by authenticated users
