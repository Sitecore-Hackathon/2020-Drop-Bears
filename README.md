![Hackathon Logo](documentation/images/hackathon.png?raw=true "Hackathon Logo")

# Drop Bears Hackathon Website

For our submission we have chosed to build the Sitecore Hackathon Website project.
This project has the following features:
 - Ability to reqister via Sitecore Experience Forms
 - Use of workflow for moderation and approvals
 - Dynamic listing of Teams and Team Members

## Technical overview

The project uses a number of features of Sitecore:
- Sitecore Experience Forms
- Custom submit action to put items on a Rebus queue
- Transfer of data to Master database via Rebus message handler
- Automatic enrolment in workflow with publish on approval

## Installation instructions

**IMPORTANT!**
As we are using custom messaging on the Sitecore service bus via Rebus, the messaging user must be allowed to create tables (for the queue) and allowed access to the appropriate schema. This is really only required on the first run in a new environment, as no further tables are created after that.

1. Update SQL privileges for *messaginguser*
```
USE ****_messaging
GRANT CREATE TABLE TO [messaginguser];
GRANT ALTER ON SCHEMA::dbo TO [messaginguser];
GO
```

2. Deploy TDS items
In the following order, push Sitecore items from the solution to your site using TDS-
- Hackathon.Feature.PageContent.Master
- Hackathon.Feature.TeamRegistration.Master
- Hackathon.Project.Website.Master
- Hackathon.Project.Website.Content.Master

3. Publish the following web projects from the solution
- Hackathon.Feature.PageContent
- Hackathon.Feature.TeamRegistration
- Hackathon.Project.Website

## Usage

1. Create a Hackathon event
The site allows you to create as many Hackathon events as you wish to publish. These can be added under the Events node in the tree.
Populate the properties of the event to suit your needs.

2. Add a form to the Hackathon event
There is a Sitecore Forms template *Register Team Form Template* which you must use to create a new Registration Form to add to the new Hackathon event.
The new form thus created  then needs to be added to the "phForm" placeholder on the Hackathon Event page created in step 1.
The form must have the correct Hackathon set in a drop-down, since it was not feasible to dynamically insert a hidden input field with the context page ItemID.

3. Publish and moderate
Teams can register via the form. Their details are processed by a custom submit action and the data sent via a message on the Rebus queue.
There is a handler that receives the message, and adds the Team and Member items to the relevant Hackathon Event as a child item.
The new items are added to the "Team Workflow" and are set to Awaiting Approval. Approval of the Team will publish the Team and child Member items.


## Technical hurdles encountered
- Creation of a new Rebus messaging table was limited by *messaginguser* not having SQL Create Table privileges
- No ability to add hidden input field to Sitecore Forms. This was desirable so that the form could pass through the Hackathon item ID and thus have new Teams and Members added to the correct Hackathon event. To work around this we created a Sitecore Form template that can be used as a template for the a new form to be added to each new Hackathon event.

## Things we would do if we had more time
- Addition of team members to xDB as Contacts
- Dynamic enrolment in Marketing Automation plans
- Use of EXM and Marketing Automation to manage the enrolment process and to send alerts at key dates (2 weeks before, 1 week before, etc)
- Find a good way to add a hidden input field with the context page ID, or use Sitecore Form Extensions from Bart Verdonck.
- Move the Teams and Members outside of the website tree.

# Submission Boilerplate

Welcome to Sitecore Hackathon 2020.

The Hackathon site can be found at http://www.sitecorehackathon.org/sitecore-hackathon-2020/

The purpose of this repository is to provide a sample which shows how to structure the Hackathon submissions.


## Entry Submission Requirements 

All teams are required to submit the following as part of their entry submission on or before the end of the Hackathon on **Saturday  February 29th 2020 at 8PM EST**. The modules should be based on [Sitecore 9.3 (Initial Release)](https://dev.sitecore.net/Downloads/Sitecore_Experience_Platform/93/Sitecore_Experience_Platform_93_Initial_Release.aspx).

**Failure to meet any of the requirements will result in automatic disqualification.** Please reach out to any of the organisers or judges if you require any clarification.

- Sitecore 9.3 (Initial Release) Module (Module install package)
   - An installation Sitecore Package (`.zip` or `.update`)

- Module code in a public Git source repository. We will be judging (amongst other things):
  - Cleanliness of code
  - Commenting where necessary
  - Code Structure
  - Standard coding standards & naming conventions

- Precise and Clear Installation Instructions document (1 – 2 pages)
- Module usage documentation on [Readme.md](documentation) file on the Git Repository (2 – 5 pages)
  - Module Purpose
  - Module Sitecore Hackathon Category
  - How does the end user use the Module?
  - Screenshots, etc.

- Create a 2 – 10 minutes video explaining the module’s functionality (A link to youtube video)

  - What problem was solved
  - How did you solve it
  - What is the end result
