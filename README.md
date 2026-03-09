# Document Approval System

Role-based **document approval workflow system** developed with **C# (.NET 8) Windows Forms** and **SQL Server**.

This application allows organizations to manage document submission, review, approval, rejection, and revision processes with audit logging and role-based access control.

---

# Features

- Role-based authentication and authorization
- Document submission and version management
- Approval / rejection / revision workflow
- Audit logging for all operations
- Email notification support
- Admin panel for user management
- Password management and hashing
- Document history tracking

---

# Technologies

- C# (.NET 8)
- Windows Forms
- SQL Server
- Microsoft.Data.SqlClient
- SHA256 password hashing
- Outlook SMTP notifications

---

# System Architecture

The project consists of the following components:

### OnaySistemi (.NET 8)

Main Windows Forms application responsible for:

- User authentication
- Document management
- Approval workflow
- Audit logging
- Admin operations

### OnaySistemiBildirim (.NET Framework 4.8)

Optional helper application responsible for email notifications.

### Helper Script

`hash_olustur_ve_guncelle.py`

Python script used to generate password hashes and update them in the database.

---

# Database

The application uses a **SQL Server database named `OnaySystem`.**

Database setup scripts include:

- `SQL_SCRIPT_SETUP.sql`
- `ADD_EMAIL_TO_USERS.sql`
- `FIX_MAIL_AND_DIREKTORLUK.sql`
- `SETUP_AUDIT_LOG.sql`

---

# Installation

1. Clone or download the repository
2. Open the solution in **Visual Studio 2022**
3. Restore NuGet packages
4. Configure `App.config`

Example:

```
connectionStrings:OnaySystem
BelgeKlasoru
ImzaKlasoru
SMTP settings (optional)
```

5. Run the database setup scripts
6. Set **OnaySistemi** as the startup project
7. Run the application

---

# Security Notes

- Do not commit connection strings or SMTP credentials
- Sensitive values should be stored locally
- Default passwords must be changed after installation

---

# Project Structure

```
OnaySistemi/
   Forms
   Services
   Helpers
   Program.cs

OnaySistemiBildirim/
   Notification helper application

SQL Scripts/
   Database setup scripts
```

---

# Author

Zeynep Tuncay
