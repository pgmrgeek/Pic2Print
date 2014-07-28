Pic2Print
=========

BETA Release 7.02 (A work in progress) 

"Pic2Print.exe" is a Microsoft Visual Studio 2010 Visual Basic .NET program that provides a user interface to an incoming stream of images.  It operates on a set of folders for the entire workflow. See the folder heirarchy below for more information. The toplevel folder must be named "OnSite" and must be located in the root of Drive C.  Sorry, but its hardcoded for now.  

c:\OnSite                - Parent folder and Kiosk folder. Any jpg landing here gets processed.<br>
c:\OnSite\actions        - holds Photoshop's action sets and javascript.<br>
c:\OnSite\backgrounds    - holds the print layouts in subfolders, spec'd by the .CSV files.<br>
c:\OnSite\capture        - incoming .jpgs can land here, to be managed by the human operator/technician.<br>
c:\OnSite\cloud          - suggested output folder for the cloud/slideshow.  Not really necessary.<br>
c:\OnSite\orig           - after images are processed, the original files are moved here.<br>
c:\OnSite\printed        - the processed files are written here - .GIF, .PSD with layers, and a flattened .JPG.<br>
c:\OnSite\software       - windows runtime code and support files.<br>

The main operations are Managed mode and Kiosk mode. 

In Managed Mode, the technician has a control panel and is given a "Refresh" button that turns green when new  images arrive. Clicking "Refresh", the images are presented so the technician can select (i.e., click on) an image, an optional background (for greenscreen), then click a number between 1-10, for 1 to 10 prints.  If multiple images are needed (for photostrips or .GIFs), the technican clicks the first image, then the "L" button to load; repeating until the GIF/Number buttons are enabled. Once the buttons are enabled, the technician selects the last image, then clicks "GIF" or a numbered print button.

In Kiosk mode, the incoming images are processed according to selections made in the configuration panel.  That means the technician can specify foreground overlay, greenscreen, layout selection, # of prints per image, etc, and the kiosk mode will abide by these settings.  For example, selecting a three image photo strip layout with greenscreen and overlay, will be processed in Kiosk mode once three images land in the folder.

Animated gifs are supported and images can be emailed, sent as MMS messages, and copied to another folder for dropbox or slideshows.  All this functionality works as of today (first release) with further enhancements forth coming.  The actual source code to Pic2Print will be located in its own repository, not in this package. 

To Run this program - 

Pull the PhotoboothMGR repository from github.  Once you pull it, copy/move the contents to the "c:\OnSite" folder.
Follow the instructions in the README.MD

All systems Go! Please use GitHub Issues list on this repository to address problems and bugs.

Doug Cody
Bay Area Event Photography
www.bayareaeventphotography.com

References -

The mail program is a github project - https://github.com/muquit/mailsend. Please send kudos and cash..

Microsoft .NET framework 4.5 download page - http://www.microsoft.com/en-us/download/details.aspx?id=30653

Inspiration -

My daughter, Michelle Palmer, who needed this program to get started in the business.


