# Org.BeyondComputing.NewRelic.Brocade.VTM
New Relic plugin for Brocade Virtual Traffic Manager

# Requirements
1. .Net 4.5
2. Brocade Virtual Traffic Manager v9.9+ or Stingray SteelApp Traffic Manager 9.9+
3. Traffic Manager API 3.3

# Traffic Manager Configuration
It is highly recommended that you do not use the default or full admin user account, but create a specific account for NewRelic to use.

1. Create a new group for permissions
2. Assign Permissions: 
2a. Advanced Management -> SOAP Control API -> Full
2b. All other Permissions -> Read-Only
3. Create a new User
4. Assign user to previously created group

# Installation
1. Download release and unzip on machine to handle monitoring.
2. Edit Config Files
    rename newrelic.template.json to newrelic.json
    Rename plugin.template.json to plugin.json
    Update settings in both config files for your environment
3. Run plugin.exe from Command line

Use NPI to install the plugin to register as a service

1. Run Command as admin: npi install org.beyondcomputing.newrelic.brocade.vtm
2. Follow on screen prompts
