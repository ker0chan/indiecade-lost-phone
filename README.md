# "Indiecade Lost Phone"
This is the short demo used for the 2017 [Indiecade Europe](http://indiecade-europe.eu/) talk: **"The Building Blocks of a Lost Phone Game"**
>How do you design and build a fake phone interface providing all the necessary mechanics of a non-linear narrative investigation game? Diane Landais and Miryam Houali will build with you a very simple "lost phone" game (almost) from scratch, using Unity, some basic tools, and the practical feedback from their previous game A Normal Lost Phone.

In this 30 min presentation, we started from an empty Unity project and proceeded to add some key elements that made the game work as a standalone "Lost Phone" game inspired by our two games, A Normal Lost Phone and Another Lost Phone. The performance was accompanied by some detailed explanation on a few key points, in order to help understand our thought process, architectural choices, and how they could be adapted to other games.
The slides (and comments, remember to enable the **speaker notes**!) are available [over here](https://docs.google.com/presentation/d/1aOX2LDuCOz8TtBanvBCyZd2Ap4Ti3LAaO0MfWCVB6p4/).

**This project is far from being optimized or production-ready, and should be considered in the context of the talk it was made for.**

Uses Unity v5.6.3f1, though most of the code should work on v5.5 upwards (?).

# Features
* Canvas parameters and UI components to work with common smartphones aspect ratios
* Home, Messages and Weather "fake" apps
* Loading dynamic content from a CSV file
* Popup notification
* Unlocking some content in reaction to ingame events

# About
Learn more about [Accidental Queens](https://www.accidentalqueens.com) and the Lost Phone games.

The `fgCSVReader.cs` script comes from frozax's [C# CSV Reader](https://github.com/frozax/fgCSVReader).

The repository includes icons from Google's [Material Design](https://github.com/google/material-design-icons) project.

Thanks to [gaeel](http://spaceshipsin.space/) for the help with the stupid live setup that we had to come up with for the talk.