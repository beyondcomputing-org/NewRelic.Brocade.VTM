# Org.BeyondComputing.NewRelic.Brocade.VTM
New Relic plugin for Brocade Virtual Traffic Manager

# Requirements
1. .Net 4.5
2. Brocade Virtual Traffic Manager v9.9+ or Stingray SteelApp Traffic Manager 9.9+
3. Traffic Manager API 3.3

# Installation
1. Download release and unzip on machine to handle monitoring.
2. Edit Config Files
    rename newrelic.template.json to newrelic.json
    Rename plugin.template.json to plugin.json
    Update settings in both config files for your environment
3. Run Org.BeyondComputing.NewRelic.Brocade.VTM.exe from Command line


Register as service - Optional - Future Support.  V1.0.0 does not currently support running as a native service.
    Register Org.BeyondComputing.NewRelic.Brocade.VTM.exe as a service to autorun on machine without logged on user.
    Run PowerShell command to create service - make sure path is correct:
      new-service -name NewRelic.Brocade.VTM -binaryPathName "C:\Plugins\Brocade.VTM\Org.BeyondComputing.NewRelic.Brocade.VTM.exe"
