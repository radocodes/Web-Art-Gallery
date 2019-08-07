# Web-Art-Gallery
Web Art Gallery - Web application, personal website of a professional artist for the presentation and sale of his own artworks and customer orders, such as paintings, illustrations and caricatures to order.

http://emaivanova.com

More details:
- The web app is written by .NET CORE 2.1 ASP MVC, using MS SQL Server and MS Entity Framework.
- It works with individual user accounts authentication Identity (but not scaffolded).
- Every user can sign up, login, logout, change his profile details and password.
- Every signed in user can make orders, send messages to web site owner (the artist) by web app (it saves messages in DB), and to make comments at aticles, at the blog part.

The web app has administration area, where administrator has the following functionality just by UI (User Interface):
- Add/Edit/Delete Artworks with picture and specific details for them (like price, size, category).
- Add/Edit/Delete Artwork Categories with name and picture.
- Add/Edit/Delete Articles.
- Add/Edit/Delete Biography of the web-site owner (the artist).
- To have control of orders, comments, messages and users.
- To make some other user administrator, or to remove user from admin role.

When you start the web app, it auto seeds one basic admin user, where you can start to administering:
- Username: admin
- Pasword: 123456

