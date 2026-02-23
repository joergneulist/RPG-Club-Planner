# RPG Club Event Manager

A secure, mobile-first web application for GMs to host tabletop sessions, manage player RSVPs, and handle automated waitlisting.

## Project Vision

To provide a streamlined, "fair-play" scheduling platform for a small RPG club. The app ensures GMs have full control over their tables while providing players with a transparent, real-time view of event availability.

## Requirements & Features

Role-Based Access (RBAC):

* Admin: Member roster management, role assignment (Player/GM/Admin), and user deletion.

* GM: Create/Edit events, upload images, manage all RSVPs, and add "Guest Placeholders."

* Player: View calendar, RSVP to events, and manage their own profile.

* Pending: New registrants are locked in a "limbo" state until an Admin assigns a role.

Core Functionality:

* The Calendar: A responsive view of upcoming sessions with click-to-detail functionality.

* Waitlist Engine: Automated "First-Come, First-Served" logic. If a confirmed player cancels, the next person in the timestamp queue is automatically promoted.

* Privacy-First: Sensitive data (Emails/Phone numbers) is only visible to the user themselves and Admins. Others see only names and RSVP status.

* Mobile Ready: Fully responsive UI for on-the-go sign-ups.

## Azure Architecture (Free-Tier Optimized)

The system is built on a serverless stack to minimize costs and maximize security:

* Hosting: Azure Static Web Apps (Free Tier)

  Serves the frontend (React/Vue/Tailwind) and provides integrated Auth.

* API Layer: Azure Functions

  Serverless endpoints that handle business logic and waitlist calculations.

* Database: Azure SQL Database (Free Offer)

  Stores users, events, and RSVPs with Row-Level Security principles.

* Storage: Azure Blob Storage (LRS)

  Securely hosts event images using Shared Access Signatures (SAS) to prevent unauthorized hotlinking.

* Security:

  Managed Identity: Connects Functions to SQL without storing passwords in code.

  UTC Standardization: All event times stored in UTC to prevent timezone conflicts.
