This is a C# implementation of the Jeopardy quiz game.
The solution consists of two projects.  One project will handle the game itself;
the other project will handle generating the question sets.


NOTES

Normally, we would want to have the separate projects in different repos;
however, I want to have the programs be self-contained without requiring 
the use of ILMerge or similar.

Additionally, I recognize that 'Jeopardy!' is trademarked; I have not
pursued authorization to use the term, but will gladly rename the project
upon request.  As such, suggestions for a new name are welcome.


FEATURES

Game

* Optional column headings
* Loads questions from config file; JSON or YAML?
* Main window provides file chooser to select the question file
* Once questions are loaded, switch to a full-screen UI
* Exit the full-screen UI if the user presses the 'escape' key

Question Manager

* Creates and edits config files
* Config files will, at minimum, specify groups of questions
* 

FUTURE PLANS

* Devise a 'format version' indicator so that, if features are added to the config files, an older version will be able to detect unsupported functionality
* Enhancement - If more questions are defined for a category than are needed, randomly select a subset to display
* Enhancement - If more categories are defined than needed, randomly select a subset to display

LICENSE

While this project will ultimately be open source, until a proper license
has been selected, it will be visible source, with all rights reserved by
the author, Stephen Wilhelm.