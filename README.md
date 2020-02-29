![Hackathon Logo](http://www.sitecorehackathon.org/wp-content/uploads/2017/01/Sitecore-Hackathon-logo-small-own-it.png "Hackathon Logo")

# Drop Bears Hackathon Website

For our submission we have chosed to build the Sitecore Hackathon Website project.
This project has the following features:
 - Ability to register via Sitecore Experience Forms
 - Use of workflow for moderation and approvals
 - Dynamic listing of Teams and Team Members

## Technical overview

The project uses a number of features of Sitecore:
- Sitecore Experience Forms
- Custom submit action to put items on the Rebus message queue
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
The new form thus created then needs to be added to the "phForm" placeholder on the Hackathon Event page created in step 1.
The form must have the correct Hackathon set in a drop-down, since it was not feasible to dynamically insert a hidden input field with the context page ItemID.

3. Publish and moderate
Teams can register via the form. Their details are processed by a custom submit action and the data sent via a message on the Rebus queue.
There is a handler that receives the message, and adds the Team and Member items to the relevant Hackathon Event as a child item.
The new items are added to the "Team Workflow" and are set to Awaiting Approval. Approval of the Team will publish the Team and child Member items.

4. Profit!
![image.png](image.png?raw=true "Screenshot")

## Technical hurdles encountered
- Creation of a new Rebus messaging table was limited by *messaginguser* not having SQL Create Table privileges
- No ability to add hidden input field to Sitecore Forms. This was desirable so that the form could pass through the Hackathon item ID and thus have new Teams and Members added to the correct Hackathon event. To work around this we created a Sitecore Form template that can be used as a template for the a new form to be added to each new Hackathon event.

## Things we would do if we had more time
- Make it look pretty
- Addition of team members to xDB as Contacts
- Dynamic enrolment in Marketing Automation plans
- Use of EXM and Marketing Automation to manage the enrolment process and to send alerts at key dates (2 weeks before, 1 week before, etc)
- Find a good way to add a hidden input field with the context page ID, or use Sitecore Form Extensions from Bart Verdonck.
- Move the Teams and Members outside of the website tree.
