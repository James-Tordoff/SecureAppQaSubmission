<span style="color:teal; font-size:2vw" >Secure App QA Web Site</span><span style="color:grey;"> - Revision History</span>
<hr style="height:0px;border-top:1px solid teal;margin-top:-10px;" />

<span style="color:orange" id="top-of-document">You must read this before trying to log any history to ensure ISO compliance - <span/>[Instructions](#instructions)

This template must be filled in for any and all work carried out within a project/ even if not checked in/published this must be a full history of work within the project.

| [Date](#date) | [CheckedIn](#checkedIn) | [Revision](#revision) | [Author](#author) | [Reason](#reason) | [Work Carried Out](#work-carried-out) | [Testing Done](#testing-done) | [Verification](#verification) | [Validation](#validation)| [Signatorys](#signatorys) | [Published](#published) |
|:-----:|:-----:|:-----:|:-----:|-------|-------|------|------|------|:-------:|------|
|20/07/23|Y|1|JT|Test Cases|Added new TicketTests test class and renamed UnitTests.cs to UserTests.cs to keep them organised.|Tested the tests in testing environment|N/A|N/A|JT|No|
|06/07/23|Y|1|JT|Authorisation Improvements|Updated UserTickets to to ensure that users cannot access unauthorised tickets. Added a new UserError Page to allow me to send errors to a custom page rather than giving exceptions etc.|Tested in debug|N/A|N/A|JT|No|
|05/07/23|Y|1|JT|Authorisation Improvements|Added Authorised Tags in UserTickets and Authorised(Admin) for ManagerTickets to ensure security.|Tested in debug with different user accounts to ensure the different authorisation levels for pages including trying to access pages they shouldnt be able to via urls|N/A|N/A|JT|No|
|02/07/23|Y|1|JT|Ticket Details|Added a check in ManagerTickets Details where if the AspNetUser isnt found but their ID is, we search for them separately.|Tested in Debug.|N/A|N/A|JT|No|
|26/06/23-1|Y|N/A|JT|Add auto creation of Data| If no Sql DataBase then run in cmd line (Update-Database -context SecureAppQaDbContext) this will generate a new empty database in c:/Temp. Then run project and upon going to log in page it will propogate all data into new database. This creates Users/Roles/Assign roles and creates tickets against users. Admin can see all tickets and which user created them.|Tested in Debug|N/A|N/A|JT|No|
|08/06/23-4|Y|N/A|JT|Add new Test Project into Solution|Added new unit tests into a new Test project and tested Ticket Addition / Edit and User Gathering |Test in Debug|N/A|N/A|JT|No|
|08/06/23-3|Y|N/A|JT|Tidy Up and Add Some Regex|Added Validation toTicket VM and class implemented were required in project, tidy up of some code.|Tested in debug|N/A|N/A|JT|No|
|08/06/23-2|Y|N/A|JT|Enable 2FA|Standard Template comes with 2FA switch to enable using an Authenticator APP Google or Microsoft, enabled system through out App, but have left it as optional as users will not have my authenticator but can show process and allow users to download one for there own account and test.|Tested in Debug|N/A|N/A|JT|No|
|08/06/23-1|Y|N/A|JT|Tidy up some scaffolded templates|Go through and remove some code and details in scaffolded pages so only what is required is sent to the view.|Built still works|N/A|N/A|JT|No|
|07/06/23-1|Y|N/A|JT|Further work and implementing 2FA|Added .NetCoreMailkit to project to enabel to send simple test emails out and capture within PaperCut (A Simple SMTP email server running on desktop) https://github.com/ChangemakerStudios/Papercut-SMTP This enables full testing with out having to create accounts etc.Updated Register and Confirmation with new email sender and code for custom email which sends a time limited confirmation token.Added a lot of stuff. Cookie Policy HTTPS Re-direction, new Ticket system. UserManager Role Creation and User Role Management and allocation. Ticket system for users who can full CRUD own tickets. Ticket system for Managers to oversea all Tickets.Updated various views to access new pages and controls. Updated Database etc. New Authorize view for showing access to page only when logged in.|Ensured project still builds|N/A|N/A|JT|No|
|06/06/23-2|Y|N/A|JT|Adding controls and tools to template|Scaffolded ASP.Net User Identity Pages into project. Created Migration on initial SQL Server Dbase and hosted within Azure. Using Entity First new Features within Asp.Net 7 (https://learn.microsoft.com/en-us/ef/core/cli/dotnet ← cli commands for scaffolding.) To then pull all created ASP.NET User Tables into my own custom Context which I will then create and populate an SQLite Dbase. Add Microsoft EntityFrameworkCore.SQLITE and EntityFramework.Tools to project and some basic configuration settings.|N/A|N/A|N/A|JT|No|
|06/06/23-1|Y|N/A|JT|Check in of Blank Template|Start of new project for Uni Course Simple User Login with 2FA and for testing against Security Risks|N/A|N/A|N/A|JT|No|


<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>

<span style="color:teal; font-size:2vw" id="instructions">Instructions</span> - [Return to top of page](#top-of-document)
<hr style="height:0px;border-top:1px solid teal;margin-top:-10px;" />

<span style="color:orange;">All work must be checked in every day with out fail and multiple times per day depending on quantity of work being done within a project. It does not matter if code is finished if you leave a project you must check-in code to ensure when you return to the code you/or another can pick up from your notes what stage the current branch is in.</span>

| Date | CheckedIn | Revision | Author | Reason | Work Carried Out | Testing Done | Verification | Validation | Signatory | Published |
|:-----:|:-----:|:-----:|:-----:|-------|-------|------|------|:------:|:-------:|------|
|15/01/23|Y|1|AD|Client requested table index on record screen to fit in mobile portrait view with no scrolling|Discussed with client about resolution/how it would look they requested we prove not ideal, implemented changes in development environment to allow client to test|Tested in debug and in development environment|We have done as client requested and passed demo for approval| This does not work at all as screen view is not viable awaiting feedback from client |AD|Yes to development server

<span style="color:teal; font-size:2vw" id="date">Date:</span>
* Date of work carried out plus what release of the day it is i.e. 15/02/23-1 and if another in the same day then 15/02/23-2 etc.

<span style="color:teal; font-size:2vw" id="checkedIn">CheckedIn:</span>
* Yes/No Y/N Answer was this edit checked in to Team Foundation Services or Git Services.

<span style="color:teal; font-size:2vw" id="revision">Revision:</span>
* Revision is for all software/builds/deployments etc, Build No, or Nuget package No, or Xamarin Release No etc.

<span style="color:teal; font-size:2vw" id="author">Author:</span>
* Initials of person writing the Code/doing the changes etc/can be multiple people if budy coding etc.

<span style="color:teal; font-size:2vw" id="reason">Reason:</span>
* Clear description of why changes/updates are being implemented.<br/>
1 If it is a bug fix, copy in details of the problem identified into this column, or a conversation or ticket request identifying the issue.<br/>
2 If it is a request, details of the request etc or copy of email just put good detail.<br/>
3 Implement good practices to identify why you are working on/amending/updating/developing product.

<span style="color:teal; font-size:2vw" id="work-carried-out">Work Carried Out:</span>
* A full clear and detailed write up of work carried out, including additions - changes etc to structure and modules/tools imported.<br/>
* Detail areas worked in and why, identify if work is still required or code has been left in a non working state - consider color highlighting etc to bring attention to non working code during developemnt.

<span style="color:teal; font-size:2vw" id="testing-done">Testing Done:</span>
* Clear and detailed break down of all testing done to show prior to launching to live all processes have been tested and checked. Show testing process from debug/test to live etc.

<span style="color:teal; font-size:2vw" id="verification">Verification:</span>
* Client requests an update to a field to have better wording, verification field would just identify we understood the requirements and we have met the requirements/ or changed them or identfied they will not work and how we resolved to meet expectations.

<span style="color:teal; font-size:2vw" id="validation">Validation:</span>
* Do the changes/updates/development work within practice, i.e. customer request for 5 extra fields within a small screen width, we do the work but in practice the view does not work so we identify we believe the result is not suitable.

<span style="color:teal; font-size:2vw" id="signatorys">Signatorys:</span>
* Initials of all developers involved with this entry and also and supervisor/lead developer who has authorised the publication.

<span style="color:teal; font-size:2vw" id="published">Published:</span>
* Was the site published yes/no - to live/ to staging / to debug / to development etc just a note.
